using System;
using FxRates.Common;
using GalaSoft.MvvmLight;

namespace FxRates.UI.Models
{
	public class DisplayFxRate : ObservableObject, IFxRate
	{
		private DisplayFxRate() { }

		public static DisplayFxRate Create(FxRate rate)
		{
			var display = new DisplayFxRate()
				{
					Ccy = rate.Ccy,
					Bid = rate.Bid,
					Offer = rate.Offer,
					PreviousOffer = rate.PreviousOffer,
					Timestamp = rate.Timestamp,
					Spread = getSpread(rate),
          Delta = getDelta(rate)
				};

      return display;
		}

		public void Update(FxRate rate)
		{
			this.Bid = rate.Bid;
			this.Offer = rate.Offer;
			this.PreviousOffer = rate.PreviousOffer;
			this.Timestamp = rate.Timestamp;
			this.Spread = getSpread(rate);
			this.Delta = getDelta(rate);
		}

		private static decimal getSpread(IFxRate rate)
		{
			return Math.Round(rate.Offer - rate.Bid, 4);
		}

		private static decimal getDelta(IFxRate rate)
		{
			return Math.Round(rate.Offer - rate.PreviousOffer, 4);
		}

		public const string CcyPropertyName = "Ccy";
		private Ccy _ccy;
		public Ccy Ccy
		{
			get { return _ccy; }
      private set
			{
				if (_ccy == value) return;
				_ccy = value;
				RaisePropertyChanged(CcyPropertyName);
			}
		}

		public const string BidPropertyName = "Bid";
		private decimal _bid = 0;
		public decimal Bid
		{
			get { return _bid; }
      private set
			{
				if (_bid == value) return;
				_bid = value;
				RaisePropertyChanged(BidPropertyName);
			}
		}

		public const string OfferPropertyName = "Offer";
		private decimal _offer = 0;
		public decimal Offer
		{
			get { return _offer; }
      private set
			{
				if (_offer == value) return;
				_offer = value;
				RaisePropertyChanged(OfferPropertyName);
			}
		}

		public const string PreviousOfferPropertyName = "PreviousOffer";
		private decimal _previousOffer = 0;
		public decimal PreviousOffer
		{
			get { return _previousOffer; }
      private set
			{
				if (_previousOffer == value) return;
				_previousOffer = value;
				RaisePropertyChanged(PreviousOfferPropertyName);
			}
		}

		public const string DeltaPropertyName = "Delta";
		private decimal _delta = 0;
		public decimal Delta
		{
			get { return _delta; }
      private set
			{
				if (_delta == value) return;
				_delta = value;
				RaisePropertyChanged(DeltaPropertyName);
			}
		}

		public const string SpreadPropertyName = "Spread";
		private decimal _spread = 0;
		public decimal Spread
		{
			get { return _spread; }
      private set
			{
				if (_spread == value) return;
				_spread = value;
				RaisePropertyChanged(SpreadPropertyName);
			}
		}

		public const string TimestampPropertyName = "Timestamp";
		private DateTime _timestamp = DateTime.MinValue;
		public DateTime Timestamp
		{
			get { return _timestamp;}
      private set
			{
				if (_timestamp == value) return;
				_timestamp = value;
				RaisePropertyChanged(TimestampPropertyName);
			}
		}
	}
}