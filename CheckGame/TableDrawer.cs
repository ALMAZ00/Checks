using CheckLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows;
using System.Linq;

namespace CheckGame
{
    [Serializable]
    public class TableDrawer : ITableDrawer
    {
        public TableDrawer(Grid grid)
        {
            gridTable = grid;
        }
        private Game Game;
        [NonSerialized]
        private readonly Grid gridTable;

        public void DrawFieldFromTable()
        {
            gridTable.Dispatcher.Invoke(() => DrawAll());            
        }
        public void SetGame(Game game)
        {
            Game = game;
        }
        private void DrawAll()
        {
            FillGrid();
            FillByRectangles(gridTable);
            FillByFigures(gridTable);
        }

        private void FillGrid()
        {
            gridTable.Children.Clear();
            gridTable.ColumnDefinitions.Clear();
            gridTable.RowDefinitions.Clear();
            AddColumnsAndRows(gridTable);
        }

        private void AddColumnsAndRows(Grid gridTable)
        {
            for (int i = 0; i < 8; i++)
            {
                gridTable.ColumnDefinitions.Add(new ColumnDefinition());
                gridTable.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void FillByFigures(Grid grid)
        {
            List<IDrawingFigure> drawingFigures = Game.GetTableDrawingFigures();

            foreach (IDrawingFigure drawingFigure in drawingFigures)
            {
                (drawingFigure as DrawingFigure).DrawFigure(grid);
            }
        }
        private void FillByRectangles(Grid grid)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    AddRectangleTo(grid, i, j);
                }
            }
        }

        private void AddRectangleTo(Grid grid, int i, int j)
        {
            Rectangle rectangle = new Rectangle();

            if ((i + j) % 2 != 0)
            {
                rectangle.Fill = Brushes.Silver;
            }
            else
            {
                rectangle.Fill = Brushes.White;
            }

            Grid.SetColumn(rectangle, i);
            Grid.SetRow(rectangle, j);
            grid.Children.Add(rectangle);
        }
                
        public void OnUIElementClick(object sender, MouseButtonEventArgs e)
        {
            UIElement clickedUI = e.Source as UIElement;

            if (clickedUI == null)
            {
                return;
            }

            clickedUI.Opacity = 0.3;
        }        
    }
}