using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get
            {
                return pieces[row, col];
            }
            set
            {
                pieces[row,col] = value;
            }
        }

        public Piece this[Position pos]
        {
            get
            {
                return this[pos.Row,pos.Column];
                
            }
            set
            {
                this[pos.Row,pos.Column] = value;
            }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        this[row, col] = new Soldier(EPlayer.White);
                    }
                }
            }

            for (int row = 5; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        this[row, col] = new Soldier(EPlayer.Black);
                    }
                }
            }
        }

        public static bool IsInside(Position pos)
        {
              return pos.Row>=0 && pos.Row<8 && pos.Column>=0 && pos.Column<8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }
    }
}
