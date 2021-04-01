using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CheckGame
{
    [Serializable]
    class DrawingEmptyFigure : DrawingFigure
    {

        public DrawingEmptyFigure(int xindex, int yindex)
        {
            UIEl = GetEmptyUIElement();
            XIndex = xindex;
            YIndex = yindex;
        }
        private UIElement GetEmptyUIElement()
        {
            Label label = new Label();
            SetUIElementSizeAndAligment(label);
            return label;
        }
    }
}
