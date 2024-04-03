using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace CheckersCSharp.Views
{
    public static class CheckersCursors
    {

        public static readonly Cursor WhiteCursor = LoadCursor("Resources/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursor("Resources/CursorB.cur");
        private static Cursor LoadCursor(string filePath)
        {
            Stream stream = System.Windows.Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream);
        }
    }
}
