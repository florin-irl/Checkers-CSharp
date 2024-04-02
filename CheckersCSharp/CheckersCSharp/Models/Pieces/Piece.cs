using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Pieces
{
    public abstract class Piece
    {
        public abstract EPieceType Type { get; }
        public abstract EPlayer Color { get; }
        public abstract Piece Copy();
    }
}
