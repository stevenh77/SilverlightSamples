using System;
using Stocks.Common.Core;
using Xunit;

namespace Stocks.Common.Tests.Unit.Core
{
  public class ExtensionMethodsTests
  {
    [Fact]
    public void Expecting_success()
    {
      string value = "ABCD";
      string expected = "DCBA";
      string actual = value.Reverse();
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Expecting_success_with_null()
    {
      string value = null;
      string expected = null;
      string actual = value.Reverse();
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Expecting_success_with_zero_length_string()
    {
      string value = string.Empty;
      string expected = string.Empty;
      string actual = value.Reverse();
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void Expecting_success_with_space()
    {
      string value = "steven hollidge";
      string expected = "egdilloh nevets";
      string actual = value.Reverse();
      Assert.Equal(expected, actual);
    }
  }
}
