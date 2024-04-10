using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersCSharp.ViewModels;

namespace CheckersCSharp.Services
{
    public class GameOperations
    {
        private GameViewModel _gameViewModel;
        public GameOperations(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
        }

        public void ToggleAllowMultipleJumps()
        {
            _gameViewModel.ToggleAllowMultipleJumps();
        }

        public void NewGame()
        {
            _gameViewModel.RestartGame();
        }

        public void SwitchTurn()
        {
            _gameViewModel.SwitchTurn();
        }

        public void SaveGame()
        {
                        _gameViewModel.SaveGame();
        }

        public void LoadGame()
        {
            _gameViewModel.LoadGame();
        }
    }
}
