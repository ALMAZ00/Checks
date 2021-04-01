using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{

    [Serializable]
    internal class Cell : IEquatable<Cell>, ICell
    {
        private Figure figure;

        public int Xindex { get; private set; }
        public int Yindex { get; private set; }
        public IFigure Figure
        {
            get 
            {
                return figure;
            } 
            set
            {
                if (value == null)
                {
                    figure = new EmptyFigure();
                }
                else
                {
                    figure = (Figure)value;
                }
                figure.Cell = this;
            }
        }
        public Cell(int x, int y, Figure figure = null)
        {
            CheckArguments(x, y);
            SetFigureAndIndexes(x, y, figure);
        }
        private void CheckArguments(int x, int y)
        {
            if (IsRightIdexs(x, y) == false)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        private void SetFigureAndIndexes(int x, int y, Figure figure)
        {
            Figure = figure;
            figure.Cell = this;
            Xindex = x;
            Yindex = y;
        }
        public void ClearFigure()
        {
            Figure = new EmptyFigure();
        }
        public bool IsNotEmpty()
        {
            return figure.IsNotEmpty();
        }
        public static bool IsRightIdexs(int X, int Y)
        {
            return (X > -1 && X < 8 && Y > -1 && Y < 8);
        }
        public static bool IsItSameCell(Cell firstCell, Cell secondCell)
        {
            return firstCell.Xindex == secondCell.Xindex && firstCell.Yindex == secondCell.Yindex;
        }

        public static bool IsRightMove(Cell from, Cell to)
        {
            return from.IsNotEmpty() && !to.IsNotEmpty();
        }
        public bool Equals(Cell other)
        {
            return other != null &&
                   EqualityComparer<Figure>.Default.Equals(figure, other.figure) &&
                   Xindex == other.Xindex &&
                   Yindex == other.Yindex &&
                   EqualityComparer<Figure>.Default.Equals(figure, other.figure);
        }
    }
}
