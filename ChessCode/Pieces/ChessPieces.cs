using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class ChessPieces
    {
        // Pawns
        public class Pawns : PieceLogic
        {
            public override PieceType Type => PieceType.Pawn;
            public override Player Colour { get; }

            public Pawns(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;

                if (colour == Player.Light)        // Sets onlyforward as up for light pieces
                {
                    onlyforward = Direction.Up;
                }

                else if (colour == Player.Dark)    // Sets onlyforward as down for dark pieces
                {
                    onlyforward = Direction.Down;
                }
            }

            public override PieceLogic Copy()
            {
                Pawns copy = new Pawns(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }

            private readonly Direction onlyforward;   // Each players pawns can only move forward    (See line 18 - 26)

            private static bool LegalMoveTo(Position position, ChessBoard board)
            {
                return ChessBoard.OnTheBoard(position) && board.EmptySquare(position);
            }

            private bool LegalCaptureAt(Position position, ChessBoard board)
            {
                if (!ChessBoard.OnTheBoard(position) || board.EmptySquare(position))
                {
                    return false;  // Cannot capture if the position is not on the board or if the square is empty
                }
                return board[position].Colour != Colour; // Can only capture if the piece is the other colour
            }

            private IEnumerable<MoveLogic> TwoMovesForward(Position fromPos, ChessBoard board)
            {
                Position FirstMovePosition = fromPos + onlyforward;

                if (LegalMoveTo(FirstMovePosition, board))                             // Checks if the first square forward is a legal move
                {
                    yield return new NormalMoves(fromPos, FirstMovePosition);

                    Position SecondMovePosition = FirstMovePosition + onlyforward;

                    if (!MovedBefore && LegalMoveTo(SecondMovePosition, board))       // Checks if the pawn hasnt been move this game and the second square is a legal move
                    {
                        yield return new NormalMoves(fromPos, SecondMovePosition);
                    }
                }
            }

            private IEnumerable<MoveLogic> DiagonalCaptures(Position fromPos, ChessBoard board)
            {
                foreach (Direction direction in new Direction[] { Direction.Left, Direction.Right })
                {
                    Position toPos = fromPos + onlyforward + direction;

                    if (LegalCaptureAt(toPos, board))                                // Checks if the diagonal position it wants to capture at is a legal capture
                    {
                        yield return new NormalMoves(fromPos, toPos);
                    }
                }
            }

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                return TwoMovesForward(fromPos, board).Concat(DiagonalCaptures(fromPos, board));
            }
        }






        //Bishops
        public class Bishops : PieceLogic
        {
            public override PieceType Type => PieceType.Bishop;
            public override Player Colour { get; }

            public Bishops(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;
            }

            public override PieceLogic Copy()
            {
                Bishops copy = new Bishops(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }

            private static readonly Direction[] directions = new Direction[]  // All of the legal directions it can go
            {
                Direction.UpRight,
                Direction.UpLeft,
                Direction.DownRight,
                Direction.DownLeft
            };

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                return MoveInDirection(fromPos, board, directions).Select(toPos => new NormalMoves(fromPos, toPos));
            }


        }






        // Knights
        public class Knights : PieceLogic
        {
            public override PieceType Type => PieceType.Knight;
            public override Player Colour { get; }

            public Knights(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;
            }

            public override PieceLogic Copy()
            {
                Knights copy = new Knights(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }

            private static IEnumerable<Position> SurroundingMoves(Position fromPos)                                 // Shows all of the surrounding moves it can make in the L shape
            {
                foreach (Direction vertical_direction in new Direction[] { Direction.Up, Direction.Down })            // Vertical squares
                {
                    foreach (Direction horizontal_directions in new Direction[] { Direction.Left, Direction.Right })  // Horizontal squares
                    {
                        yield return fromPos + 2 * vertical_direction + horizontal_directions;
                        yield return fromPos + 2 * horizontal_directions + vertical_direction;
                    }
                }
            }

            private IEnumerable<Position> PotentialPosition(Position fromPos, ChessBoard board)
            {
                return SurroundingMoves(fromPos).Where(position => ChessBoard.OnTheBoard(position) && (board.EmptySquare(position) || board[position].Colour != Colour));
            }

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                return PotentialPosition(fromPos, board).Select(toPos => new NormalMoves(fromPos, toPos));
            }
        }






        //Rooks
        public class Rooks : PieceLogic
        {
            public override PieceType Type => PieceType.Rook;
            public override Player Colour { get; }

            public Rooks(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;
            }

            public override PieceLogic Copy()
            {
                Rooks copy = new Rooks(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }

            private static readonly Direction[] directions = new Direction[]  // All of the legal directions it can go
            {
                Direction.Up,
                Direction.Left,
                Direction.Right,
                Direction.Down
            };

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                return MoveInDirection(fromPos, board, directions).Select(toPos => new NormalMoves(fromPos, toPos));
            }
        }






        // Queens
        public class Queens : PieceLogic
        {
            public override PieceType Type => PieceType.Queen;
            public override Player Colour { get; }

            public Queens(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;
            }

            public override PieceLogic Copy()
            {
                Queens copy = new Queens(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }


            private static readonly Direction[] directions = new Direction[]  // All of the legal directions it can go
            {
                Direction.UpRight,
                Direction.UpLeft,
                Direction.DownRight,
                Direction.DownLeft,
                Direction.Up,
                Direction.Left,
                Direction.Right,
                Direction.Down

            };

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                return MoveInDirection(fromPos, board, directions).Select(toPos => new NormalMoves(fromPos, toPos));
            }
        }






        // Kings
        public class Kings : PieceLogic
        {
            public override PieceType Type => PieceType.King;
            public override Player Colour { get; }

            public Kings(Player colour) // Constructor takes colour as a parameter and sets the colour property
            {
                Colour = colour;
            }

            public override PieceLogic Copy()
            {
                Kings copy = new Kings(Colour);
                copy.MovedBefore = MovedBefore;
                return copy;
            }

            private static readonly Direction[] directions = new Direction[]
            {
                Direction.UpRight,
                Direction.UpLeft,
                Direction.DownLeft,
                Direction.DownRight,
                Direction.Up,
                Direction.Down,
                Direction.Left,
                Direction.Right

            };


            private IEnumerable<Position> PotentialPosition(Position fromPos, ChessBoard board)
            {
                foreach (Direction direction in directions)
                {
                    Position toPos = fromPos + direction;

                    if (!ChessBoard.OnTheBoard(toPos))
                    {
                        continue;
                    }

                    if (board.EmptySquare(toPos) || board[toPos].Colour != Colour)     // Checks if the square is empty or is an opposite colour piece
                    {
                        yield return toPos;
                    }
                }
            }

            public override IEnumerable<MoveLogic> Moves(Position fromPos, ChessBoard board)
            {
                foreach (Position toPos in PotentialPosition(fromPos, board))   // Loops through legal positions and returns a normal move for each
                {
                    yield return new NormalMoves(fromPos, toPos);
                }
            }
        }





    }
}
