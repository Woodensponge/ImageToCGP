using System;
using System.Drawing;

namespace ImageToCGP
{
    class Conversion
    {
        public static int BeginConversion(string file)
        {
            Bitmap image = (Bitmap)Image.FromFile(file);
            image.SetResolution(16f, 16f);
            return 0;
        }
    }
}
