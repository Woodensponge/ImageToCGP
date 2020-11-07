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
            for (int x = 0; x <= image.Width; x++)
                for (int y = 0; y <= image.Height; y++)
                {

                }
            return 0;
        }
    }
}
