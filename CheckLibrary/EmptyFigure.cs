using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{
    [Serializable]
    internal class EmptyFigure : Figure
    {
        public override bool IsNotEmpty()
        {
            return false;
        }
        public override bool CanCutDown(Cell cell2)
        {
            return false;
        }

        public override bool CanMove(Cell cell)
        {
            return false;
        }

        public override void CutDown(Cell cell)
        {

        }

        public override bool HaveEnemy()
        {
            return false;
        }

        public override void Move(Cell cell)
        {

        }
        public override IDrawingFigure GetDrawingFigure(IDrawingFigureFactory factory)
        {
            return factory.CreateDrawingEmptyFigure(Cell);
        }
    }
}
