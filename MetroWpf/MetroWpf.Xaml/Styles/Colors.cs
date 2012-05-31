using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MetroWpf.Xaml.Styles
{
  public static class AccentColors
  {
    public static readonly Color Black = Color.FromRgb(0, 0, 0);
    public static readonly Color Blue = Color.FromArgb(0xFF, 0x1B, 0xA1, 0xE2);
    public static readonly Color Brown = Color.FromArgb(0xFF, 0xA0, 0x50, 0x00);
    public static readonly Color Green = Color.FromArgb(0xFF, 0x33, 0x99, 0x33);
    public static readonly Color Lime = Color.FromArgb(0xFF, 0x8C, 0xBF, 0x26);
    public static readonly Color Magenta = Color.FromArgb(0xFF, 0xFF, 0x00, 0x97);
    public static readonly Color Orange = Color.FromArgb(0xFF, 0xF0, 0x96, 0x09);
    public static readonly Color Pink = Color.FromArgb(0xFF, 0xE6, 0x71, 0xB8);
    public static readonly Color Purple = Color.FromArgb(0xFF, 0xA2, 0x00, 0xFF);
    public static readonly Color Red = Color.FromArgb(0xFF, 0xE5, 0x14, 0x00);
    public static readonly Color Viridian = Color.FromArgb(0xFF, 0x00, 0xAB, 0xA9);
    public static readonly Color White = Color.FromRgb(240, 240, 240);
    public static readonly Color Yellow = Color.FromRgb(255, 247, 137);
  }

  public static class DarkColors
  {
    public static readonly Color Transparent = Color.FromArgb(0x00, 0x11, 0x11, 0x011);
    public static readonly Color Semitransparent = Color.FromArgb(0xAA, 0x11, 0x11, 0x11);
    public static readonly Color Background = Color.FromArgb(0xFF, 0x11, 0x11, 0x11);
    public static readonly Color Foreground = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
    public static readonly Color Contrast = Color.FromArgb(0xFF, 0x11, 0x11, 0x11);
    public static readonly Color Highlight = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);
    public static readonly Color MiddleLight = Color.FromArgb(0xFF, 0x99, 0x99, 0x99);
    public static readonly Color Lowlight = Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC);
    public static readonly Color Disabled = Color.FromArgb(0xFF, 0x6C, 0x69, 0x66);
  }

  public static class LightColors
  {
    public static readonly Color Transparent = Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF);
    public static readonly Color Semitransparent = Color.FromArgb(0xAA, 0xFF, 0xFF, 0xFF);
    public static readonly Color Background = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
    public static readonly Color Foreground = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
    public static readonly Color Contrast = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
    public static readonly Color Highlight = Color.FromArgb(0xFF, 0xE2, 0xE2, 0xE2);
    public static readonly Color MiddleLight = Color.FromArgb(0xFF, 0x77, 0x77, 0x77);
    public static readonly Color Lowlight = Color.FromArgb(0xFF, 0x4D, 0x4D, 0x4D);
    public static readonly Color Disabled = Color.FromArgb(0xFF, 0xB8, 0xB5, 0xB2);
  }
}
