using System;
using System.Diagnostics;

namespace Stocks.Common.Core
{
  public class TimedDelegates
  {
    public static T Execute<T>(
      Func<T, T> func,
      T paramIn,
      out Stopwatch stopwatch)
    {
      stopwatch = new Stopwatch();
      stopwatch.Start();
      T result = func(paramIn);
      stopwatch.Stop();
      return result;
    }
  }
}