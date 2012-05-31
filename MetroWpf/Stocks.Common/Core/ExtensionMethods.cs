using System;

namespace Stocks.Common.Core
{
  public static class StringHelper
  {
    /// <summary>
    /// Receives string and returns the string with its letters reversed.
    /// </summary>
    public static string Reverse(this string s)
    {
      if (s == null) return null;
      char[] arr = s.ToCharArray();
      Array.Reverse(arr);
      return new string(arr);
    }
  }
}
