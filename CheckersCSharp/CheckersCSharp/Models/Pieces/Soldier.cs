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

        private static IEnumerable<Move> PromotionMoves(Position from, Position to)
        {
            yield return new SoldierPromotion(from, to, EPieceType.King);
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            if(Color == EPlayer.Black)
            {
                IEnumerable<Position> blackMoves = MovePositionsInDirs(from, board, blackDirs);
                foreach(Position to in blackMoves)
                {
                    if(to.Row == 0)
                    {
                        yield return new SoldierPromotion(from, to, EPieceType.King);
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
                }
            }
            else
            {
                IEnumerable<Position> whiteMoves = MovePositionsInDirs(from, board, whiteDirs);
                foreach(Position to in whiteMoves)
                {
                    if(to.Row == 7)
                    {
                        yield return new SoldierPromotion(from, to, EPieceType.King);
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
                }
            }
        }
    }
}
