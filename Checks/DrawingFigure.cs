using CheckLibrary;
using System.Drawing;
using System.Windows.Controls;

namespace Checks
{
    abstract class DrawingFigure
    {
        protected Bitmap ImageBit;
        public int XIndex { get; protected set; }
        public int YIndex { get; protected set; }
        public abstract void DrawFigure(Grid grid);
    }
}
