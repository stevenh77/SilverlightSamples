using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Stocks.Common;
using Stocks.Common.Events;
using Stocks.UI.Models;

namespace Stocks.UI.ViewModels
{
    public class StocksViewModel : ViewModelBase
    {
        private readonly IStocksService _service;

        public BindingList<DisplayStockPrice> DisplayStockPrices { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand SubscriptionCommand { get; set; }

        public StocksViewModel(IStocksService service)
        {
            _service = service;
            GetLatestPrices();

            SubscriptionCommand = new RelayCommand(SubscriptionCommandExecute);
            ServiceCommand = new RelayCommand(ServiceRunningCommandExecute);

            var priceUpdates = Observable.FromEventPattern<PriceChangedEventArgs>(
                _service, 
                "PriceChanged");

            priceUpdates.Where(e => Subscribed)
                //.Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(PriceChanged);
        }

        public void PriceChanged(EventPattern<PriceChangedEventArgs> e)
        {
            var displayRate = DisplayStockPrices.First(
                rate => rate.Symbol == e.EventArgs.Price.Symbol);

            if (displayRate != null)
                displayRate.Update(e.EventArgs.Price);
        }


        private void GetLatestPrices()
        {
            DisplayStockPrices = new BindingList<DisplayStockPrice>();
            var currentRates = _service.GetFullCurrentPrices();
            foreach (var latestRate in currentRates)
            {
                var displayRate = DisplayStockPrice.Create(latestRate);
                DisplayStockPrices.Add(displayRate);
            }
        }

        private const string SubscribedPropertyName = "Subscribed";
        private bool _subscribed;

        public bool Subscribed
        {
            get { return _subscribed; }
            set
            {
                if (_subscribed == value) return;
                _subscribed = value;
                RaisePropertyChanged(SubscribedPropertyName);
            }
        }

        private const string ServiceRunningPropertyName = "ServiceRunning";
        private bool _serviceRunning;

        public bool ServiceRunning
        {
            get { return _serviceRunning; }
            set
            {
                if (_serviceRunning == value) return;
                _serviceRunning = value;
                RaisePropertyChanged(ServiceRunningPropertyName);
            }
        }

        private void ServiceRunningCommandExecute()
        {
            if (_service.IsRunning)
            {
                _service.Stop();
                ServiceRunning = false;
            }
            else
            {
                _service.Start();
                ServiceRunning = true;
            }
        }

        private void SubscriptionCommandExecute()
        {
            //toggle subscribed
            Subscribed = !Subscribed;
        }
    }
}