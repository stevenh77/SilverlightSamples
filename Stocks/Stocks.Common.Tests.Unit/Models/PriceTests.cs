using Stocks.Common.Models;
using Xunit;
using System.Collections.Generic;

namespace Stocks.Common.Tests.Unit.Models
{
  public class PriceTests
  {
    #region GetHashCode tests
    [Fact]
    public void GetHashCode_success()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal) 123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal) 123.45;
      rhs.PreviousPrice = 0;
      var expected = lhs.GetHashCode();
      var actual = rhs.GetHashCode();
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetHashCode_should_be_different()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal) 123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = 0;
      rhs.PreviousPrice = (decimal)123.45;
      var expected = lhs.GetHashCode();
      var actual = rhs.GetHashCode();
      Assert.NotEqual(expected, actual);
    }

    [Fact]
    public void GetHashCode_should_be_different_and_not_overflow()
    {
      var lhs = new Price() 
      { 
        Symbol = "GOOG", 
        CurrentPrice = (decimal) 999999.99, 
        PreviousPrice = (decimal) 999999.99 
      };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal) 999999.99;
      rhs.PreviousPrice = 0;
      var expected = lhs.GetHashCode();
      var actual = rhs.GetHashCode();
      Assert.NotEqual(expected, actual);
    }
    #endregion

    #region Equals tests
    [Fact]
    public void Equals_success()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal)123.45;
      rhs.PreviousPrice = 0;
      var expected = true;

      var actual = lhs.Equals(rhs);
      Assert.Equal(expected, actual);

      actual = rhs.Equals(lhs);
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Equals_expected_to_fail()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal)123.44;
      rhs.PreviousPrice = 0;
      var expected = true;

      var actual = lhs.Equals(rhs);
      Assert.NotEqual(expected, actual);

      actual = rhs.Equals(lhs);
      Assert.NotEqual(expected, actual);
    }

    [Fact]
    public void Equals_success_using_object()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal)123.45;
      rhs.PreviousPrice = 0;

      object obj = rhs;
      var expected = true;

      var actual = lhs.Equals(obj);
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Equals_expected_to_fail_using_object()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal)123.46;
      rhs.PreviousPrice = 0;

      object obj = rhs;
      var expected = true;

      var actual = lhs.Equals(obj);
      Assert.NotEqual(expected, actual);
    }
    #endregion

    #region CompareTo tests

    public void CompareTo_the_same_by_symbol_implicit_comparer()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      
      var rhs = new Price();
      rhs.Symbol = "GOOG";
      rhs.CurrentPrice = (decimal)999;
      rhs.PreviousPrice = 0;
      
      var expected = 0;
      var actual = lhs.CompareTo(rhs);
      
      Assert.Equal(expected, actual);
    }

    public void CompareTo_different_by_symbol_implicit_comparer()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      
      var rhs = new Price();
      rhs.Symbol = "AAPL";
      rhs.CurrentPrice = (decimal)123.45;
      rhs.PreviousPrice = 0;
      
      var expected = 1;
      var actual = lhs.CompareTo(rhs);
      
      Assert.Equal(expected, actual);

      expected = -1;
      actual = rhs.CompareTo(lhs);

      Assert.Equal(expected, actual);
    }

    public void CompareTo_same_by_current_price()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };
      
      var rhs = new Price();
      rhs.Symbol = "MSFT";
      rhs.CurrentPrice = (decimal)123.45;
      rhs.PreviousPrice = (decimal)999;
      
      var expected = 0;
      var actual = rhs.CompareTo(lhs, PriceComparisonType.CurrentPrice);

      Assert.Equal(expected, actual);
    }

    public void CompareTo_different_by_current_price()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };

      var rhs = new Price();
      rhs.Symbol = "MSFT";
      rhs.CurrentPrice = (decimal)9;
      rhs.PreviousPrice = (decimal)999;

      var expected = -1;
      var actual = lhs.CompareTo(rhs, PriceComparisonType.CurrentPrice);

      Assert.Equal(expected, actual);

      expected = 1;
      actual = rhs.CompareTo(lhs, PriceComparisonType.CurrentPrice);
      Assert.Equal(expected, actual);
    }

    public void CompareTo_same_by_previous_price()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)44, PreviousPrice = 999 };

      var rhs = new Price();
      rhs.Symbol = "MSFT";
      rhs.CurrentPrice = (decimal)123.45;
      rhs.PreviousPrice = (decimal)999;

      var expected = 0;
      var actual = rhs.CompareTo(lhs, PriceComparisonType.PreviousPrice);

      Assert.Equal(expected, actual);
    }

    public void CompareTo_different_by_previous_price()
    {
      var lhs = new Price() { Symbol = "GOOG", CurrentPrice = (decimal)123.45, PreviousPrice = 0 };

      var rhs = new Price();
      rhs.Symbol = "MSFT";
      rhs.CurrentPrice = (decimal)9;
      rhs.PreviousPrice = (decimal)999;

      var expected = -1;
      var actual = lhs.CompareTo(rhs, PriceComparisonType.PreviousPrice);

      Assert.Equal(expected, actual);

      expected = 1;
      actual = rhs.CompareTo(lhs, PriceComparisonType.PreviousPrice);
      Assert.Equal(expected, actual);
    }
    #endregion

    #region == tests

    #endregion

    #region != tests

    #endregion

    #region List tests

    [Fact]
    public void Sorting_a_list()
    {
      var actual = getListOfPrices();

      var expected = new List<Price>(5)
      {
        new Price() { Symbol = "AAPL", CurrentPrice = (decimal)123.45, PreviousPrice = (decimal) 123.40 },
        new Price() { Symbol = "DIZ", CurrentPrice = (decimal)96.45, PreviousPrice = (decimal) 87.20 },
        new Price() { Symbol = "GOOG", CurrentPrice = (decimal)89.23, PreviousPrice = (decimal) 90.86 },
        new Price() { Symbol = "MSFT", CurrentPrice = (decimal)67.45, PreviousPrice = (decimal) 92.23 },
        new Price() { Symbol = "ZZZ", CurrentPrice = (decimal)3123.45, PreviousPrice = (decimal) 3123.45 }
      };

      actual.Sort();
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sorting_a_list_by_currentPrice()
    {
      var actual = getListOfPrices();

      var expected = new List<Price>(5)
      {
        new Price() { Symbol = "MSFT", CurrentPrice = (decimal)67.45, PreviousPrice = (decimal) 92.23 },
        new Price() { Symbol = "GOOG", CurrentPrice = (decimal)89.23, PreviousPrice = (decimal) 90.86 },
        new Price() { Symbol = "DIZ", CurrentPrice = (decimal)96.45, PreviousPrice = (decimal) 87.20 },
        new Price() { Symbol = "AAPL", CurrentPrice = (decimal)123.45, PreviousPrice = (decimal) 123.40 },
        new Price() { Symbol = "ZZZ", CurrentPrice = (decimal)3123.45, PreviousPrice = (decimal) 3123.45 }
      };
      
      var comparer = new Price.PriceComparer();
      comparer.ComparisonMethod = PriceComparisonType.CurrentPrice;
      actual.Sort(comparer);
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sorting_a_list_by_previousPrice()
    {
      var actual = getListOfPrices();

      var expected = new List<Price>(5)
      {
        new Price() { Symbol = "DIZ", CurrentPrice = (decimal)96.45, PreviousPrice = (decimal) 87.20 },
        new Price() { Symbol = "GOOG", CurrentPrice = (decimal)89.23, PreviousPrice = (decimal) 90.86 },
        new Price() { Symbol = "MSFT", CurrentPrice = (decimal)67.45, PreviousPrice = (decimal) 92.23 },
        new Price() { Symbol = "AAPL", CurrentPrice = (decimal)123.45, PreviousPrice = (decimal) 123.40 },
        new Price() { Symbol = "ZZZ", CurrentPrice = (decimal)3123.45, PreviousPrice = (decimal) 3123.45 }
      };

      var comparer = new Price.PriceComparer();
      comparer.ComparisonMethod = PriceComparisonType.PreviousPrice;
      actual.Sort(comparer);
      Assert.Equal(expected, actual);
    }

    private List<Price> getListOfPrices()
    {
      return new List<Price>(5)
      {
        new Price() { Symbol = "GOOG", CurrentPrice = (decimal)89.23, PreviousPrice = (decimal) 90.86 },
        new Price() { Symbol = "AAPL", CurrentPrice = (decimal)123.45, PreviousPrice = (decimal) 123.40 },
        new Price() { Symbol = "MSFT", CurrentPrice = (decimal)67.45, PreviousPrice = (decimal) 92.23 },
        new Price() { Symbol = "ZZZ", CurrentPrice = (decimal)3123.45, PreviousPrice = (decimal) 3123.45 },
        new Price() { Symbol = "DIZ", CurrentPrice = (decimal)96.45, PreviousPrice = (decimal) 87.20 }
      };
    }

    #endregion

    // need to test for null and other object types being passed in as null
  }
}
