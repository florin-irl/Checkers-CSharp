using CheckersCSharp.Models.Moves;
using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class GameState
    {
        public Board Board { get; }
        public EPlayer CurrentPlayer { get; private set; }

        public bool MultipleJumps { get; set; }

        public int BlackPieces { get; set; }

        public int WhitePieces { get; set; }

        public GameState(EPlayer player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
            MultipleJumps = false;
            BlackPieces = 12;
            WhitePieces = 12;

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
            if(move.Execute(Board))
            {
                UpdatePieceCounter(move);
            }
            if(!MultipleJumps)
            {
                CurrentPlayer = CurrentPlayer.Opponent();
            }
            
        }

        public void UpdatePieceCounter(Move move)
        {
            Position pos = move.ToPos;
            Piece piece = Board[pos];
            if (piece.Color == EPlayer.Black)
            {
                WhitePieces--;
            }
            else
            {
                BlackPieces--;
            }
            Console.WriteLine("Black: " + BlackPieces + " White: " + WhitePieces);
        }
    }
}
