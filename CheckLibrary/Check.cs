using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{
    [Serializable]
    internal class Check : Figure
    {
        public Check(Table table, Color color)
        {
            CreateCheckFrom(table, color);
        }

        private void CreateCheckFrom(Table table, Color color)
        {
            if (table == null)
            {
                throw new ArgumentNullException();
            }

            Color = color;
            FigureTable = table;
        }
        
        public override void Move(Cell toCell)
        {
            toCell.Figure = new Check(FigureTable, Color);
            Cell.ClearFigure();
            Cell = toCell;
        }
        public override void CutDown(Cell toCell)
        {
            GetCellBetween(Cell, toCell).ClearFigure();
            Move(toCell);
        }

        private Cell GetCellBetween(Cell fromCell, Cell toCell)
        {
            (int xIndex, int yIndex) = GetIndexesBetween(fromCell, toCell);

            return FigureTable[xIndex, yIndex];
        }

        private (int xIndex, int yIndex) GetIndexesBetween(Cell fromCell, Cell toCell)
        {
            (int xIndex, int yIndex) indexes;

            indexes.xIndex = (fromCell.Xindex + toCell.Xindex) / 2;
            indexes.yIndex = (fromCell.Yindex + toCell.Yindex) / 2;

            return indexes;
        }

        public override bool CanMove(Cell cell)
        {
            if (IsCellStandNextTo(cell) && !cell.IsNotEmpty())
            {
                if (CanGoTowardsThe(cell))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanGoTowardsThe(Cell cell)
        {
            return (IsBlackColor() && Cell.Yindex - cell.Yindex == -1)
                    || (IsWhiteColor() && Cell.Yindex - cell.Yindex == 1);
        }

        private bool IsCellStandNextTo(Cell cell)
        {
            return Math.Abs(Cell.Xindex - cell.Xindex) == 1;
        }

        public override bool CanCutDown(Cell cell)
        {
            bool canCutDown = false;

            if (IsCellStandsThroughOneTo(cell) && !cell.IsNotEmpty())
            {
                canCutDown = IsDifferentColorOf(cell);
            }

            return canCutDown;
        }

        private bool IsCellStandsThroughOneTo(Cell cell)
        {
            return Math.Abs(Cell.Xindex - cell.Xindex) == 2
                         && Math.Abs(Cell.Yindex - cell.Yindex) == 2;
        }

        private bool IsDifferentColorOf(Cell cell)
        {
            bool isDifferentColor = false;
            if (GetCellBetween(Cell, cell).IsNotEmpty())
            {
                if (IsDifferentColor(GetCellBetween(Cell, cell).Figure))
                {
                    isDifferentColor = true;
                }
            }

            return isDifferentColor;
        }
        public override bool HaveEnemy()
        {
            bool haveEnemy = false;

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    haveEnemy = IsEnemyOn(i, j);
                    if (haveEnemy)
                    {
                        return haveEnemy;
                    }
                }
            }
            return haveEnemy;
        }
        private bool IsEnemyOn(int i, int j)
        {
            bool isEnemyOn = false;

            if (Cell.IsRightIdexs(Cell.Xindex + 2 * j, Cell.Yindex + 2 * i))
            {
                Cell enemy = FigureTable[Cell.Xindex + 2 * j, Cell.Yindex + 2 * i];
                if (enemy != null && CanCutDown(enemy))
                {
                    isEnemyOn = true;
                }
            }

            return isEnemyOn;
        }

        public override bool IsNotEmpty()
        {
            return true;
        }

        public override IDrawingFigure GetDrawingFigure(IDrawingFigureFactory factory)
        {
            return factory.CreateDrawingCheck(Cell);
        }
    }
}
