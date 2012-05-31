using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Stocks.Common;
using Stocks.Common.Core;
using Stocks.Common.Events;
using Stocks.Common.Models;

namespace Stocks.Service
{
    public class StocksService : IStocksService
    {
        #region Delegates

        public delegate void PriceChangedEventHandler(object sender, PriceChangedEventArgs e);

        #endregion

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IConfigurationService _configurationService;
        private readonly IWebClientShim _webClient;
        private IList<Company> _companies;
        private List<Price> _currentPrices;
        private string _serviceUrl;

        public StocksService(
            IConfigurationService configurationService,
            IWebClientShim webClientShim)
        {
            new AssemblyInit();

            _webClient = webClientShim;
            _configurationService = configurationService;
            Stats = new SummaryStats();

            GetCompanyList();
        }

        public SummaryStats Stats { get; private set; }

        #region IStocksService Members

        public bool IsActive { get; private set; }

        public void Start()
        {
            PrepareForServiceStarting();
            Task.Factory.StartNew(DownloadPrices);
        }

        public void Stop()
        {
            IsActive = false;
            Log.Debug("StockService stopped");
        }

        #endregion

        private void GetCompanyList()
        {
            _companies = _configurationService.GetCompanies();

            string symbolsCsv = _companies.Select(
                c => c.Symbol).Aggregate((c, d) => c + "," + d);

            _serviceUrl = _configurationService.GetServiceUrl(symbolsCsv);
        }

        public event PriceChangedEventHandler PriceChanged;

        protected virtual void OnPriceChanged(Price price)
        {
            Stats.PriceChangeEvents++;

            if (PriceChanged != null)
                PriceChanged(this, new PriceChangedEventArgs {Price = price});
        }

        private void PrepareForServiceStarting()
        {
            Log.Debug("StockService starting");
            IsActive = true;
            Stats.Reset();
            _currentPrices = new List<Price>(_companies.Count);
        }

        private void DownloadPrices()
        {
            try
            {
                while (IsActive)
                {
                    Stopwatch timeToDownload;
                    string webResponse = TimedDelegates.Execute(
                        _webClient.DownloadString,
                        _serviceUrl,
                        out timeToDownload);

                    PopulatePricesFromWebResponse(webResponse);
                    UpdateStats(timeToDownload, webResponse);
                }
            }
            catch (Exception e)
            {
                Log.Error("Exception during DownloadPrices()");
                Log.Error("Stack Trace {0}: /r/nException Message: {1}", e.StackTrace, e.Message);
                Stop();
            }
        }

        private void PopulatePricesFromWebResponse(string webResponse)
        {
            string[] webPrices = webResponse.Split(
                new[] {"\n", "\r\n"},
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string webPriceData in webPrices)
            {
                Price webPrice = Factory.CreatePrice(webPriceData);
                Price localPrice = _currentPrices.Find(x => x.Symbol == webPrice.Symbol);

                if (localPrice == null)
                {
                    _currentPrices.Add(new Price(webPrice.Symbol, webPrice.CurrentPrice, webPrice.PreviousPrice));
                    continue;
                }

                if (!localPrice.Equals(webPrice))
                    UpdateLocalPrice(webPrice, localPrice);
            }
        }

        private void UpdateLocalPrice(Price webPrice, Price localPrice)
        {
            localPrice.PreviousPrice = localPrice.CurrentPrice;
            localPrice.CurrentPrice = webPrice.CurrentPrice;
            OnPriceChanged(localPrice);
        }

        private void UpdateStats(Stopwatch timeToDownload, string webResponse)
        {
            Stats.LastWebRequest.Duration = (int) timeToDownload.ElapsedMilliseconds;
            Stats.LastWebRequest.PricesDownloaded = _currentPrices.Count;
            Stats.LastWebRequest.Response = webResponse;
            Stats.LastWebRequest.Request = _serviceUrl;
            Stats.LastWebRequest.SymbolCount = _companies.Count;
            Stats.NumberOfRequests++;
            Log.Trace(Stats);
        }
    }
}