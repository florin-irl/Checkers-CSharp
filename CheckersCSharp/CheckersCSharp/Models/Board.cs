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
                return this[pos.Row,pos.Col];
            }
        }
    }
}
