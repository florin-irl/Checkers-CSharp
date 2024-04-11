using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CheckersCSharp.Commands;
using CheckersCSharp.Models;
using CheckersCSharp.Models.Moves;
using CheckersCSharp.Models.Pieces;
using CheckersCSharp.Services;
using CheckersCSharp.Views;
using Microsoft.Win32;

namespace CheckersCSharp.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private GameLogic _gameLogic;
        public string BlackWins { get; set; }
        public string WhiteWins { get; set; }

        public ICommand ToggleAllowMultipleJumpsCommand { get; }
        public ICommand NewGameCommand { get; }

        public ICommand SwitchTurnCommand { get; }

        public ICommand SaveGameCommand { get; }

        public ICommand LoadGameCommand { get; }

        public void SwitchTurn()
        {
            if (!_gameLogic.MultipleJumps)
            {
                return;
            }
            _gameLogic.CurrentPlayer = _gameLogic.CurrentPlayer == EPlayer.Black ? EPlayer.White : EPlayer.Black;
            DrawBoard(_gameLogic.Board);
            CurrentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;
            if (_gameLogic.IsGameOver())
            {
                ShowGameOverMenu();
            }
        }


        public ObservableCollection<Image> PieceImages { get; set; } = new ObservableCollection<Image>();
        public ObservableCollection<Rectangle> Highlights { get; set; } = new ObservableCollection<Rectangle>();

        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private Position selectedPos = null;

        private GameOperations _gameOperations;
        public GameOverMenuUserControl GameOverMenuUserControl { get; set; } = null;

        private Cursor _currentCursor;
        public Cursor CurrentCursor
        {
            get => _currentCursor;
            set
            {
                _currentCursor = value;
                OnPropertyChanged();
            }
        }

        public void ToggleAllowMultipleJumps()
        {
            // Flip the value of your bool property in GameLogic
            _gameLogic.MultipleJumps = !_gameLogic.MultipleJumps;
        }


        public GameViewModel()
        {
            _gameLogic = new GameLogic(EPlayer.Black, Board.Initial());

            _gameOperations = new GameOperations(this);

            ToggleAllowMultipleJumpsCommand = new RelayCommand(param => _gameOperations.ToggleAllowMultipleJumps());
            NewGameCommand = new RelayCommand(param => _gameOperations.NewGame());
            SwitchTurnCommand = new RelayCommand(param => _gameOperations.SwitchTurn());
            SaveGameCommand = new RelayCommand(param => _gameOperations.SaveGame());
            LoadGameCommand = new RelayCommand(param => _gameOperations.LoadGame());

            InitializeBoard();

            DrawBoard(_gameLogic.Board);

            _currentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;

            using (StreamReader sr = new StreamReader("ScoreBoard.txt"))
            {
                BlackWins = sr.ReadLine();
                WhiteWins = sr.ReadLine();
            }
        }

        public void SaveGame()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Checker Game Files (*.ck)|*.ck",
                DefaultExt = "ck",
                AddExtension = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                _gameLogic.SaveGame(saveFileDialog.FileName);
            }
        }

        public void LoadGame()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Checker Game Files (*.ck)|*.ck",
                DefaultExt = "ck",
                AddExtension = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                _gameLogic.LoadGame(openFileDialog.FileName);
                DrawBoard(_gameLogic.Board);
                CurrentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;
            }
        }
        
        

        private void InitializeBoard()
        {
            
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    PieceImages.Add(image);
                    //PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    Highlights.Add(highlight);
                    //HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    PieceImages[r * 8 + c].Source = Images.GetImage(piece);
                }
            }
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (var move in moves)
            {
                moveCache[move.ToPos] = move;

            }
        }



        private void ShowHighlights()
        {
            var color = Color.FromArgb(150, 125, 255, 255);

            foreach (var to in moveCache.Keys)
            {
                Highlights[to.Row * 8 + to.Column].Fill = new SolidColorBrush(color);
            }

        }
        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = _gameLogic.LegalMovesForPiece(pos);

            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();

            if (moveCache.TryGetValue(pos, out Move move))
            {
                if (move.Type == EMoveType.SoldierPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }


        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                Highlights[to.Row * 8 + to.Column].Fill = Brushes.Transparent;
            }
        }

        private void HandlePromotion(Position from, Position to)
        {
            PieceImages[to.Row * 8 + to.Column].Source = Images.GetImage(_gameLogic.CurrentPlayer, EPieceType.Soldier);
            PieceImages[from.Row * 8 + from.Column].Source = null;
            OnPropertyChanged(nameof(PieceImages));

            Move promMove = new SoldierPromotion(from, to, EPieceType.King);
            HandleMove(promMove);
        }

        private void HandleMove(Move move)
        {
            _gameLogic.MakeMove(move);
            DrawBoard(_gameLogic.Board);
            CurrentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;

            if (_gameLogic.IsGameOver())
            {
                ShowGameOverMenu();
            }
        }
        private void ShowGameOverMenu()
        {
            GameOverMenuUserControl = new GameOverMenuUserControl(_gameLogic.Result, _gameLogic.CurrentPlayer);
            OnPropertyChanged(nameof(GameOverMenuUserControl));
            if (_gameLogic.Result.Winner == EPlayer.Black)
            {
                int value = int.Parse(BlackWins);
                value++;
                BlackWins = value.ToString();
            }
            else
            {
                int value = int.Parse(WhiteWins);
                value++;
                WhiteWins = value.ToString();
            }

            using (StreamWriter sw = new StreamWriter("ScoreBoard.txt"))
            {
                sw.WriteLine(BlackWins);
                sw.WriteLine(WhiteWins);
            }

            GameOverMenuUserControl.OptionSelected += option =>
            {
                if (option == EOption.Restart)
                {
                    GameOverMenuUserControl.Content = null;
                    OnPropertyChanged(nameof(GameOverMenuUserControl));
                    RestartGame();
                    
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        public void RestartGame()
        {
            HideHighlights();
            moveCache.Clear();
            _gameLogic = new GameLogic(EPlayer.Black, Board.Initial());
            DrawBoard(_gameLogic.Board);
            CurrentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;

        }

        public void OnBoardGridClicked(Position pos)
        {
            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

       
    

    }
}
