using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public abstract class PieceLogic
    {
        public abstract PieceType Type { get; }
        public abstract Player Colour { get; }
        public bool MovedBefore { get; set; } = false; // Set to false initially because some moves are only legal if the piece has not moved yet e.g. Castling

        public abstract PieceLogic Copy();

        // IEnumerable Used to iterate a given object in c#, The method allows readonly access to the collection and returns
        public abstract IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board);

        protected IEnumerable<Position> MoveInDirection(Position fromPos, ChessBoard board, Direction direction)
        {
            for (Position position = fromPos + direction; ChessBoard.OnTheBoard(position); position += direction) // Checks one square ahead in given direction and checks / returns if it is empty
            {
                if (board.EmptySquare(position))
                {
                    yield return position;      // If it is empty it will continue to check the next square
                    continue;
                }

                PieceLogic piece = board[position];

                if (piece.Colour != Colour)
                {
                    yield return position;
                }

                yield break;
            }
        }

        protected IEnumerable<Position> MoveInDirection(Position fromPos, ChessBoard board, Direction[] directions)
        {
            return directions.SelectMany(direction => MoveInDirection(fromPos, board, direction));
        }
    }
}
