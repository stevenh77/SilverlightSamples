using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using MetroWpf.Xaml.Styles;

namespace MetroWpf.Xaml.Adorners
{
  public class NotesAdorner : Adorner
  {
    public NotesAdorner(UIElement adornedElement)
      : base(adornedElement)
    {
      NoteBrush = new SolidColorBrush(AccentColors.Black);
      NoteWidth = 70;
      NoteHeight = 30;

      TextBrush = new SolidColorBrush(AccentColors.White);
      Text = "Adorner!";
      TextTypeface = new Typeface("Segoe UI");
    }

    public SolidColorBrush NoteBrush { get; set; }
    public double NoteWidth { get; set; }
    public double NoteHeight { get; set; }
    
    public SolidColorBrush TextBrush { get; set; }
    public string Text { get; set; }
    public Typeface TextTypeface { get; set; }

    protected override void OnRender(DrawingContext drawingContext)
    {
      Rect adornedElementRect = 
        new Rect(this.AdornedElement.DesiredSize);

      var noteContainer =
        DrawContainer(drawingContext, adornedElementRect);

      DrawMessageText(
        drawingContext,
        noteContainer.Left,
        noteContainer.Top);
    }

    private Rect DrawContainer(
      DrawingContext drawingContext, 
      Rect adornedElementRect)
    {
      // border and fill color
      SolidColorBrush renderBrush = NoteBrush;
      renderBrush.Opacity = 0.2;
      Pen renderPen = new Pen(NoteBrush, 1.5);

      // location and sizing
      double xOffset = 20;
      double yOffset = 0;
      double renderRadius = 5.0;

      Point topLeftPosition = new Point(
        adornedElementRect.Right + xOffset,
        adornedElementRect.Top + yOffset);

      Point bottomRightPosition = new Point(
        adornedElementRect.Right + xOffset + NoteWidth,
        adornedElementRect.Top + NoteHeight);

      Rect noteContainer = new Rect(
        topLeftPosition, 
        bottomRightPosition);
      
      //Add to visual tree (should we add to logical tree as well?)
      drawingContext.DrawRoundedRectangle(
        renderBrush,
        renderPen,
        noteContainer,
        renderRadius,
        renderRadius);

      return noteContainer;
    }

    private void DrawMessageText(
      DrawingContext drawingContext,
      double left, 
      double top)
    {
      FormattedText formattedText = new FormattedText(
        Text,
        CultureInfo.InvariantCulture,
        FlowDirection.LeftToRight,
        TextTypeface,
        12,
        TextBrush);

      Point topLeftPosition = new Point(
        left + 8,
        top + 4);

      drawingContext.DrawText(
        formattedText, 
        topLeftPosition);
    }
  }
}