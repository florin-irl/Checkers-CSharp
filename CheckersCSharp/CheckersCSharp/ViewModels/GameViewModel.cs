using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersCSharp.Models;

namespace CheckersCSharp.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly GameLogic _gameLogic;

        GameViewModel()
        {

            _gameLogic = new GameLogic(EPlayer.Black, Board.Initial());
        }
    }
}
