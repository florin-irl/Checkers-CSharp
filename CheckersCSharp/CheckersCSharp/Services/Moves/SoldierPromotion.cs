using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Moves
{
    public class SoldierPromotion : Move
    {
        public override EMoveType Type => EMoveType.SoldierPromotion;
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly EPieceType newType;

        public SoldierPromotion(Position from, Position to, EPieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;

        }

        private Piece CreatePromotionPiece(EPlayer color)
        {
            return new King(color);
        }

        public override bool Execute(Board board)
        {
            if(Math.Abs(FromPos.Row - ToPos.Row) == 2)
            {
                Position capturePos = new Position((FromPos.Row + ToPos.Row) / 2, (FromPos.Column + ToPos.Column) / 2);
                Piece soldier = board[FromPos];
                board[FromPos] = null;

                Piece newKing = CreatePromotionPiece(soldier.Color);
                board[ToPos] = newKing;

                board[capturePos] = null;
                return true;
            }
            else
            {
                Piece soldier = board[FromPos];
                board[FromPos] = null;

                Piece newKing = CreatePromotionPiece(soldier.Color);
                board[ToPos] = newKing;
                return false;
            }
           
        }
    }
}
