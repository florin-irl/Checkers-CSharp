﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column) { Row = row; Column = column; }

        public EPlayer SquareColor()
        {
            if ((Row + Column) % 2 == 0)
                return EPlayer.White;
            else
                return EPlayer.Black;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            int hashCode = 240067226;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Column.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
    }
}