﻿using CheckersCSharp.Models.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models.Pieces
{
    public class King : Piece
    {
        public override EPieceType Type => EPieceType.King;

        public override EPlayer Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public King(EPlayer color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        } 
    }
}
