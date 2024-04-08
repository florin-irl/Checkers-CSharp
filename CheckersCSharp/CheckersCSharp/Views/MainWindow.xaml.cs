using CheckersCSharp.Models;
using CheckersCSharp.Models.Moves;
using CheckersCSharp.Models.Pieces;
using CheckersCSharp.Views;
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


namespace CheckersCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(IsMenuOnScreen())
            {
                return;
            }

            var point = e.GetPosition(BoardGrid); 
            var pos = TransformToSquarePos(point);

            ((GameViewModel)DataContext).OnBoardGridClicked(pos);

        }

        private Position TransformToSquarePos(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }
    }
}
