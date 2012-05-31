using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using FxRates.Common;
using FxRates.UI.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FxRates.UI.ViewModels
{
  public class FxRatesViewModel : ViewModelBase
  {
    private IPricingService pricingService;
    
    public BindingList<DisplayFxRate> DisplayFxRates { get; set; }
    public ICommand ServiceRunningCommand { get; set; }
    public ICommand SubscriptionCommand { get; set; }
    
    public FxRatesViewModel(IPricingService service)
    {
      pricingService = service;
      GetLatestPrices();

      SubscriptionCommand = new RelayCommand(SubscriptionCommand_Execute);
      ServiceRunningCommand = new RelayCommand(ServiceRunningCommand_Execute);

      var priceUpdates = Observable.FromEventPattern<PriceUpdateEventArgs>(
        pricingService, "PriceUpdate");

      priceUpdates.Where(e => (Subscribed)
         // && (e.EventArgs.LatestPrice.Ccy == Ccy.EURtoGBP) example of filter
          )
                  //.Throttle(TimeSpan.FromSeconds(1))  example of throttling
                  .Subscribe(PriceUpdate);
    }

    public void PriceUpdate(EventPattern<PriceUpdateEventArgs> e)
    {
        var displayRate = DisplayFxRates.First(rate => rate.Ccy == e.EventArgs.LatestPrice.Ccy);
        
        if (displayRate != null)
          displayRate.Update(e.EventArgs.LatestPrice);
    }


    private void GetLatestPrices()
    {
      DisplayFxRates = new BindingList<DisplayFxRate>();
      var currentRates = pricingService.GetFullCurrentPrices();
      foreach (var latestRate in currentRates)
      {
        var displayRate = DisplayFxRate.Create(latestRate);
        DisplayFxRates.Add(displayRate);
      }
    }

    private const string SubscribedPropertyName = "Subscribed";
    private bool _subscribed = false;
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
      get 
      {
        return _serviceRunning;
      }
      set
      {
        if (_serviceRunning == value) return;
        _serviceRunning = value;
        RaisePropertyChanged(ServiceRunningPropertyName);
      }
    }

    void ServiceRunningCommand_Execute()
    {
      if (pricingService.IsRunning)
      {
        pricingService.Stop();
        ServiceRunning = false;
      }
      else
      {
        pricingService.Start();
        ServiceRunning = true;
      }
    }

    void SubscriptionCommand_Execute()
    {
      //toggle subscribed
      Subscribed = !Subscribed;
    }
  }
}
