using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CheckLibrary.Test")]

namespace CheckLibrary
{
    [Serializable]
    internal class Table : IEquatable<Table>
    {
        private Cell[,] cells;
        private const int columnsCount = 8;
        private const int rowsCount = 8;
        public int MinIndex => 0;
        public int MaxIndex => 7;
        public Cell From { get; set; }
        public Cell To { get; set; }
        public Table()
        {
            SetNullForFromTo();
            cells = new Cell[columnsCount, rowsCount];
            CreateTable();
        }
        public Cell this[int i, int j]
        {
            get
            {
                if (i < MinIndex || i > MaxIndex || j < MinIndex || j > MaxIndex)
                {
                    throw new IndexOutOfRangeException();
                }
                foreach (var item in cells)
                {
                    if (item != null && item.Xindex == i && item.Yindex == j)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
        private void Refresh()
        {
            SetNullForFromTo();
            cells = new Cell[columnsCount, rowsCount];
        }
        private void SetNullForFromTo()
        {
            From = null;
            To = null;
        }
        internal List<ICell> GetAllCells()
        {
            List<ICell> allCells = new List<ICell>();

            for (int i = MinIndex; i <= MaxIndex; i++)
            {
                for (int j = MinIndex; j <= MaxIndex; j++)
                {
                    allCells.Add(this[i, j]);
                }
            }

            return allCells;
        }
        public int CountOfWhite
        {
            get
            {
                int rezult = 0;

                for (int i = MinIndex; i <= MaxIndex; i++)
                {
                    for (int j = MinIndex; j <= MaxIndex; j++)
                    {
                        rezult = NewWhiteFigureCount(rezult, i, j);
                    }
                }

                return rezult;
            }
        }
        public int CountOfBlack
        {
            get 
            {
                int rezult = 0;

                for (int i = MinIndex; i <= MaxIndex; i++)
                {
                    for (int j = MinIndex; j <= MaxIndex; j++)
                    {
                        rezult = NewBlackFigureCount(rezult, i, j);
                    }
                }

                return rezult;
            }
        }

        private int NewWhiteFigureCount(int rezult, int i, int j)
        {
            if (cells[j, i].IsNotEmpty())
            {
                if (cells[j, i].Figure.IsWhiteColor())
                {
                    rezult++;
                }
            }

            return rezult;
        }
        private int NewBlackFigureCount(int rezult, int i, int j)
        {
            if (cells[j, i].IsNotEmpty())
            {
                if (cells[j, i].Figure.IsBlackColor())
                {
                    rezult++;
                }
            }

            return rezult;
        }

        private void CreateTable()
        {
            Refresh();
            CreateAllFigures();
        }

        private void CreateAllFigures()
        {
            for (int i = MinIndex; i <= MaxIndex; i++)
            {
                for (int j = MinIndex; j <= MaxIndex; j++)
                {
                    CreateFigureOn(i, j);
                }
            }
        }

        private void CreateFigureOn(int xIndex, int yIndex)
        {
            Cell newCell;
            Figure newFigure;
            if (IsItIndexesOfFigureOnStartingBoard(xIndex, yIndex))
            {
                newFigure = CreateNotNonEmptyFigure(xIndex);
            }
            else
            {
                newFigure = new EmptyFigure();
            }

            newCell = new Cell(yIndex, xIndex, newFigure);
            cells[yIndex, xIndex] = newCell;
        }

        private bool IsItIndexesOfFigureOnStartingBoard(int xIndex, int yIndex)
        {
            return (xIndex + yIndex) % 2 != 0 && (xIndex <= 2 || xIndex >= 5);
        }

        private Figure CreateNotNonEmptyFigure(int xIndex)
        {
            Figure newFigure;

            if (xIndex <= 2)
            {
                newFigure = new Check(this, Color.BlackColor);
            }
            else
            {
                newFigure = new Check(this, Color.WhiteColor);
            }

            return newFigure;
        }

        private bool ShouldCutDown(WhoGo whoGo)
        {
            bool shouldCutDown = false;

            foreach (var cell in cells)
            {
                shouldCutDown = HaveEnemyFigureOn(cell, whoGo);

                if (shouldCutDown)
                {
                    return shouldCutDown;
                }
            }
            
            return shouldCutDown;
        }
        private bool HaveEnemyFigureOn(Cell cell, WhoGo whoGo)
        {
            bool haveEnemy = false;

            if (cell.IsNotEmpty())
            {
                var figure = cell.Figure as Figure;

                haveEnemy = HaveEnemyFigure(figure, whoGo);
            }

            return haveEnemy;
        }
        private bool HaveEnemyFigure(Figure figure, WhoGo whoGo)
        {
            bool haveEmeny = false;

            if ((whoGo == WhoGo.White && figure.IsWhiteColor()) ||
                (whoGo == WhoGo.Black && figure.IsBlackColor()))
            {
                haveEmeny = figure.HaveEnemy();
            }

            return haveEmeny;
        }
        public bool CompleteGo(Coordinate coordinate, WhoGo whoGo)
        {
            bool isCompleteGo = GameMove(this[coordinate.X, coordinate.Y], whoGo);

            if (isCompleteGo == true)
            {
                AddAppearedKing();
                SetNullForFromTo();
            }

            return isCompleteGo;
        }
        private bool GameMove(Cell selCell, WhoGo whoGo)
        {
            bool wasGameMove = false;

            if (From == null)
            {
                OnEmptyFromCell(selCell);
            }
            else
            {
                wasGameMove = OnFromCellIsNotEmpty(selCell, whoGo);
            }

            return wasGameMove;
        }

        private void OnEmptyFromCell(Cell selCell)
        {
            if (selCell.IsNotEmpty())
            {
                From = selCell;
            }
        }

        private bool OnFromCellIsNotEmpty(Cell selCell, WhoGo whoGo)
        {
            bool wasAnyGo = false;

            if (AdvisesWhoGo(From, whoGo))
            {
                To = selCell;
                wasAnyGo = BodyOfGo(selCell, whoGo);
            }
            else
            {
                SetNullForFromTo();
            }

            return wasAnyGo;
        }

        private bool AdvisesWhoGo(Cell cell, WhoGo whoGo)
        {
            return (whoGo == WhoGo.White && cell.Figure.Color == Color.WhiteColor)
                || (whoGo == WhoGo.Black && cell.Figure.Color == Color.BlackColor);
        }
        private bool BodyOfGo(Cell selCell, WhoGo whoGo)
        {
            bool wasCutDown = false;
            bool wasMove = false;

            if (Cell.IsRightMove(From, To))
            {

                if (ShouldCutDown(whoGo))
                {
                    wasCutDown = OnShouldCutDown(selCell);
                }
                else
                {
                    wasMove = OnTheMove(selCell);
                }
            }

            return IsAnyGoEnded(selCell, wasCutDown, wasMove);
        }

        private bool IsAnyGoEnded(Cell selCell, bool wasCutDown, bool wasMove)
        {
            bool isAnyGoEnded = false;

            if (wasMove == true || wasCutDown == true)
            {
                isAnyGoEnded = true;
                SetNullForFromTo();
            }
            if (wasCutDown == true && (selCell.Figure as Figure).HaveEnemy())
            {
                isAnyGoEnded = false;
            }
            if (wasCutDown == false && wasMove == false)
            {
                SetNullForFromTo();
            }

            return isAnyGoEnded;
        }

        private bool OnTheMove(Cell selCell)
        {
            bool wasMove = false;

            if ((From.Figure as Figure).CanMove(selCell))
            {
                wasMove = true;
                (this[From.Xindex, From.Yindex].Figure as Figure).Move(selCell);
            }

            return wasMove;
        }

        private bool OnShouldCutDown(Cell selCell)
        {
            bool wasCutDown = false;

            if ((From.Figure as Figure).CanCutDown(selCell))
            {
                wasCutDown = true;
                (this[From.Xindex, From.Yindex].Figure as Figure).CutDown(selCell);
            }

            return wasCutDown;
        }

        private void AddAppearedKing()
        {
            foreach (Cell cell in cells)
            {
                SetKingFigureOn(cell);
            }
        }

        private void SetKingFigureOn(Cell item)
        {
            if (item.Figure is Check)
            {
                if ((item.Figure.Color == Color.WhiteColor && item.Yindex == 0) ||
                     item.Figure.Color == Color.BlackColor && item.Yindex == 7)
                {
                    DoKing(item);
                }
            }
        }

        private void DoKing(Cell cell)
        {
            cell = new Cell(cell.Xindex, cell.Yindex, new King(this, cell.Figure.Color));
            AssignCellToArray(cell);
        }

        private void AssignCellToArray(Cell cell)
        {
            for (int i = MinIndex; i <= MaxIndex; i++)
            {
                for (int j = MinIndex; j <= MaxIndex; j++)
                {
                    if (Cell.IsItSameCell(cells[i, j], cell))
                    {
                        cells[i, j] = cell;
                    }
                }
            }
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Table);
        }

        public bool Equals(Table other)
        {
            return other != null &&
                   EqualityComparer<Cell[,]>.Default.Equals(cells, other.cells) &&
                   EqualityComparer<Cell>.Default.Equals(From, other.From) &&
                   EqualityComparer<Cell>.Default.Equals(To, other.To);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(cells, From, To);
        }
    }
}
