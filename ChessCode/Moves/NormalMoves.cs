using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class NormalMoves : MoveLogic
    {
        public override MoveType Type => MoveType.Normal;
        public override Position FromPosition { get; }
        public override Position ToPosition { get; }

        public NormalMoves(Position fromPos, Position toPos) // Constructor takes the positions and stores them in the variables
        {
            FromPosition = fromPos;
            ToPosition = toPos;
        }

        public override void Execute(ChessBoard board)  // Takes the piece at 'From' and moves it to the 'To', Then makes 'From' null and evaluates HasMoved to true
        {
            PieceLogic piece = board[FromPosition];
            board[ToPosition] = piece;
            board[FromPosition] = null;
            piece.MovedBefore= true;
        }
    }
}
