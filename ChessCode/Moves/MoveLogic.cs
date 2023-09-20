using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public abstract class MoveLogic
    {
        public abstract MoveType Type { get; }
        public abstract Position FromPosition { get; }
        public abstract Position ToPosition { get; }

        public abstract void Execute(ChessBoard board);
    }
}
