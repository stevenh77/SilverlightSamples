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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private IList<Company> _companies;
        private readonly IConfigurationService _configurationService;
        private List<Price> _currentPrices;
        private string _serviceUrl;
        private readonly IWebClientShim _webClient;

        public bool IsRunning { get; private set; }
        public SummaryStats Stats { get; private set; }

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

        private void GetCompanyList()
        {
            _companies = _configurationService.GetCompanies();

            var symbolsCsv = _companies.Select(
              c => c.Symbol).Aggregate((c, d) => c + "," + d);

            _serviceUrl = _configurationService.GetServiceUrl(symbolsCsv);
        }

        public IList<Price> GetFullCurrentPrices()
        {
            return _companies.Select(company => new Price(company.Name, company.Symbol, 0, 0)).ToList();
        }

        public event PriceChangedEventHandler PriceChanged;
        public delegate void PriceChangedEventHandler(object sender, PriceChangedEventArgs e);
        protected virtual void OnPriceChanged(Price price)
        {
            Stats.PriceChangeEvents++;

            if (PriceChanged != null)
                PriceChanged(this, new PriceChangedEventArgs() { Price = price });
        }

        public void Start()
        {
            PrepareForServiceStarting();
            Task.Factory.StartNew(DownloadPrices);
        }

        public void Stop()
        {
            IsRunning = false;
            Log.Debug("StockService stopped");
        }

        private void PrepareForServiceStarting()
        {
            Log.Debug("StockService starting");
            IsRunning = true;
            Stats.Reset();
            _currentPrices = new List<Price>(_companies.Count);
        }

        private void DownloadPrices()
        {
            try
            {
                while (IsRunning)
                {
                    Stopwatch timeToDownload;
                    var webResponse = TimedDelegates.Execute(
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
                this.Stop();
            }
        }

        private void PopulatePricesFromWebResponse(string webResponse)
        {
            var webPrices = webResponse.Split(
              new[] { "\n", "\r\n" },
              StringSplitOptions.RemoveEmptyEntries);

            foreach (var webPriceData in webPrices)
            {
                var webPrice = Factory.CreatePrice(webPriceData);

                var localPrice = _currentPrices.Find(
                    x => x.Symbol == webPrice.Symbol);

                if (localPrice == null)
                {
                    _currentPrices.Add(
                        new Price(
                            webPrice.CompanyName, 
                            webPrice.Symbol, 
                            webPrice.CurrentPrice, 
                            webPrice.PreviousPrice));
                    continue;
                }

                if (localPrice.CurrentPrice != webPrice.CurrentPrice)
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
            Stats.LastWebRequest.Duration = (int)timeToDownload.ElapsedMilliseconds;
            Stats.LastWebRequest.PricesDownloaded = _currentPrices.Count;
            Stats.LastWebRequest.Response = webResponse;
            Stats.LastWebRequest.Request = _serviceUrl;
            Stats.LastWebRequest.SymbolCount = _companies.Count;
            Stats.NumberOfRequests++;
            Log.Trace(Stats);
        }
    }
}