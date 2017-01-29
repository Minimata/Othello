using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Othello
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board board;
        public int BoardThickness { get; set; }
        private Grid DynamicGrid;

        private delegate void KeyPressed(object sender, KeyEventArgs e);

        private Dictionary<Key, KeyPressed> KeyEvents;

        public MainWindow()
        {
            InitializeComponent();
            BoardThickness = 30;
            board = new Board(parent: this);

            CreateBoard(board, BoardThickness);
            WholeBoardUpdate(board.LogicBoard);

            KeyEvents = new Dictionary<Key, KeyPressed>();
            KeyEvents.Add(Key.R, RPressed);
            KeyEvents.Add(Key.Escape, EscPressed);
            DataContext = board;
        }

        private void CreateBoard(Board board, int thick = 30)
        {
            // Create the Grid
            DynamicGrid = new Grid();
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

            tile.MouseDown += this.Tile_MouseDown;
            tile.MouseEnter += this.Tile_Enter;
            tile.MouseLeave += this.Tile_Leave;

            return tile;
        }

        public void WholeBoardUpdate(int[,] logicBoard)
        {
            CreateBoard(board, BoardThickness);
            for (int i = 0; i < logicBoard.GetLength(0); i++)
            {
                for (int j = 0; j < logicBoard.GetLength(1); j++)
                {
                    Rectangle rect = DynamicGrid.Children
                        .Cast<Rectangle>()
                        .First(e => Grid.GetColumn(e) == i && Grid.GetRow(e) == j);
                    switch (logicBoard[i, j])
                    {
                        case 0:
                            rect.Fill = Brushes.DarkGreen;
                            break;
                        case 1:
                            PlacePawn(rect, true);
                            break;
                        case -1:
                            PlacePawn(rect, false);
                            break;
                    }
                }
            }
            
        }

        public void UpdateBoard(Tuple<int, int> pair, bool isWhite)
        {
            Rectangle rect = DynamicGrid.Children
                .Cast<Rectangle>()
                .First(e => Grid.GetColumn(e) == pair.Item1 && Grid.GetRow(e) == pair.Item2);

            PlacePawn(rect, isWhite);
        }

        private void PlacePawn(Rectangle tile, bool isWhite)
        {
            BitmapImage b = new BitmapImage();

            b.BeginInit();
            if(isWhite) b.UriSource = new Uri("../../../image/cookie.png", UriKind.Relative);
            else b.UriSource = new Uri("../../../image/oreo.png", UriKind.Relative);
            b.EndInit();
            
            tile.Fill = new ImageBrush(b);
            DataContext = board;

            BitmapImage t = new BitmapImage();
            t.BeginInit();
            if (isWhite) t.UriSource = new Uri("../../../image/oreo.png", UriKind.Relative);
            else t.UriSource = new Uri("../../../image/cookie.png", UriKind.Relative);
            t.EndInit();

            imgTurn.Fill = new ImageBrush(t);
        }

        private void Tile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle tile = (Rectangle) sender;
            int x = Grid.GetColumn(tile);
            int y = Grid.GetRow(tile);

            board.TileClicked(x, y);
        }

        private void Tile_Enter(object sender, EventArgs e)
        {
            Rectangle tile = (Rectangle)sender;
            int x = Grid.GetColumn(tile);
            int y = Grid.GetRow(tile);

            if(board.isTilePlayable(x, y))
            {
                tile.Fill = Brushes.ForestGreen;
            }
        }

        private void Tile_Leave(object sender, EventArgs e)
        {
            Rectangle tile = (Rectangle)sender;
            int x = Grid.GetColumn(tile);
            int y = Grid.GetRow(tile);

            if (tile.Fill == Brushes.ForestGreen)
            {
                tile.Fill = Brushes.DarkGreen;
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyEvents.ContainsKey(e.Key))
            {
                KeyEvents[e.Key](sender, e);
            }
            
            Debug.Write(board.BlackScore);

        }

        private void RPressed(object sender = null, KeyEventArgs e = null)
        {
            board.Reset();
            WholeBoardUpdate(board.LogicBoard);
        }

        private void EscPressed(object sender = null, KeyEventArgs e = null)
        {
            Close();
        }

        private void RPressed(object sender, MouseButtonEventArgs e)
        {
            RPressed();
        }

        private void EscPressed(object sender, MouseButtonEventArgs e)
        {
            EscPressed();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
