﻿using ChessCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
