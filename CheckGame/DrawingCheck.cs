using System;
using System.Windows.Controls;
using FigureColor = CheckLibrary.Color;
using System.Windows;
using System.Windows.Media;

namespace CheckGame
{
    [Serializable]
    class DrawingCheck : DrawingFigure
    {
        public DrawingCheck(FigureColor color, int x, int y)
        {
            XIndex = x;
            YIndex = y;
            Color = color;

            UIEl = GetCheckUIElement();
        }

        private UIElement GetCheckUIElement()
        {
            Label label = new Label
            {
                Content = "Ш"
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
