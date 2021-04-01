
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{

    [Serializable]
    internal abstract class Figure : IMove, IEquatable<Figure>, IFigure
    {
        public Cell Cell { get; set; }
        public Color Color { get; set; }
        public Table FigureTable { get; protected set; }

        public abstract void Move(Cell cell);
        public abstract void CutDown(Cell cell);
        public abstract bool HaveEnemy();
        public abstract bool CanMove(Cell cell);
        public abstract bool CanCutDown(Cell cell2);
        public abstract IDrawingFigure GetDrawingFigure(IDrawingFigureFactory factory);

        public bool IsDifferentColor(IFigure figure)
        {
            bool isDiffernetColor = false;

            if ((Color == Color.WhiteColor && figure.Color == Color.BlackColor)
                || Color == Color.BlackColor && figure.Color == Color.WhiteColor)
            {
                isDiffernetColor = true;
            }
            return isDiffernetColor;
        }
        public bool IsWhiteColor()
        {
            return Color == Color.WhiteColor;
        }
        public bool IsBlackColor()
        {
            return Color == Color.BlackColor;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Figure);
        }

        public abstract bool IsNotEmpty();

        public bool Equals(Figure other)
        {
            return other != null &&
                   GetType() == other.GetType() &&
                   Color == other.Color;
        }

        public override int GetHashCode()
        {
            int hashCode = -223045792;
            hashCode = hashCode * -1521134295 + EqualityComparer<Cell>.Default.GetHashCode(Cell);
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Table>.Default.GetHashCode(FigureTable);
            return hashCode;
        }
    }
}
