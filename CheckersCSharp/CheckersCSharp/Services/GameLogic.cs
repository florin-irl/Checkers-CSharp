using CheckersCSharp.Models.Moves;
using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CheckersCSharp.Services;

namespace CheckersCSharp.Models
{
    public class GameLogic
    {
        public Board Board { get; private set; }
        public EPlayer CurrentPlayer { get;  set; }

        public Result Result { get; private set; } = null;

        public bool MultipleJumps { get; set; }

        public int BlackPieces { get; set; }

        public int WhitePieces { get; set; }

        public GameLogic(EPlayer player, Board board)
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
                if (!MultipleJumps)
                {
                    CurrentPlayer = CurrentPlayer.Opponent();
                }
                else
                {

                    IEnumerable<Move> possibleMoves = LegalMovesForPiece(move.ToPos);
                    List<Move> updatedMoves = new List<Move>();

                    foreach (Move m in possibleMoves)
                    {
                        Position capturePos = new Position((m.FromPos.Row + m.ToPos.Row) / 2, (m.FromPos.Column + m.ToPos.Column) / 2);
                        if (Board[capturePos]
                            != null)
                        {
                            updatedMoves.Add(m);
                        }
                    }

                    if (updatedMoves.Count == 0)
                    {
                        CurrentPlayer = CurrentPlayer.Opponent();
                    }
                }
            }
            else
            {
                CurrentPlayer = CurrentPlayer.Opponent();
            }
            
            CheckForGameEnd();
            
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


        private void CheckForGameEnd()
        {

            if (BlackPieces == 0)
            {
                Result = Result.Win(EPlayer.White);
            }
            else if (WhitePieces == 0)
            {
                Result = Result.Win(EPlayer.Black);
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        public void SaveGame(string filePath)
        {
            var gameConfiguration = new GameConfiguration(this);
            JSONUtility.SerializeGame(gameConfiguration, filePath);
        }

        public void LoadGame(string filePath)
        {
            var gameConfiguration = JSONUtility.DeserializeGame(filePath);
            Board = new Board(gameConfiguration.Pieces);
            CurrentPlayer = gameConfiguration.CurrentPlayer;
            MultipleJumps = gameConfiguration.MultiJumps;
            BlackPieces = gameConfiguration.BlackPieces;
            WhitePieces = gameConfiguration.WhitePieces;

        }
    }
}
