using System;
using System.Drawing;

namespace ImageToCGP
{
    class Conversion
    {
        public static int BeginConversion(string file, int minHeight = 0, int maxHeight = 20)
        {
            Bitmap image = new Bitmap(file);
            Console.WriteLine("Beginning conversion to " + file);
#if(DEBUG)
            Console.WriteLine("Image width: " + image.Width);
            Console.WriteLine("Image height: " + image.Height);
            Console.WriteLine("Minimum height: " + minHeight);
            Console.WriteLine("Maximum height: " + maxHeight);
#endif
            image.SetResolution(16f, 16f);
            for (int x = 0; x <= image.Width; x++)
                for (int y = 0; y <= image.Height; y++)
                {

                }
            return 0;
        }
    }
}
