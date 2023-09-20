using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ChessCode;

namespace UI
{
    public static class ChessSprites
    {
        private static ImageSource LoadSprites(string filepath)  // Method takes relative path of an image as a parameter, returns a new bitmap image passing in a URI
        {
            return new BitmapImage(new Uri(filepath, UriKind.Relative));
        }

        private static readonly Dictionary<PieceType, ImageSource> darkSources = new()
        {
            {PieceType.Pawn, LoadSprites("Assets/Chess_pdt60.png") },        // All of the black pieces are loaded from a dictionary they are stored in
            {PieceType.Bishop, LoadSprites("Assets/Chess_bdt60.png") },
            {PieceType.Knight, LoadSprites("Assets/Chess_ndt60.png") },
            {PieceType.Rook, LoadSprites("Assets/Chess_rdt60.png") },
            {PieceType.Queen, LoadSprites("Assets/Chess_qdt60.png") },
            {PieceType.King, LoadSprites("Assets/Chess_kdt60.png") },
        };

        private static readonly Dictionary<PieceType, ImageSource> lightSources = new()
        {
            {PieceType.Pawn, LoadSprites("Assets/Chess_plt60.png") },         // All of the white pieces are loaded from a dictionary they are stored in
            {PieceType.Bishop, LoadSprites("Assets/Chess_blt60.png") },
            {PieceType.Knight, LoadSprites("Assets/Chess_nlt60.png") },
            {PieceType.Rook, LoadSprites("Assets/Chess_rlt60.png") },
            {PieceType.Queen, LoadSprites("Assets/Chess_qlt60.png") },
            {PieceType.King, LoadSprites("Assets/Chess_klt60.png") },
        };

        public static ImageSource GetImage(Player colour, PieceType type) // Method takes the colour and piece type and returns the corresponing image
        {
            return colour switch
            {
                Player.Light => lightSources[type],
                Player.Dark => darkSources[type],
                _ => null   // Depending on the colour, It will search the corresponding dictionary
            };
        }

        public static ImageSource GetImage(PieceLogic piece) // 
        {
            if (piece == null)
            {
                return null;
            }

            return GetImage(piece.Colour, piece.Type);
        }
    }
}
