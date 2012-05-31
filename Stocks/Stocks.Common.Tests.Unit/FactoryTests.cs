using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Stocks.Common.Models;
using Stocks.Common.Exceptions;

namespace Stocks.Common.Tests.Unit
{
  public class FactoryTests
  {
    [Fact]
    private void Create_for_success()
    {
      // "GOOG","Google Inc.",604.64
      string value = @"""GOOG"",""Google Inc."",604.64";

      var actual = Factory.CreatePrice(value);
      var expected = new Price() 
        { 
          Symbol = "GOOG", 
          CurrentPrice = (decimal) 604.64,
          PreviousPrice = (decimal) 604.64
        };

      Assert.Equal<Price>(expected, actual);
    }

    [Fact]
    private void Create_for_success_with_comma_in_name() 
    {
      // "AAPL","Apple Corp, Inc.",0.79
      string value = @"""AAPL"",""Apple Corp, Inc"",0.79";

      var actual = Factory.CreatePrice(value);
      var expected = new Price()
      {
        Symbol = "AAPL",
        CurrentPrice = (decimal) 0.79,
        PreviousPrice = (decimal) 0.79
      };

      Assert.Equal<Price>(expected, actual);
    }


    [Fact]
    private void Create_for_success_with_single_char_symbol()
    {
      // "X","Tiny",0
      string value = @"""X"",""Tiny"",0";

      var actual = Factory.CreatePrice(value);
      var expected = new Price()
      {
        Symbol = "X",
        CurrentPrice = (decimal)0,
        PreviousPrice = 0
      };

      Assert.Equal<Price>(expected, actual);
    }
    [Fact]
    private void Create_for_success_with_zero_price()
    {
      // "XXXX","Beer Co",0
      string value = @"""XXXX"",""Beer Co"",0";

      var actual = Factory.CreatePrice(value);
      var expected = new Price()
      {
        Symbol = "XXXX",
        CurrentPrice = (decimal)0,
        PreviousPrice = 0
      };

      Assert.Equal<Price>(expected, actual);
    }
    [Fact]
    private void Create_with_missing_format_variable_raises_exception()
    {
      string value = "Missing Format Variable.";
      Assert.Throws<InvalidWebPriceDataException>(
        () => Factory.CreatePrice(value));
    }

    [Fact]
    private void Create_with_empty_raises_exception()
    {
      string value = string.Empty;
      Assert.Throws<InvalidWebPriceDataException>(
        () => Factory.CreatePrice(value));
    }

    [Fact]
    private void Create_with_null_raises_exception()
    {
      string value = null;
      Assert.Throws<InvalidWebPriceDataException>(
        () => Factory.CreatePrice(value));
    }
  }
}
