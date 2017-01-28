using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

            createBoard(board, BoardThickness);
            WholeBoardUpdate(board.LogicBoard);

            KeyEvents = new Dictionary<Key, KeyPressed>();
            KeyEvents.Add(Key.R, RPressed);
            KeyEvents.Add(Key.Escape, EscPressed);
        }

        private void createBoard(Board board, int thick = 30)
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

            tile.MouseDown += this.tile_MouseDown;

            return tile;
        }

        public void WholeBoardUpdate(int[,] logicBoard)
        {
            createBoard(board, BoardThickness);
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
                            rect.Fill = Brushes.White;
                            break;
                        case -1:
                            rect.Fill = Brushes.Black;
                            break;
                        default:
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

            if (isWhite)
            {
                rect.Fill = Brushes.White;
            }
            else
            {
                rect.Fill = Brushes.Black;
            }
            
        }

        private void tile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle tile = (Rectangle) sender;
            int x = Grid.GetColumn(tile);
            int y = Grid.GetRow(tile);
            Debug.WriteLine(x + " : " + y);

            board.TileClicked(x, y);


            //placePawn(x, y, TileState.White, tile);
        }

        /*
        private void placePawn(int x, int y, TileState c, Rectangle tile)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("oreo.png", UriKind.Relative);
            b.EndInit();

            Image image = new Image();
            tile.Fill = new ImageBrush(b);            

        }
        */
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyEvents.ContainsKey(e.Key))
            {
                KeyEvents[e.Key](sender, e);
            }
        }

        private void RPressed(object sender, KeyEventArgs e)
        {
            board.Reset();
            WholeBoardUpdate(board.LogicBoard);
        }

        private void EscPressed(object sender, KeyEventArgs e)
        {
            Close();
        }
        
    }
}
