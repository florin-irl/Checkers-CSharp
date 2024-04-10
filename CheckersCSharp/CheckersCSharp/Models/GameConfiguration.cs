using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CheckersCSharp.Models.Pieces;

namespace CheckersCSharp.Models
{
    public class GameConfiguration
    {
        public Piece[] Pieces { get; set; }
        public int BlackPieces { get; set; }
        public int WhitePieces { get; set; }

        public EPlayer CurrentPlayer { get; set; }

        public bool MultiJumps { get; set; }

        public GameConfiguration(GameLogic gameLogic)
        {
            Pieces = new Piece[8 * 8];
            BlackPieces = gameLogic.BlackPieces;
            WhitePieces = gameLogic.WhitePieces;
            CurrentPlayer = gameLogic.CurrentPlayer;
            MultiJumps = gameLogic.MultipleJumps;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Pieces[i * 8 + j] = gameLogic.Board[i, j];
                }
            }
        }

        [JsonConstructorAttribute]
        public GameConfiguration(Piece[] pieces, int blackPieces, int whitePieces, EPlayer currentPlayer,
            bool multiJumps)
        {
            Pieces = pieces;
            BlackPieces = blackPieces;
            WhitePieces = whitePieces;
            CurrentPlayer = currentPlayer;
            MultiJumps = multiJumps;
        }
    }
}
