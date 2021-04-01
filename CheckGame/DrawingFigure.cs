using CheckLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CheckGame
{
    [Serializable]
    abstract class DrawingFigure : IDrawingFigure
    {
        [NonSerialized]
        protected UIElement UIEl;
        protected Color Color;
        public int XIndex { get; set; }
        public int YIndex { get; set; }

        protected void SetUIElementSizeAndAligment(Label label)
        {
            label.FontSize = 30;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
        }
        public void DrawFigure(Grid grid)
        {
            Grid.SetColumn(UIEl, XIndex);
            Grid.SetRow(UIEl, YIndex);
            grid.Children.Add(UIEl);
        }
    }
}
