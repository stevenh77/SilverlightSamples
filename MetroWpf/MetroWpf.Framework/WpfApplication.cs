using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MetroWpf.Framework.Interfaces;
using System.Windows.Media.Imaging;

namespace MetroWpf.Framework
{
  public class WpfApplication : IWpfApplication
  {
    private Dispatcher _dispatcher;
    public TaskScheduler UiTaskScheduler { get; private set; }

    public BitmapSource ApplicationIcon { get; private set; }
    public string ApplicationTitle { get { return "Metro WPF"; } }

    public void Initialize()
    {
      _dispatcher = Application.Current.Dispatcher;
      UiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

      ApplicationIcon = new BitmapImage(
       new Uri("../Presentation/logo.ico", UriKind.Relative));
    }

    public void Invoke(Action action)
    {
      _dispatcher.Invoke(action);
    }

    public void RefreshCommands()
    {
      CommandManager.InvalidateRequerySuggested();
    }
  }
}
