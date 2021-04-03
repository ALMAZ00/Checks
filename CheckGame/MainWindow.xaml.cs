using CheckLibrary;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CheckGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private TableDrawer tableDrawer;
        static private ManeGridDrawer maneGridDrawer;
        static private Game game;
        static private GameMover gameMover;
        static private bool IsGameOver;
        static private DrawingFigureFactory Factory;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateNewGame()
        {
            tableDrawer = new TableDrawer(gridTable);
            Factory = new DrawingFigureFactory();
            game = new Game(tableDrawer, Factory);
            gameMover = new GameMover(game);
            maneGridDrawer = new ManeGridDrawer(maneGrid, game);            
        }
        private void OnGameCreatingButtonClick(object sender, RoutedEventArgs e)
        {
            CreateNewGame();
            maneGridDrawer.SetTextBlocksText();
        }
        private void OnFigureTableMouseDown(object sender, MouseButtonEventArgs e)
        {
            tableDrawer.OnUIElementClick(sender, e);

            (Coordinate coordinateFrom, Coordinate coordinateTo) = gameMover.GetCoordinates(sender, e);
            IsGameOver = gameMover.OneMove(coordinateFrom, coordinateTo);

            maneGridDrawer.SetTextBlocksText();

            predMoveButton.IsEnabled = true;


            if (IsGameOver)
            {
                maneGridDrawer.OnGameOver();
            }
        }
        private void OnSaveGameButtonClick(object sender, RoutedEventArgs e)
        {
            if (game != null)
            {
                GameSaver.SaveGame(game);
            }
        }

        private void OnLoadGameButtonClick(object sender, RoutedEventArgs e)
        {
            game = GameSaver.LoadGame();
            OnGameCreating(game);
        }

        private void OnBackMoveButtonClick(object sender, RoutedEventArgs e)
        {
            game = GameSaver.BackMove();
            OnGameCreating(game);
        }
        private void OnGameCreating(Game game)
        {
            game.SetTableDrawer(tableDrawer);
            gameMover.Game = game;

            maneGridDrawer.SetTextBlocksText();
            predMoveButton.IsEnabled = false;
        }
        private void OnManeGridEscapeClick(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                tableDrawer.DrawFieldFromTable();
            }
        }
    }
}
