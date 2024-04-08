using CheckersCSharp.Models;
using CheckersCSharp.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersCSharp.Views
{
    public static class Images
    {
        private static readonly Dictionary<EPieceType, ImageSource> whiteSources = new Dictionary<EPieceType,ImageSource>
        {
            {
                EPieceType.Soldier,LoadImage("Assets/WhiteSoldier.png")
            },

            {
                EPieceType.King,LoadImage("Assets/WhiteKing.png")
            }
        };

        private static readonly Dictionary<EPieceType, ImageSource> blackSources = new Dictionary<EPieceType, ImageSource>
        {
            {
                EPieceType.Soldier,LoadImage("Assets/BlackSoldier.png")
            },

            {
                EPieceType.King,LoadImage("Assets/BlackKing.png")
            }
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(EPlayer color, EPieceType type)
        {
            return color == EPlayer.White ? whiteSources[type] : blackSources[type];
        }

        public static ImageSource GetImage(Piece piece)
        {
            if(piece==null)
            {
                return null;
            }
            return GetImage(piece.Color, piece.Type);
        }
    } 
}
