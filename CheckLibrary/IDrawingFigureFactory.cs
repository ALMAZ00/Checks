using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLibrary
{
    public interface IDrawingFigureFactory
    {
        public IDrawingFigure CreateDrawingCheck(ICell cell);
        public IDrawingFigure CreateDrawingKing(ICell cell);
        public IDrawingFigure CreateDrawingEmptyFigure(ICell cell);
    }
}
