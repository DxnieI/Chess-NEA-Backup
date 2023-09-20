using ChessCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCode
{
    public class ChessBoard
    {
        private readonly PieceLogic[,] pieces = new PieceLogic[8, 8];  // 8 rows and 8 columns for the chess board, private array so access is provided through an indexer

        public PieceLogic this[int rank, int file] // Indexer
        {
            get { return pieces[rank, file]; }  // Returns the piece in the current position (null if empty)
            set { pieces[rank, file] = value; }
        }

        public PieceLogic this[Position position] // Indexer
        {
            get { return this[position.Rank, position.File]; }
            set { this[position.Rank, position.File] = value; }
        }

        private void AddStartingPieces()  // Adds all the pieces to the board
        {
            this[0, 0] = new ChessPieces.Rooks(Player.Dark);
            this[0, 1] = new ChessPieces.Knights(Player.Dark);
            this[0, 2] = new ChessPieces.Bishops(Player.Dark);
            this[0, 3] = new ChessPieces.Queens(Player.Dark);
            this[0, 4] = new ChessPieces.Kings(Player.Dark);
            this[0, 5] = new ChessPieces.Bishops(Player.Dark);
            this[0, 6] = new ChessPieces.Knights(Player.Dark);
            this[0, 7] = new ChessPieces.Rooks(Player.Dark);

            this[7, 0] = new ChessPieces.Rooks(Player.Light);
            this[7, 1] = new ChessPieces.Knights(Player.Light);
            this[7, 2] = new ChessPieces.Bishops(Player.Light);
            this[7, 3] = new ChessPieces.Queens(Player.Light);
            this[7, 4] = new ChessPieces.Kings(Player.Light);
            this[7, 5] = new ChessPieces.Bishops(Player.Light);
            this[7, 6] = new ChessPieces.Knights(Player.Light);
            this[7, 7] = new ChessPieces.Rooks(Player.Light);

            for (int a = 0; a < 8; a++)  // Adding all the pawns
            {
                this[1, a] = new ChessPieces.Pawns(Player.Dark);
                this[6, a] = new ChessPieces.Pawns(Player.Light);
            }
        }

        public static ChessBoard InitialBoard()  // Returns a board which is correctly set up at the start of the game
        {
            ChessBoard board = new ChessBoard();
            board.AddStartingPieces();
            return board;
        }

        public static bool OnTheBoard(Position position)  // Method takes a position and returns true if its on the board
        {
            return position.Rank >= 0 && position.Rank < 8 && position.File >= 0 && position.File < 8;
        }

        public bool EmptySquare(Position position) // Method takes a position and returns true if there is no piece at that position
        {
            return this[position] == null;
        }
    }
}
