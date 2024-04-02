using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersCSharp.Models
{
    public class GameState
    {
        public Board Board { get; }
        public EPlayer CurrentPlayer { get; private set; }

        public GameState(EPlayer player, Board board)
        {
            CurrentPlayer = player;
            Board = board;

        }
    }
}
