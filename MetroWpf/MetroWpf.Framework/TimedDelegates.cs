using System;
using System.Diagnostics;

namespace MetroWpf.Framework
{
  public class TimedDelegates
  {
    public static void ExecuteAction(
  Action action,
  out Stopwatch stopwatch)
    {
      stopwatch = new Stopwatch();
      stopwatch.Start();
      action();
      stopwatch.Stop();
    }

    public static void ExecuteAction<T>(
      Action<T> action,
      T paramIn,
      out Stopwatch stopwatch)
    {
      stopwatch = new Stopwatch();
      stopwatch.Start();
      action(paramIn);
      stopwatch.Stop();
    }

    public static T ExecuteFunc<T>(
      Func<T> func,
      out Stopwatch stopwatch)
    {
      stopwatch = new Stopwatch();
      stopwatch.Start();
      T result = func();
      stopwatch.Stop();
      return result;
    }

    public static T ExecuteFunc<T>(
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
