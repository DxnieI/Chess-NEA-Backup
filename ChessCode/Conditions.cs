using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class Conditions
    {
        public ChessBoard Board { get; }
        // Stores the board and which current player it is
        public Player CurrentPlayer { get; private set; }

        public Conditions(Player player, ChessBoard board) // Constructor takes the player and board as parameters, this will make testing easier
        {
            CurrentPlayer = player;
            Board = board;
        }

        public IEnumerable<MoveLogic> PiecesLegalMoves(Position position)   // Takes a position as parameter and returns all the moves that piece can make
        {
            if (Board.EmptySquare(position) || Board[position].Colour != CurrentPlayer)   // Checks if its empty or has a piece of the other colour
            {
                return Enumerable.Empty<MoveLogic>(); // Results in no legal moves
            }

            PieceLogic piece = Board[position];   // Else if we get all the moves once its at that position
            return piece.Moves(position, Board);
        }

        public void MovePiece(MoveLogic move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
        }
    }
}
