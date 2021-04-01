using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FigureColor = CheckLibrary.Color;

namespace CheckGame
{
    [Serializable]
    class DrawingKing : DrawingFigure
    {
        public DrawingKing(FigureColor color, int x, int y)
        {
            XIndex = x;
            YIndex = y;
            Color = color;

            UIEl = GetKingUIElement();
        }
        private UIElement GetKingUIElement()
        {
            Label label = new Label
            {
                Content = "К"
            };

            if (Color == FigureColor.WhiteColor)
            {
                label.Foreground = Brushes.White;
            }
            else
            {
                label.Foreground = Brushes.Black;
            }

            SetUIElementSizeAndAligment(label);
            return label;
        }
    }
}
