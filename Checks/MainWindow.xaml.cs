using System.Windows;
using System.Windows.Input;

namespace Checks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public GameSettings gameSettings;
        static public ITableDrawer tableDrawer;
        static public Game game;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void gridTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (tableDrawer as TableDrawer).SetMoveIndex(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameSettings = new GameSettings("game.txt");
            tableDrawer = new TableDrawer(gridTable);
            game = new Game(gameSettings, tableDrawer);
            tableDrawer.DrawFieldFromTable();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            game.SaveGame();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            game.LoadGame();
            tableDrawer.DrawFieldFromTable();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            game.BackMove();
            tableDrawer.DrawFieldFromTable();
        }
    }
}
