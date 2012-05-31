using Stocks.Common.Core;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Stocks.Common.Models
{
  public class Price : IEquatable<Price>, IComparable<Price>, IComparable
  {
    public string Symbol { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal PreviousPrice { get; set; }

    public Price()
    {
    }

    public Price(string symbol, decimal currentPrice, decimal previousPrice)
      : this()
    {
      Symbol = symbol;
      CurrentPrice = currentPrice;
      PreviousPrice = previousPrice;
    }

    public override bool Equals(object obj)
    {
      if (obj is Price)
        return Equals(obj as Price);
      else
        return false;
    }

    public bool Equals(Price other)
    {
      return (Symbol == other.Symbol
        && CurrentPrice == other.CurrentPrice
        && PreviousPrice == other.PreviousPrice);
    }

    public int CompareTo(Price other)
    {
      return Symbol.CompareTo(other.Symbol);
    }

    public int CompareTo(Price other, PriceComparisonType comparisonType)
    {
      switch (comparisonType)
      {
        case PriceComparisonType.NotSet:
        case PriceComparisonType.Symbol:
          return Symbol.CompareTo(other.Symbol);
        case PriceComparisonType.CurrentPrice:
          return CurrentPrice.CompareTo(other.CurrentPrice);
        case PriceComparisonType.PreviousPrice:
          return PreviousPrice.CompareTo(other.PreviousPrice);
        default:
          throw new Exception("Unknown comparison type");
      }
    }

    public int CompareTo(object obj)
    {
      Price other;
      if (obj is Price)
        other = obj as Price;
      else
        throw new ArgumentException("obj is not a Price");

      return CompareTo(other);
    }
    public override int GetHashCode()
    {
      int hash = 13;
      hash = (hash * 7) + Symbol.GetHashCode();
      hash = (hash * 7) + CurrentPrice.GetHashCode();
      hash = (hash * 7) + PreviousPrice.GetHashCode();
      return hash;
    }

    public static bool operator ==(Price lhs, Price rhs)
    {
      if (System.Object.ReferenceEquals(lhs, rhs))
        return true;

      if (((object)lhs == null) || ((object)rhs == null))
        return false;

      return lhs.Symbol == rhs.Symbol
        && lhs.CurrentPrice == rhs.CurrentPrice
        && lhs.PreviousPrice == rhs.PreviousPrice;
    }

    public static bool operator !=(Price lhs, Price rhs)
    {
      return !(lhs == rhs);
    }


    public class PriceComparer : IComparer<Price>, IComparer
    {
      public PriceComparisonType ComparisonMethod { get; set; }

      public int Compare(Price x, Price y)
      {
        return x.CompareTo(y, ComparisonMethod);
      }

      public int Compare(object x, object y)
      {
        Price lhs, rhs;

        if (x is Price)
          lhs = x as Price;
        else
          throw new ArgumentException("x is not a Price");

        if (y is Price)
          rhs = y as Price;
        else
          throw new ArgumentException("y is not a Price");

        return lhs.CompareTo(rhs, ComparisonMethod);
      }
    }
  }

  public enum PriceComparisonType { NotSet = 0, Symbol, CurrentPrice, PreviousPrice }
}
