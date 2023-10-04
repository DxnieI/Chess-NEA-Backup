using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ChessCode;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;  

namespace UI
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();
            InitializeBoard();

            conditions = new Conditions(Player.Light, ChessBoard.InitialBoard());
            DrawingBoard(conditions.Board);
        }

        private readonly Image[,] pieceImages = new Image[8, 8];

        private void InitializeBoard() // Method loops over all positions, each image control is stored in the array and added as a child to the uniform grid
        {
            for (int r = 0; r < 8; r++)
            {
                for (int f = 0; f < 8; f++)
                {
                    Image image = new Image();
                    pieceImages[r, f] = image;
                    grid_piece.Children.Add(image);

                    Rectangle MoveHelping = new Rectangle();
                    MoveHelp[r,f] = MoveHelping;
                    grid_move_help.Children.Add(MoveHelping);
                }
            }
        }

        private void DrawingBoard(ChessBoard board)  // Takes board as a parameter and sets the source of all images so it matches the pieces on the board
        {
            for (int r = 0; r < 8; r++)
            {
                for (int f = 0; f < 8; f++)
                {
                    PieceLogic piece = board[r, f];
                    pieceImages[r, f].Source = ChessSprites.GetImage(piece);
                }
            }
        }

        private Conditions conditions;


        
        private readonly Rectangle[,] MoveHelp = new Rectangle[8, 8];
        private readonly Dictionary<Position, MoveLogic> moveTemp = new Dictionary<Position, MoveLogic>();
        private Position selectedPosition = null;

        private void TempMoves(IEnumerable<MoveLogic> moves)
        {
            moveTemp.Clear();

            foreach (MoveLogic move in moves)
            {
                moveTemp[move.ToPosition] = move;
            }
        }

        private void ShowHelpMoves()
        {
            Color colour = Color.FromArgb(255, 255, 204, 203);

            foreach (Position toPosition in moveTemp.Keys)
            {
                MoveHelp[toPosition.Rank, toPosition.File].Fill = new SolidColorBrush(colour);
            }
        }

        private void HideHelpMoves()
        {
            foreach (Position toPosition in moveTemp.Keys)
            {
                MoveHelp[toPosition.Rank, toPosition.File].Fill = Brushes.Transparent;
            }
        }

        private void ExecuteMove(MoveLogic move)
        {
            conditions.MovePiece(move);
            DrawingBoard(conditions.Board);
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = grid_board.ActualWidth / 8;
            int rank = (int)(point.Y / squareSize);
            int file = (int)(point.X / squareSize);

            return new Position(rank, file);
        }

        private void MovesForSelectedPosition(Position position)
        {
            IEnumerable<MoveLogic> moves = conditions.PiecesLegalMoves(position);

            if (moves.Any())
            {
                selectedPosition = position;
                TempMoves(moves);
                ShowHelpMoves();
            }
        }

        private void MoveToSelectedPosition(Position position)
        {
            selectedPosition = null;
            HideHelpMoves();

            if (moveTemp.TryGetValue(position, out MoveLogic move))
            {
                ExecuteMove(move);
            }
        }

        private void grid_board_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(grid_board);

            Position position = ToSquarePosition(point);

            if (selectedPosition == null)
            {
                MovesForSelectedPosition(position);
            }
            else
            {
                MoveToSelectedPosition(position);
            }
        }
    }
}
