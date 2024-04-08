using CheckersCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckersCSharp.Commands;

namespace CheckersCSharp.ViewModels
{
    public class GameOverMenuViewModel
    {
        public string Result { get; set; }
        public string CurrentPlayer { get; set; }

        public GameOverMenuViewModel(Result result, EPlayer currentPlayer)
        {
            Result = GetWinnerText(result.Winner);
            CurrentPlayer = GetReasonText(result.Reason, currentPlayer);
        }
        private static string GetWinnerText(EPlayer winner)
        {
            return winner == EPlayer.White ? "White wins!" : "Black wins!";
        }

        private static string GetReasonText(EEndReason reason, EPlayer currentPlayer)
        {
            return reason == EEndReason.WhiteWin ? "Black has no more pieces!" : "White has no more pieces!";
        }
    }
}
