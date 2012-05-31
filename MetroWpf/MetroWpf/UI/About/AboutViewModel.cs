using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using MetroWpf.Framework.Interfaces;

namespace MetroWpf.UI.About
{
  public class AboutViewModel : ViewModelBase
  {
    private readonly IWpfApplication wpfApplication;
    
    public AboutViewModel(IWpfApplication wpfApplication)
    {
      this.wpfApplication = wpfApplication;
    }

    public string ApplicationTitle { get { return wpfApplication.ApplicationTitle; } }

    public BitmapSource ApplicationIcon { get { return wpfApplication.ApplicationIcon; } }

    public string Version
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public string Configuration
    {
      get
      {
        var attr = Assembly.GetExecutingAssembly()
            .GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false)
            .OfType<AssemblyConfigurationAttribute>()
            .FirstOrDefault() ?? new AssemblyConfigurationAttribute("");

        return attr.Configuration;
      }
    }

    // let's evolve the app as a fully loaded starting point for an app
    private readonly List<string> authors = new List<string>
                                                    {
                                                         "Steven Hollidge"
                                                     };
    public string Authors
    {
      get { return string.Join(", ", authors); }
    }

    private readonly List<string> components = new List<string>
                                                       {
                                                         "MahApps.Metro",
                                                         "Markdown",
                                                         "MVVM Light"                                                        
                                                     };
    public string Components
    {
      get { return string.Join(", ", components); }
    }

    public void Visit()
    {
      try
      {
        System.Diagnostics.Process.Start("http://stevenhollidge.com");
      }
      catch // browser exceptions
      {

      }
    }
  }
}
