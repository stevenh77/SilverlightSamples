using System;
using System.Threading.Tasks;

namespace MetroWpf.Framework
{
  public class ServiceClient
  {
    public static T Execute<T>(
      Func<T> func, 
      int timeoutMilliseconds, 
      int maxAttempts)
    {
      T result;
      Exception e;
      TryExecute(func, timeoutMilliseconds, maxAttempts, out result, out e);
      if (e != null) throw e;
      return result;
    }

    public static bool TryExecute<T>(
      Func<T> func, 
      int timeoutMilliseconds, 
      int maxAttempts, 
      out T result, 
      out Exception e)
    {
      bool isSuccessful = false;
      var t = default(T);
      e = null;

      var task = Task.Factory.StartNew(() =>
      {
        for (int i = 0; i < maxAttempts; i++)
        {
          try { t = func(); }
          catch { if (i == maxAttempts - 1) { throw; } }
        }
      });

      try
      {
        task.Wait(timeoutMilliseconds);
        if (!task.IsCompleted)
          e = new TimeoutException();
        else
          isSuccessful = true;
      }
      catch (AggregateException ae)
      {
        e = ae.InnerException;
      }

      result = t;
      return isSuccessful;
    }
  }
}
