using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OverlapStackPanelDemo
{
    public class OverlapStackPanel : Panel
    {
        #region Dependency Properties

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(OverlapStackPanel), new PropertyMetadata((Orientation.Vertical)));
        public Orientation Orientation { get { return (Orientation) GetValue(OrientationProperty); } set { SetValue(OrientationProperty, (Enum) value); } }
        
        public static readonly DependencyProperty OverlapProperty = DependencyProperty.Register("Overlap", typeof(double), typeof(OverlapStackPanel), new PropertyMetadata(double.NaN));
        public double Overlap { get { return (double)GetValue(OverlapProperty); } set { SetValue(OverlapProperty, value); } }

        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register("Rotation", typeof(double), typeof(OverlapStackPanel), new PropertyMetadata(double.NaN));
        public double Rotation { get { return (double)GetValue(RotationProperty); } set { SetValue(RotationProperty, value); } }

        public static readonly DependencyProperty LeftOffsetProperty = DependencyProperty.Register("LeftOffset", typeof(double), typeof(OverlapStackPanel), new PropertyMetadata(double.NaN));
        public double LeftOffset { get { return (double)GetValue(LeftOffsetProperty); } set { SetValue(LeftOffsetProperty, value); } }

        public static readonly DependencyProperty UpperOffsetProperty = DependencyProperty.Register("UpperOffset", typeof (double), typeof (OverlapStackPanel), new PropertyMetadata(double.NaN));
        public double UpperOffset { get { return (double)GetValue(UpperOffsetProperty); } set { SetValue(UpperOffsetProperty, value); } }

        #endregion
        
        #region Overrides
        
        protected override Size MeasureOverride(Size availableSize)
        {
            var desiredSize = new Size();
            var childrenResolved = 0;

            if (Orientation == Orientation.Vertical)
                availableSize.Height = double.PositiveInfinity;
            else
                availableSize.Width = double.PositiveInfinity;

            foreach (UIElement child in Children.Where(child => child != null))
            {
                child.Measure(availableSize);

                if (Orientation == Orientation.Vertical)
                {
                    desiredSize.Width = LeftOffset + Math.Max(desiredSize.Width, child.DesiredSize.Width);
                    desiredSize.Height += UpperOffset + child.DesiredSize.Height - Overlap;
                }
                else
                {
                    desiredSize.Height = UpperOffset + Math.Max(desiredSize.Height, child.DesiredSize.Height);
                    desiredSize.Width += LeftOffset + child.DesiredSize.Width - Overlap;
                }
                childrenResolved++;
            }

            // take into account the first item doesn't overlap
            if (Orientation == Orientation.Vertical)
            {
                desiredSize.Height += Overlap;
            }
            else
            {
                desiredSize.Width += Overlap;
            }

            return desiredSize;
        }

        protected sealed override Size ArrangeOverride(Size arrangeSize)
        {
            int childrenResolved = 0;
            double itemX = 0;
            double itemY = 0;
            foreach (UIElement child in Children.Where(child => child != null))
            {
                double itemOverlap = (childrenResolved == 0) 
                    ? 0 
                    : Overlap;

                Rect targetRect;
                if (Orientation == Orientation.Vertical)
                {    
                    targetRect = new Rect
                                         {
                                             X = LeftOffset + (LeftOffset * childrenResolved),
                                             Y = itemY + UpperOffset + (UpperOffset * childrenResolved) - itemOverlap,
                                             Width = child.DesiredSize.Width,
                                             Height = child.DesiredSize.Height
                                         };

                    itemY += child.DesiredSize.Height - itemOverlap;
                }
                else
                {
                    targetRect = new Rect
                                        {
                                            X = itemX + LeftOffset + (LeftOffset * childrenResolved) - itemOverlap,
                                            Y = UpperOffset + (UpperOffset * childrenResolved),
                                            Width = child.DesiredSize.Width,
                                            Height = child.DesiredSize.Height
                                        };

                    itemX += child.DesiredSize.Width - itemOverlap;
                }
                child.Arrange(targetRect);

                var rotate = new RotateTransform {Angle = Rotation};
                child.RenderTransform = rotate;
                
                childrenResolved++;
            }
            return arrangeSize;
        }

        #endregion
        
        #region Constructor

        public OverlapStackPanel()
        {
        }

        #endregion
    }
}