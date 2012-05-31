To install SilverlightGlimpse in your application you will need to:

1) Add two references (if using NuGet this will be done automatically for you):
  a)  SilverlightGlimpse.dll
  b)  System.Windows.Controls.dll (part of the Silverlight Toolkit SDK http://silverlight.codeplex.com/)

2) Wrap your VisualRoot by include the following code in your App.xaml.cs file:

private void Application_Startup(object sender, StartupEventArgs e)
{
	try
	{
		// Replace MainPage for the name of your startup object
		this.RootVisual = new MainPage();
		SilverlightGlimpse.Services.Glimpse.Service.Load(this);
	}
	catch (Exception ex)
	{
		SilverlightGlimpse.Services.Glimpse.Service.DisplayLoadFailure(this, ex);
	}
}