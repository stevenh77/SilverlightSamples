using System;
using System.Collections.Generic;
using System.Linq;
using FxRates.Common;

namespace FxRates.Service
{
  class PriceFactory
  {
    private int _numberOfCcys;

    public PriceFactory()
    {
      _numberOfCcys = (int) Enum.GetValues(typeof(Ccy)).Cast<Ccy>().Max() + 1;
    }

    public readonly List<FxRate> CurrentPrices = new List<FxRate>
      {
          FxRate.Create(Ccy.AUDtoUSD, (decimal) 1.0724, (decimal) 1.0731, DateTime.Now),
          FxRate.Create(Ccy.EURtoCHF, (decimal) 1.2075, (decimal) 1.2094, DateTime.Now),
          FxRate.Create(Ccy.EURtoGBP, (decimal) 0.8375, (decimal) 0.8401, DateTime.Now),
          FxRate.Create(Ccy.EURtoJPY, (decimal) 103.2930, (decimal) 103.3180, DateTime.Now),
          FxRate.Create(Ccy.EURtoUSD, (decimal) 1.3154, (decimal) 1.3276, DateTime.Now),
          FxRate.Create(Ccy.GBPtoJPY, (decimal) 123.3250, (decimal) 123.3350, DateTime.Now),
          FxRate.Create(Ccy.GBPtoUSD, (decimal) 1.5707, (decimal) 1.5723, DateTime.Now),
          FxRate.Create(Ccy.USDtoCAD, (decimal) 0.9974, (decimal) 0.9999, DateTime.Now),
          FxRate.Create(Ccy.USDtoCHF, (decimal) 0.9179, (decimal) 0.9191, DateTime.Now),
          FxRate.Create(Ccy.USDtoJPY, (decimal) 78.5090, (decimal) 78.5187, DateTime.Now)
      };

    public FxRate GetNextPrice()
    {
      Random random = new Random();
      
      var ccyIndex = random.Next(0, _numberOfCcys);
      var rate = CurrentPrices[ccyIndex];

      int randomSpread = random.Next(-100, 100);
      decimal deltaPercentage = (decimal) randomSpread / 10000;
      if (deltaPercentage != 0)
      {
        var bid = Math.Round(rate.Bid * (1 + deltaPercentage), 4);
        var offer = Math.Round(rate.Offer * (1 + deltaPercentage), 4);
        rate.UpdatePrice(bid, offer, DateTime.Now);
      }
      return rate;
    }
  }
}
