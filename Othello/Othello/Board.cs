using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

using OthelloConsole;

namespace Othello
{
    class Board : IPlayable
    {
        public enum TileState
        {
            Empty,
            Black,
            White
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
        }

        private List<Vector2i> directions;

        private TileState[,] gameBoard;

        public Board()
        {
            gameBoard = new TileState[8,8];

            directions.Add(new Vector2i(0, 1));
            directions.Add(new Vector2i(0, -1));
            directions.Add(new Vector2i(1, 0));
            directions.Add(new Vector2i(1, 1));
            directions.Add(new Vector2i(1, -1));
            directions.Add(new Vector2i(-1, 0));
            directions.Add(new Vector2i(-1, 1));
            directions.Add(new Vector2i(-1, -1));


        }

        private TileState GetTileState(bool isWhite)
        {
            if (isWhite)
            {
                return TileState.White;
            }
            else
            {
                return TileState.Black;
            }
        }


        public bool isPlayable(int column, int line, bool isWhite)
        {
            List<Vector2i> changed = new List<Vector2i>();
            //6 direction
            foreach (var direction in directions)
            {
                var directionCopy = direction;
                List<Vector2i> changedTemp = new List<Vector2i>();
                while (gameBoard[column + directionCopy.x, line + directionCopy.y] == GetTileState(isWhite))
                {
                    changedTemp.Add(directionCopy);
                    directionCopy += direction;
                }

                try
                {
                    if (gameBoard[column + directionCopy.x, line + directionCopy.y] == GetTileState(!isWhite))
                    {
                        changed.AddRange(changedTemp);
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    
                }
            }

            return (changed.Count > 0);
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
