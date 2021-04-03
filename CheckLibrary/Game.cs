using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckLibrary
{
    [Serializable]
    public class Game
    {
        internal Table Table { get; private set; }
        public WhoGo WhoGo { get; private set; }
        private ITableDrawer TableDrawer;
        private IDrawingFigureFactory factory;
        public Game(ITableDrawer drawer, IDrawingFigureFactory factory)
        {            
            CreateTable();
            SetFactory(factory);
            SetTableDrawer(drawer);            
        }

        private void SetFactory(IDrawingFigureFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException();
            }

            this.factory = factory;
        }
        private void CreateTable()
        {
            Table = new Table();
            WhoGo = WhoGo.White;
        }
        public void SetTableDrawer(ITableDrawer drawer)
        {
            SetTableDrawerFrom(drawer);
            TableDrawer.DrawFieldFromTable();
        }

        private void SetTableDrawerFrom(ITableDrawer drawer)
        {
            if (drawer == null)
            {
                throw new ArgumentNullException();
            }
            TableDrawer = drawer;
            TableDrawer.SetGame(this);
        }
        public List<IDrawingFigure> GetTableDrawingFigures()
        {
            List<ICell> cells = Table.GetAllCells();
            List<IDrawingFigure> figures = new List<IDrawingFigure>();

            foreach (ICell cell in cells)
            {
                figures.Add(cell.Figure.GetDrawingFigure(factory));
            }

            return figures;
        }
        public bool OneMove(Coordinate coordinate1, Coordinate coordinate2)
        {
            if (coordinate1 != null && coordinate2 != null)
            {
                OnRightMove(coordinate1, coordinate2);
            }
            return IsGameOver();
        }
        private void OnRightMove(Coordinate coordinate1, Coordinate coordinate2)
        {
            GameSaver.SavePredTable(this);

            Table.CompleteGo(coordinate1, WhoGo);
            bool wasMove = Table.CompleteGo(coordinate2, WhoGo);

            OnMoveEnding(wasMove);
        }

        private void OnMoveEnding(bool wasMove)
        {
            TableDrawer.DrawFieldFromTable();

            if (wasMove)
            {
                WhoGoExchange();
            }
        }

        private void WhoGoExchange()
        {
            if (WhoGo == WhoGo.White)
            {
                WhoGo = WhoGo.Black;
            }
            else
            {
                WhoGo = WhoGo.White;
            }
        }
        private bool IsGameOver()
        {
            return (Table.CountOfBlack == 0 || Table.CountOfWhite == 0);
        }
        public WinColor WhoWin()
        {
            if (IsGameOver())
            {
                if (Table.CountOfWhite == 0)
                {
                    return WinColor.Black;
                }
                else if (Table.CountOfBlack == 0)
                {
                    return WinColor.White;
                }
            }
            return WinColor.None;
        }
    }
}
