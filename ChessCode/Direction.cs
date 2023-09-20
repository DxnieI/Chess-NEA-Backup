using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class Direction
    {
        public int RankChange { get; }  // Changes in rank and file
        public int FileChange { get; }

        public Direction(int rankChange, int fileChange) // Constructor initialises rankChange and fileChange
        {
            RankChange = rankChange;
            FileChange = fileChange;
        }

        public static Direction operator +(Direction direction1, Direction direction2)  // Plus operator, Takes two direction parameters and returns a new combined direction
        {
            return new Direction(direction1.RankChange + direction2.RankChange, direction1.FileChange + direction2.FileChange);
        }

        public static Direction operator *(int scalar, Direction direction) // Multiplication operator, Takes scalar and a direction as paramters so a direction can be scaled
        {
            return new Direction(scalar * direction.RankChange, scalar * direction.FileChange);
        }

        // Direction takes the RowDelta and ColumnDelta as parameters to make the up, down, left, right and diagonal directions
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction UpRight = Up + Right;      // Diagonal directions combine directions
        public readonly static Direction UpLeft = Up + Left;
        public readonly static Direction DownRight = Down + Right;
        public readonly static Direction DownLeft = Down + Left;
    }
}
