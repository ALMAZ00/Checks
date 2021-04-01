using CheckLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CheckGame
{
    public class GameMover
    {
        private Coordinate coordinateFrom, coordinateTo;
        public Game Game { set; private get; }
        public GameMover(Game newGame)
        {
            Game = newGame;
        }
        public (Coordinate coordinateFrom, Coordinate coordinateTo) GetCoordinates(object sender, MouseButtonEventArgs e)
        {
            UIElement clickedUI = e.Source as UIElement;

            if (clickedUI != null)
            {
                SetCoordinates(clickedUI);
            }

            return (coordinateFrom, coordinateTo);
        }
        public bool OneMove(Coordinate coordinateFrom, Coordinate coordinateTo)
        {
            bool isGameOver = false;

            if (coordinateFrom != null && coordinateTo != null)
            {
                isGameOver = Game.OneMove(coordinateFrom, coordinateTo);
                SetNullForCoordinates();
            }

            return isGameOver;
        }
        private (Coordinate coordinateFrom, Coordinate coordinateTo) SetCoordinates(UIElement clickedUI)
        {
            int column = Grid.GetColumn(clickedUI);
            int row = Grid.GetRow(clickedUI);

            if (coordinateFrom == null)
            {
                coordinateFrom = new Coordinate(column, row);
            }
            else if (coordinateTo == null)
            {
                coordinateTo = new Coordinate(column, row);
            }

            return (coordinateFrom, coordinateTo);
        }
        private void SetNullForCoordinates()
        {
            coordinateFrom = null;
            coordinateTo = null;
        }
    }
}
