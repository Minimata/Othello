using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Othello.Board;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Board b = new Board();

            b.gameBoard[3, 3] = TileState.White;
            b.gameBoard[4, 4] = TileState.White;
            b.gameBoard[3, 4] = TileState.Black;
            b.gameBoard[4, 3] = TileState.Black;

            b.ShowInConsole();

        }
    }
}
