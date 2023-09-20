using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public enum Player // enum stores which players turn it is and used to determine the colour of the chess pieces
    {
        None,  // also used to store who won the game e.g. None = draw
        Light,
        Dark
    }

    public static class PlayerOP
    {
        public static Player Opponent(this Player player)  // Method takes the player as a parameter and return the opponent
        {
            return player switch //switch expression
            {
                Player.Light => Player.Dark,
                Player.Dark => Player.Light,
                _ => Player.None,
            };
        }
    }
}
