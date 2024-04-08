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
using CheckersCSharp.ViewModels;

namespace CheckersCSharp.Views
{
    /// <summary>
    /// Interaction logic for GameOverMenuUserControl.xaml
    /// </summary>
    public partial class GameOverMenuUserControl : UserControl
    {

        public event Action<EOption> OptionSelected;
        public GameOverMenuUserControl(Result result, EPlayer currentPlayer)
        {
            InitializeComponent();

            DataContext = new GameOverMenuViewModel(result, currentPlayer);
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
