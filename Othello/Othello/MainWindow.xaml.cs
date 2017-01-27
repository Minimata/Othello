using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Board board;
        public int BoardThickness { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();

            BoardThickness = 30;
            createBoard(board, BoardThickness);
        }

        private void createBoard(Board board, int thick = 30)
        {
            // Create the Grid
            Grid DynamicGrid = new Grid();
            DynamicGrid.Margin = new Thickness(thick, thick, thick, thick);
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
            DynamicGrid.ShowGridLines = false;

            for (int i = 0; i < board.NumTiles; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                DynamicGrid.ColumnDefinitions.Add(col);

                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                DynamicGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < board.NumTiles; i++)
            {
                for (int j = 0; j < board.NumTiles; j++)
                {
                    UIElement tile = CreateTile(i, j);
                    DynamicGrid.Children.Add(tile);

                }
            }

            // Display grid into a Window
            plate.Children.Add(DynamicGrid);

        }

        private UIElement CreateTile(int x, int y)
        {
            Rectangle tile = new Rectangle();

            tile.Stroke = Brushes.Black;
            tile.Fill = Brushes.DarkGreen;
            Grid.SetColumn(tile, x);
            Grid.SetRow(tile, y);

            tile.MouseDown += this.tile_MouseDown;

            return tile;
        }

        private void tile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle tile = (Rectangle) sender;
            int x = Grid.GetColumn(tile);
            int y = Grid.GetRow(tile);
            Debug.WriteLine(x + " : " + y);


            placePawn(x, y, TileState.White, tile);
        }

        private void placePawn(int x, int y, TileState c, Rectangle tile)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("oreo.png", UriKind.Relative);
            b.EndInit();

            Image image = new Image();
            tile.Fill = new ImageBrush(b);
        }
    }
}
