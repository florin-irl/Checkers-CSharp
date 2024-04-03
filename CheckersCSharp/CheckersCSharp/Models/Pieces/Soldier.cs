using CheckersCSharp.Models.Moves;
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

        private static readonly Direction[] blackDirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast
        };

        private static readonly Direction[] whiteDirs = new Direction[]
        {
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Soldier(EPlayer color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Soldier copy = new Soldier(Color);
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            if(Color == EPlayer.Black)
            {
                return MovePositionsInDirs(from, board, blackDirs).Select(to => new NormalMove(from, to));
            }
            else
            {
                return MovePositionsInDirs(from, board, whiteDirs).Select(to => new NormalMove(from, to));
            }
        }
    }
}
