using CheckLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checks
{
    class ViewModel
    {
        public static List<DrawingFigure> GetDrawingFigures(Game game)
        {
            List<DrawingFigure> drawingFigures = new List<DrawingFigure>();
            List<ICell> cells = game.GetTableCells();
            foreach (ICell cell in cells)
            {
                if (cell.Figure is ICheck)
                {
                    drawingFigures.Add(new DrawingCheck(cell.Figure.Color, cell.Xindex, cell.Yindex));
                }
                if (cell.Figure is IKing)
                {
                    drawingFigures.Add(new DrawingKing(cell.Figure.Color, cell.Xindex, cell.Yindex));
                }
                if (cell.Figure is IEmptyFigure)
                {
                    drawingFigures.Add(new DrawingEmptyFigure());
                }
            }

            return drawingFigures;
        }
    }
}
