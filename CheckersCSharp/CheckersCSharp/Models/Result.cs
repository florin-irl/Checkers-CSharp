using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class Result
    {
        public EPlayer Winner { get; }
        public EEndReason Reason { get; }

        public Result(EPlayer winner, EEndReason reason)
        {
            Winner = winner;
            Reason = reason;
        }

        public static Result Win(EPlayer winner)
        {
            if(winner == EPlayer.White)
            {
                return new Result(EPlayer.White, EEndReason.WhiteWin);
            }
            else
            {
                return new Result(EPlayer.Black, EEndReason.BlackWin);
            }
        }
    }
}
