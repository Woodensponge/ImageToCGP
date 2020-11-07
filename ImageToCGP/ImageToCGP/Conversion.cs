using System;
using System.Drawing;

namespace ImageToCGP
{
    class Conversion
    {
        public static int BeginConversion(string file, int minHeight = 0, int maxHeight = 20)
        {
            Bitmap image = new Bitmap(file);

            Console.WriteLine(image);
            Console.WriteLine(minHeight);
            Console.WriteLine(maxHeight);

            image.SetResolution(16f, 16f);
            for (int x = 0; x <= image.Width; x++)
                for (int y = 0; y <= image.Height; y++)
                {

                }
            return 0;
        }
    }
}
