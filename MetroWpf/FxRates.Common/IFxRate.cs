using System;

namespace FxRates.Common
{
    public interface IFxRate
    {
        decimal Bid { get; }
        Ccy Ccy { get; }
        decimal Offer { get; }
        decimal PreviousOffer { get; }
        DateTime Timestamp { get; }
    }
}