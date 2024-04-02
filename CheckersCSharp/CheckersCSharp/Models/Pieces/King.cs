using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Pieces
{
    public class King : Piece
    {
        public override EPieceType Type => EPieceType.King;

        public override EPlayer Color { get; }

        public King(EPlayer color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            return copy;
        }
    }
}
