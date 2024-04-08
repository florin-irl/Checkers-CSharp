using CheckersCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckersCSharp.Views
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {

        public event Action<EOption> OptionSelected;
        public GameOverMenu(GameLogic gameLogic)
        {
            InitializeComponent();

            Result result = gameLogic.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            WinnerName.Text = GetReasonText(result.Reason, gameLogic.CurrentPlayer);

        }

        private static string GetWinnerText(EPlayer winner)
        {
            
            return winner == EPlayer.White ? "White wins!" : "Black wins!";
            
        }

        private static string GetReasonText(EEndReason reason, EPlayer currentPlayer)
        {
            return reason == EEndReason.WhiteWin ? "Black has no more pieces!" : "White has no more pieces!";
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(EOption.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(EOption.Exit);
        }
    }
}
