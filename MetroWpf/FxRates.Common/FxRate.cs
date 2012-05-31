using System;

namespace FxRates.Common
{
    public class FxRate : IFxRate
    {
        private FxRate(Ccy ccy, decimal bid, decimal offer, DateTime timestamp)
        {
            Ccy = ccy;
            Bid = bid;
            PreviousOffer = offer;
            Offer = offer;
            Timestamp = timestamp;
        }

        #region IFxRate Members

        public decimal Bid { get; private set; }
        public Ccy Ccy { get; private set; }
        public decimal Offer { get; private set; }
        public decimal PreviousOffer { get; private set; }
        public DateTime Timestamp { get; private set; }

        #endregion

        public static FxRate Create(Ccy ccy, decimal bid, decimal offer, DateTime timestamp)
        {
            return new FxRate(ccy, bid, offer, timestamp);
        }

        public void UpdatePrice(decimal bid, decimal offer, DateTime timestamp)
        {
            Bid = bid;
            PreviousOffer = Offer;
            Offer = offer;
            Timestamp = timestamp;
        }
    }
}