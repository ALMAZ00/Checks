using CheckLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckGame
{
    [Serializable]
    class DrawingFigureFactory : IDrawingFigureFactory
    {
        public IDrawingFigure CreateDrawingCheck(ICell cell)
        {
            return new DrawingCheck(cell.Figure.Color, cell.Xindex, cell.Yindex);
        }

        public IDrawingFigure CreateDrawingEmptyFigure(ICell cell)
        {
            return new DrawingEmptyFigure(cell.Xindex, cell.Yindex);
        }

        public IDrawingFigure CreateDrawingKing(ICell cell)
        {
            return new DrawingKing(cell.Figure.Color, cell.Xindex, cell.Yindex);
        }
    }
}
