using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckLibrary
{
    [Serializable]
    internal class King : Figure
    {
        public King(Table table, Color color)
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
        public override bool IsNotEmpty()
        {
            return true;
        }
        public override bool CanMove(Cell cell)
        {
            bool canMove = false;

            if (IsCellOnSameDiagonalWith(cell) && !cell.IsNotEmpty())
            {
                canMove = IsCleanPathTo(cell);
            }

            return canMove;
        }

        private bool IsCellOnSameDiagonalWith(Cell cell)
        {
            return Math.Abs(Cell.Xindex - cell.Xindex) == Math.Abs(cell.Yindex - Cell.Yindex);
        }

        private bool IsCleanPathTo(Cell cell)
        {
            (int coefX, int coefY) = DirectionDeterminant.GetCoefficientsTo(Cell, cell);

            for (int i = 1; i <= Math.Abs(Cell.Xindex - cell.Xindex); i++)
            {
                if (GetCellOn(coefX * i, coefY * i).IsNotEmpty())
                {
                    return false;
                }
            }

            return true;
        }

        private Cell GetCellOn(int coefX, int coefY)
        {
            return FigureTable[Cell.Xindex + coefX, Cell.Yindex + coefY];
        }

        public override bool CanCutDown(Cell cell)
        {
            bool canCutDown = false;

            if (IsCellOnSameDiagonalWith(cell) && !cell.IsNotEmpty())
            {
                canCutDown = CanCutDownOnPathTo(cell);
            }

            return canCutDown;
        }
        private bool CanCutDownOnPathTo(Cell cell)
        {
            bool canCutDownOnPathTo = false;
            (int coefX, int coefY) = DirectionDeterminant.GetCoefficientsTo(Cell, cell);
            Cell cellBetween = FigureTable[cell.Xindex - coefX, cell.Yindex - coefY];

            if (cellBetween.IsNotEmpty() && IsDifferentColor(cellBetween.Figure))
            {
                canCutDownOnPathTo = IsRightWayForCutDown(cell, coefX, coefY);
            }

            return canCutDownOnPathTo;
        }
        private bool IsRightWayForCutDown(Cell cell, int coefX, int coefY)
        {
            bool isRightWayForCutDown = false;

            for (int i = 1; i < Math.Abs(Cell.Xindex - cell.Xindex); i++)
            {
                Cell cellBetween = GetCellOn(coefX * i, coefY * i);
                Cell cellNextForBetween = GetCellOn(coefX * (i + 1), coefY * (i + 1));

                if (cellBetween.IsNotEmpty())
                {
                    if (IsDifferentColor(cellBetween.Figure) == false
                        || cellNextForBetween.IsNotEmpty() == false)
                    {
                        isRightWayForCutDown = true;
                    }
                }
            }

            return isRightWayForCutDown;
        }        
        public override void Move(Cell toCell)
        {
            toCell.Figure = new Check(FigureTable, Color);
            Cell.ClearFigure();
            Cell = toCell;
        }
        public override void CutDown(Cell toCell)
        {
            ClearFelledCells(toCell);
            Move(toCell);
        }
        private void ClearFelledCells(Cell cell)
        {
            for (int i = 1; i < Math.Abs(Cell.Xindex - cell.Xindex) + 1; i++)
            {
                ClearCell(cell, i);
            }
        }
        private void ClearCell(Cell endingCell, int distanceFromStartCell)
        {
            (int coefX, int coefY) = DirectionDeterminant.GetCoefficientsTo(Cell, endingCell);
            Cell felledCell = GetCellOn(distanceFromStartCell * coefX, distanceFromStartCell * coefY);

            if (felledCell.IsNotEmpty())
            {
                felledCell.ClearFigure();
            }
        }
        public override bool HaveEnemy()
        {
            bool haveEnemy = false;

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    haveEnemy = HaveEnemyOnDiagonal(i, j);
                    if (haveEnemy)
                    {
                        return haveEnemy;
                    }
                }
            }
            return haveEnemy;
        }
        private bool HaveEnemyOnDiagonal(int xDirection, int yDirection)
        {
            bool haveEnemyOnDiagonal = false;

            for (int step = 0; step < FigureTable.MaxIndex; step++)
            {
                haveEnemyOnDiagonal = IsEnemyOn(xDirection, yDirection, step);

                if (haveEnemyOnDiagonal)
                {
                    return haveEnemyOnDiagonal;
                }
            }

            return haveEnemyOnDiagonal;
        }
        private bool IsEnemyOn(int xDirection, int yDirection, int step)
        {
            bool isEnemyOn = false;

            if (Cell.IsRightIdexs
                (Cell.Xindex + xDirection * step, 
                Cell.Yindex + yDirection * step))
            {
                Cell enemy = FigureTable[
                    Cell.Xindex + xDirection * step, 
                    Cell.Yindex + yDirection * step];

                if (enemy != null && CanCutDown(enemy))
                {
                    isEnemyOn = true;
                }
            }

            return isEnemyOn;
        }
        public override IDrawingFigure GetDrawingFigure(IDrawingFigureFactory factory)
        {
            return factory.CreateDrawingKing(Cell);
        }
    }
}
