using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Moves
{
    public class NormalMove : Move
    {
        public override EMoveType Type => EMoveType.Normal;
        public override Position FromPos { get; }

        public override Position ToPos { get; }

        public NormalMove(Position fromPos, Position toPos)
        {
            FromPos = fromPos;
            ToPos = toPos;
        }

        public override bool Execute(Board board)
        {
            if (Math.Abs(FromPos.Row - ToPos.Row) == 2)
            {
                Position capturePos = new Position((FromPos.Row + ToPos.Row) / 2, (FromPos.Column + ToPos.Column) / 2);
                Piece piece = board[FromPos];
                board[ToPos] = piece;
                board[FromPos] = null;
                board[capturePos] = null;
                return true;
            }
            else
            {
                Piece piece = board[FromPos];
                board[ToPos] = piece;
                board[FromPos] = null;
                return false;
            }
        }
    }
}
