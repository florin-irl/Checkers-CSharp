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
    }
}
