﻿using CheckersCSharp.Models;
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
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();

        private GameViewModel _gameViewModel;
        private Position selectedPos = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            _gameLogic = new GameLogic(EPlayer.Black, Board.Initial());

            DrawBoard(_gameLogic.Board);

            SetCursor(_gameLogic.CurrentPlayer);
        }

        private void InitializeBoard()
        {
            for(int r=0;r<8;r++)
            {
                for(int c=0;c<8;c++)
                {
                    Image image = new Image();
                    pieceImages[r,c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for(int r=0; r<8;r++)
            {
                   for(int c=0;c<8;c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r,c].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if(IsMenuOnScreen())
            {
                return;
            }

            Point point = e.GetPosition(BoardGrid);
            Position pos = TransformToSquarePos(point);

            if(selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }

        }

        private Position TransformToSquarePos(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = _gameLogic.LegalMovesForPiece(pos);

            if(moves.Any())
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
            
            if(moveCache.TryGetValue(pos, out Move move))
            {
                if(move.Type==EMoveType.SoldierPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
            
          
        }

        private void HandlePromotion(Position from, Position to)
        {
            pieceImages[to.Row,to.Column].Source = Images.GetImage(_gameLogic.CurrentPlayer,EPieceType.Soldier);
            pieceImages[from.Row, from.Column].Source = null;

            Move promMove = new SoldierPromotion(from, to, EPieceType.King);
            HandleMove(promMove);
        }

        private void HandleMove(Move move)
        {
            _gameLogic.MakeMove(move);
            DrawBoard(_gameLogic.Board);
            SetCursor(_gameLogic.CurrentPlayer);

            if(_gameLogic.IsGameOver())
            {
                ShowGameOverMenu();
            }
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach(Move move in moves)
            {
                moveCache[move.ToPos] = move;

            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 255);

            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }

        }

        private void HideHighlights()
        {
            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(EPlayer player)
        {
            if(player == EPlayer.White)
            {
                Cursor = CheckersCursors.WhiteCursor;
            }
            else
            {
                Cursor = CheckersCursors.BlackCursor;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOverMenu()
        {
            GameOverMenu menu = new GameOverMenu(_gameLogic);
            MenuContainer.Content = menu;

            menu.OptionSelected += option =>
            {
                if (option == EOption.Restart)
                {
                    menu.Content = null;
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
            SetCursor(_gameLogic.CurrentPlayer);
        }
    }
}
