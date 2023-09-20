using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class Position
    {
        public int Rank { get; }
        public int File { get; }

        public Position(int rank, int file)  // Constructor takes rank and file, stores them in the variables
        {
            Rank = rank;
            File = file;
        }

        public Player SquareColour()
        {
            if ((Rank + File) % 2 == 0)  // If it evaluates as even its a light square, otherwise its a dark square
            {
                return Player.Light;
            }

            return Player.Dark;
        }

        //Generated Equals and GetHashCode
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Rank == position.Rank &&
                   File == position.File;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rank, File);
        }

        public static bool operator ==(Position left, Position right)  // can use == and != to compare positions
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static Position operator +(Position position, Direction direction) // Takes a position and direction as parameters, returns the position you get after one step in that direction
        {
            return new Position(position.Rank + direction.RankChange, position.File + direction.FileChange);
        }
    }
}
