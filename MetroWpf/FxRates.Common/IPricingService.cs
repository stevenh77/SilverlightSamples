using System;
using System.Collections.Generic;

namespace FxRates.Common
{
    public interface IPricingService
    {
        bool IsRunning { get; }
        List<FxRate> GetFullCurrentPrices();
        void Start();
        void Stop();
        event EventHandler<PriceUpdateEventArgs> PriceUpdate;
    }
}