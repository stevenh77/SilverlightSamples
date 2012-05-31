using System;

namespace FxRates.Common
{
    public class PriceUpdateEventArgs : EventArgs
    {
        public FxRate LatestPrice { get; set; }
    }
}