using System;
using System.Collections.Generic;

namespace MetroWpf
{
  public static class CollectionExtensions
  {
    #region · Extensions ·

    public static bool AddUnique<T>(this ICollection<T> collection, T value)
    {
      if (!collection.Contains(value))
      {
        collection.Add(value);
        return true;
      }

      return false;
    }

    public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
    {
      var count = 0;

      foreach (var value in values)
      {
        if (collection.AddUnique(value))
        {
          count++;
        }
      }

      return count;
    }

    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
    {
      foreach (T value in values)
      {
        collection.Add(value);
      }
    }

    #endregion
  }
}