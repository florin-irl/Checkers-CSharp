using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Moves
{
    public class CaptureMove : Move
    {
        public override EMoveType Type => EMoveType.Capture;
        public override Position FromPos { get; }

        public override Position ToPos { get; }

        public Position CapturePos { get; }

        public CaptureMove(Position fromPos, Position toPos, Position capturePos)
        {
            FromPos = fromPos;
            ToPos = toPos;
            CapturePos = capturePos;
        }

        public override bool Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            board[CapturePos] = null;
            return true;
        }
    }
}
