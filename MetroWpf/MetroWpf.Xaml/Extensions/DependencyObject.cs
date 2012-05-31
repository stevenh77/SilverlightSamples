using System.Windows;
using System.Windows.Media;

namespace MetroWpf.Xaml.Extensions
{
  public static class DependencyObjectExtensions
  { 
    /// <summary>
    /// Finds a parent of a given control/item on the visual tree.
    /// </summary>
    /// <typeparam name="T">Type of Parent</typeparam>
    /// <param name="child">Child whose parent is queried</param>
    /// <returns>Returns the first parent item that matched the type (T), if no match found then it will return null</returns>
    public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
    {
      DependencyObject parentObject = VisualTreeHelper.GetParent(child);
      if (parentObject == null) return null;
      T parent = parentObject as T;
      if (parent != null)
        return parent;
      else
        return TryFindParent<T>(parentObject);
    }  
  }
}
