using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageToCGP
{
    class Conversion
    {
        static string GetFileName(string path)
        {
            string[] array = null;

            if (!path.Contains("\\") && !path.Contains("/"))
                return null;

            else if (path.Contains("\\"))
                array = path.Split('\\');

            else if (path.Contains("/"))
                array = path.Split('/');

            return array[array.Length - 1];
        }

        public static int BeginConversion(string file, int minHeight = 0, int maxHeight = 20)
        {

            Bitmap bitmap = new Bitmap(file);
            Console.WriteLine("Beginning conversion to " + file);
#if (DEBUG)
            Console.WriteLine("File name: " + GetFileName(file));
            Console.WriteLine("Image width: " + bitmap.Width);
            Console.WriteLine("Image height: " + bitmap.Height);
            Console.WriteLine("Minimum height: " + minHeight);
            Console.WriteLine("Maximum height: " + maxHeight);
#endif
            using (Bitmap tempBitmap = new Bitmap(bitmap, new Size(16, 16)))
            {
                bitmap = new Bitmap(tempBitmap);
            }
#if (DEBUG)
            Console.WriteLine("New image width: " + bitmap.Width);
            Console.WriteLine("New image height: " + bitmap.Height);
#endif

            Color pixel;
            Color firstPixel = bitmap.GetPixel(0, 0);
            int rowCounter = 0;

            /*
             * This for loop starts from the top left corner.
             * For every row the for loop goes through, a new line in the CGP file is being
             * written on.
             */ 
            for (int y = bitmap.Height; y > 0; y--)
                for (int x = bitmap.Width; x > 0; x--)
                { 
                    Console.WriteLine("X: " + x + " Y: " + y);
                }
            return 0;
        }
    }
}
