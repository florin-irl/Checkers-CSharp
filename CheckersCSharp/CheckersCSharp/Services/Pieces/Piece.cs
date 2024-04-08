using CheckersCSharp.Models.Moves;
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

        public abstract IEnumerable<Move> GetMoves(Position from, Board board);

        protected IEnumerable<Position> MovePositionsInDir(Position from,Board board,Direction dir)
        {
            Position pos = from + dir;
            if(Board.IsInside(pos) && board.IsEmpty(pos))
            {
                yield return pos;
            }
            else
            if(Board.IsInside(pos + dir) && board.IsEmpty(pos + dir) && board[pos].Color!=Color)
            {
                pos = pos + dir;
                yield return pos;
            }
            yield break;
        }

        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir)); 
        }
    }
}
