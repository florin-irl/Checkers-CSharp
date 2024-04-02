using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Pieces
{
    public class Soldier : Piece
    {
        public override EPieceType Type => EPieceType.Soldier;

       public override EPlayer Color { get; }

        public Soldier(EPlayer color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Soldier copy = new Soldier(Color);
            return copy;
        }
    }
}
