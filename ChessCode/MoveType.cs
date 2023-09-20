using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public enum MoveType
    {
        Normal,
        CastlingKS,
        CastlingQS,
        EnPassant,
        OpeningPawn,
        Promotion

    };
}
