using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Documents;
using OthelloConsole;

namespace Othello
{
    class Board : IPlayable
    {
        public enum TileState
        {
            Empty = 0,
            Black = -1,
            White = 1
        };

        public struct Vector2i
        {
            public Vector2i(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;

            public static Vector2i operator +(Vector2i v1, Vector2i v2)
            {
                return new Vector2i(v1.x + v2.x, v1.y + v2.y);
            }

            public bool IsValid(int size)
            {
                bool response = true;

                response &= (this.x >= 0 && this.x < size);
                response &= (this.y >= 0 && this.y < size);

                return response;
            }
        }

        private List<Vector2i> directions;
        public int[,] LogicBoard { get; set; }
        private MainWindow main;
        private bool isWhite;
        public int WhiteScore { get; set; }
       public int BlackScore { get; set; }
        public int NumTiles { get; }

        public Board(int numTiles = 8, MainWindow parent = null)
        {
            main = parent;
            NumTiles = numTiles;
            Reset();

            directions = new List<Vector2i>();
            directions.Add(new Vector2i(0, 1));
            directions.Add(new Vector2i(0, -1));
            directions.Add(new Vector2i(1, 0));
            directions.Add(new Vector2i(1, 1));
            directions.Add(new Vector2i(1, -1));
            directions.Add(new Vector2i(-1, 0));
            directions.Add(new Vector2i(-1, 1));
            directions.Add(new Vector2i(-1, -1));
        }

        public void Reset()
        {
            isWhite = false;

            LogicBoard = new int[NumTiles, NumTiles];
            int half = NumTiles/2;
            LogicBoard[half - 1, half - 1] = (int) TileState.White;
            LogicBoard[half, half] = (int) TileState.White;
            LogicBoard[half - 1, half] = (int) TileState.Black;
            LogicBoard[half, half - 1] = (int) TileState.Black;
            BlackScore = WhiteScore = 2;
        }

        public void TileClicked(int column, int line)
        {
            if (playMove(column, line, isWhite))
            {
                this.isWhite = !isWhite;
            }
        }
        
        public bool isPlayable(int column, int line, bool isWhite)
        {
            if((LogicBoard[column, line] != 0)) return false; // Tile must be empty

            int color = (int) TileState.Black;
            if (isWhite) color = (int) TileState.White;
            Vector2i pos = new Vector2i(column, line);

            foreach (var dir in directions)
            {
                Vector2i sideTile = pos + dir;
                if (sideTile.IsValid(NumTiles) &&
                        LogicBoard[sideTile.x, sideTile.y] != color &&
                        LogicBoard[sideTile.x, sideTile.y] != 0)
                {
                    Vector2i tile = sideTile + dir;
                    while (tile.IsValid(NumTiles) && 
                        LogicBoard[tile.x, tile.y] != color &&
                        LogicBoard[tile.x, tile.y] != 0)
                    {
                        tile = tile + dir;
                    }
                    if(tile.IsValid(NumTiles) && LogicBoard[tile.x, tile.y] == color)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool playMove(int column, int line, bool isWhite)
        {
            //Check if the move can be done
            if (isPlayable(column, line, isWhite))
            {
                //Settling an integer for entering color in the logic state board.
                int color = (int)TileState.Black;
                if (isWhite) color = (int)TileState.White;

                //Settling the actual position for vectorial operations and a list of pawns to replace
                Vector2i pos = new Vector2i(column, line);
                List<Tuple<int, int>> pawnsToReplace = new List<Tuple<int, int>>();


                foreach (var dir in directions)
                {
                    List<Tuple<int, int>> tmp = new List<Tuple<int, int>>();
                    Vector2i tile = pos + dir;

                    //While we're in the board and we can find an opposite color pawn in a direction, we add positions to add a pawn to later, and we iterate until we reach 
                    //the end of the board or pawn of our color.
                    while (tile.IsValid(NumTiles) &&
                        LogicBoard[tile.x, tile.y] != color &&
                        LogicBoard[tile.x, tile.y] != 0)
                    {
                        tmp.Add(new Tuple<int, int>(tile.x, tile.y));
                        tile = tile + dir;
                    }

                    //If we actually ended up on a pawn of our color instead of finishing off the board, then it's a valid line of pawns to replace.
                    if (tile.IsValid(NumTiles) && LogicBoard[tile.x, tile.y] == color)
                    {
                        pawnsToReplace.Add(new Tuple<int, int>(column, line));
                        pawnsToReplace.AddRange(tmp);
                    }
                }

                //Keeping the scor in place
                if (isWhite)
                {
                    BlackScore += pawnsToReplace.Count;
                    WhiteScore -= pawnsToReplace.Count - 1;
                }
                else
                {
                    WhiteScore += pawnsToReplace.Count;
                    BlackScore -= pawnsToReplace.Count - 1;
                }

                //We then update the logic board based on the pawns to replace
                foreach (var pair in pawnsToReplace)
                {
                    LogicBoard[pair.Item1, pair.Item2] = color;
                    main.UpdateBoard(pair, isWhite);
                }
                return true;
            }
            return false;
        }

        public Tuple<char, int> getNextMove(int[,] game, int level, bool whiteTurn)
        {
            throw new NotImplementedException();
        }

        public int getWhiteScore()
        {
            return WhiteScore;
        }

        public int getBlackScore()
        {
            return BlackScore;
        }

        public bool isWhiteTurn()
        {
            return isWhite;
        }
    }
}
