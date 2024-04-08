using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
using CheckersCSharp.Views;

namespace CheckersCSharp.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private GameLogic _gameLogic;
        public ObservableCollection<Image> PieceImages { get; set; } = new ObservableCollection<Image>();
        public ObservableCollection<Rectangle> Highlights { get; set; } = new ObservableCollection<Rectangle>();

        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private Position selectedPos = null;

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

        public GameViewModel()
        {
            _gameLogic = new GameLogic(EPlayer.Black, Board.Initial());

            InitializeBoard();

            DrawBoard(_gameLogic.Board);

            _currentCursor = _gameLogic.CurrentPlayer == EPlayer.Black ? CheckersCursors.BlackCursor : CheckersCursors.WhiteCursor;
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

        private void RestartGame()
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
