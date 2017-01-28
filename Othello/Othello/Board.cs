﻿using System;
using System.Collections.Generic;

using OthelloConsole;

namespace Othello
{
    class Board : IPlayable
    {
        private MainWindow main;
        private bool isWhite;
        public int NumTiles { get; private set; }
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
        }

        public void TileClicked(int column, int line)
        {
            if (isPlayable(column, line, isWhite))
            {
                //playMove(column, line, isWhite);
                main.UpdateBoard(new int[,] { { column, line } }, isWhite);
                isWhite = !isWhite;
            }
        }
        
        public bool isPlayable(int column, int line, bool isWhite)
        {
            bool response = false;
            if((LogicBoard[column, line] != 0)) return response; // Tile must be empty

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
                        LogicBoard[tile.x, tile.y] != color)
                    {
                        tile = tile + dir;
                    }
                    if(tile.IsValid(NumTiles) && LogicBoard[tile.x, tile.y] == color)
                    {
                        response = true;
                    }
                }
            }

            return response;
        }

        public bool playMove(int column, int line, bool isWhite)
        {
            throw new NotImplementedException();
        }

        public Tuple<char, int> getNextMove(int[,] game, int level, bool whiteTurn)
        {
            throw new NotImplementedException();
        }

        public int getWhiteScore()
        {
            throw new NotImplementedException();
        }

        public int getBlackScore()
        {
            throw new NotImplementedException();
        }
    }
}
