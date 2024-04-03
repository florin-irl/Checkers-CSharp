using CheckersCSharp.Models.Moves;
using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class GameState
    {
        public Board Board { get; }
        public EPlayer CurrentPlayer { get; private set; }

        public bool MultipleJumps { get; set; }

        public GameState(EPlayer player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
            MultipleJumps = false;

        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if(Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            return piece.GetMoves(pos, Board);
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            if(!MultipleJumps)
            {
                CurrentPlayer = CurrentPlayer.Opponent();
            }
            
        }
    }
}
