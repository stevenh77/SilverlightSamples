using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MetroWpf.Framework.Interfaces
{
  public interface IWpfApplication
  {
    /// <summary>
    ///   Tasks run on this scheduler will execute on the UI thread
    /// </summary>
    TaskScheduler UiTaskScheduler { get; }

    void Initialize();
    void Invoke(Action action);

    void RefreshCommands();

    string ApplicationTitle {get; }
    BitmapSource ApplicationIcon { get; } 
  }
}
