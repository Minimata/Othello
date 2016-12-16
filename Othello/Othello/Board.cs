using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    class Board
    {
        public enum TileState
        {
            Empty,
            Black,
            White
        };

        private TileState[,] gameBoard;

        public  Board()
        {
            gameBoard = new TileState[8,8];
        }


    }
}
