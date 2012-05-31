using System;
using GalaSoft.MvvmLight;
using Stocks.Common.Models;

namespace Stocks.UI.Models
{
    public class DisplayStockPrice : ObservableObject
    {
        public static DisplayStockPrice Create(Price price)
        {
            return new DisplayStockPrice()
                       {
                           CompanyName =  price.CompanyName,
                           Symbol = price.Symbol,
                           CurrentPrice = price.CurrentPrice,
                           PreviousPrice = price.PreviousPrice,
                           Timestamp = DateTime.Now
                       };
        }

        public void Update(Price price)
        {
            Symbol = price.Symbol;
            CurrentPrice = price.CurrentPrice;
            PreviousPrice = price.PreviousPrice;
            Delta = price.CurrentPrice - price.PreviousPrice;
            Timestamp = DateTime.Now;
        }

        public const string SymbolPropertyName = "Symbol";
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            private set
            {
                if (_symbol == value) return;
                _symbol = value;
                RaisePropertyChanged(SymbolPropertyName);
            }
        }

        public const string CompanyNamePropertyName = "CompanyName";
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            private set
            {
                if (_companyName == value) return;
                _companyName = value;
                RaisePropertyChanged(CompanyNamePropertyName);
            }
        }

        public const string CurrentPricePropertyName = "CurrentPrice";
        private decimal _currentPrice = 0;
        public decimal CurrentPrice
        {
            get { return _currentPrice; }
            private set
            {
                if (_currentPrice == value) return;
                _currentPrice = value;
                RaisePropertyChanged(CurrentPricePropertyName);
            }
        }

        public const string PreviousPricePropertyName = "PreviousPrice";
        private decimal _previousPrice = 0;
        public decimal PreviousPrice
        {
            get { return _previousPrice; }
            private set
            {
                if (_previousPrice == value) return;
                _previousPrice = value;
                RaisePropertyChanged(PreviousPricePropertyName);
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

        public const string TimestampPropertyName = "Timestamp";
        private DateTime _timestamp = DateTime.MinValue;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            private set
            {
                if (_timestamp == value) return;
                _timestamp = value;
                RaisePropertyChanged(TimestampPropertyName);
            }
        }
    }
}
