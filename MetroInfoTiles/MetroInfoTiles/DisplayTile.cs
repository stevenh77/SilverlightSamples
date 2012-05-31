using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MetroInfoTiles
{
	public class DisplayTile : Control
	{
	    public static readonly DependencyProperty TitleTextProperty =
	        DependencyProperty.Register("TitleText", typeof (string), typeof (DisplayTile), new PropertyMetadata(default(string)));

	    public string TitleText
	    {
	        get { return (string) GetValue(TitleTextProperty); }
	        set { SetValue(TitleTextProperty, value); }
	    }

	    public static readonly DependencyProperty DescriptionTextProperty =
	        DependencyProperty.Register("DescriptionText", typeof (string), typeof (DisplayTile), new PropertyMetadata(default(string)));

	    public string DescriptionText
	    {
	        get { return (string) GetValue(DescriptionTextProperty); }
	        set { SetValue(DescriptionTextProperty, value); }
	    }

	    public static readonly DependencyProperty SideBarColourProperty =
	        DependencyProperty.Register("SideBarColour", typeof (SolidColorBrush), typeof (DisplayTile), new PropertyMetadata(default(SolidColorBrush)));

	    public SolidColorBrush SideBarColour
	    {
	        get { return (SolidColorBrush) GetValue(SideBarColourProperty); }
	        set { SetValue(SideBarColourProperty, value); }
	    }
	}
}