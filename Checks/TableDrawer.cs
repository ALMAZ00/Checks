using CheckLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Checks
{
    internal class TableDrawer : ITableDrawer
    {
        public TableDrawer(Grid grid)
        {
            gridTable = grid;
        }
        public Game Game
        {
            private get
            {
                return Game;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                Game = value;
            }
        }
        private readonly Grid gridTable;
        private List<DrawingFigure> cells;
        private Coordinate coordinateFrom, coordinateTo;

        public void DrawFieldFromTable()
        {
            gridTable.Children.Clear();
            //FillByRectangles(gridTable);
            cells = ViewModel.GetDrawingFigures(Game);
            FillByFigures(gridTable);
        }
        private void FillByFigures(Grid grid)
        {
            foreach (DrawingFigure drawingFigure in cells)
            {
                drawingFigure.DrawFigure(grid);
            }
        }

        private void FillByRectangles(Grid grid)
        {
            //Grid filling by rectandles
            for (int i = 0; i < 8; i++)
            {
                Rectangle rectangle;
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        rectangle = new Rectangle
                        {
                            Fill = Brushes.Black
                        };
                    }
                    else
                    {
                        rectangle = new Rectangle
                        {
                            Fill = Brushes.White
                        };
                    }
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    grid.Children.Add(rectangle);
                }
            }
        }
        public Coordinate[] GetMoveIndexes()
        {
            Coordinate[] coordinate = new Coordinate[2];
            if (coordinateFrom != null && coordinateTo != null)
            {
                coordinate[0] = new Coordinate(coordinateFrom.X, coordinateFrom.Y);
                coordinate[1] = new Coordinate(coordinateTo.X, coordinateTo.Y);
                SetNullForCoordinates();
            }
            return coordinate;
        }
        private void SetNullForCoordinates()
        {
            coordinateFrom = null;
            coordinateTo = null;
        }
        public void SetMoveIndex(object sender, MouseButtonEventArgs e)
        {
            var clickedImg = (sender as Image);
            int column = Grid.GetColumn(clickedImg);
            int row = Grid.GetRow(clickedImg);

            if (coordinateFrom == null)
            {
                coordinateFrom = new Coordinate(column, row);
            }
            else if (coordinateTo == null)
            {
                coordinateTo = new Coordinate(column, row);
            }
        }
    }
}