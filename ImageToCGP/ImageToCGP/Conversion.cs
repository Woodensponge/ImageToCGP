using System;
using System.Collections;
using System.Drawing;

namespace ImageToCGP
{
    class Conversion
    {
        static int SwitchRatio(byte value, byte oldMin, byte oldMax, int newMin, int newMax)
        {
            int oldRange = (oldMin - oldMax);
            int newRange = (newMin - newMax);
            return (((value - oldMin) * newRange) / oldRange) + newMin;
        }
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
            //Readjust min and max heights if the user exceeded cyber grind limitations.
            if (maxHeight > 50)
                maxHeight = 50;
            if (minHeight < -50)
                minHeight = -50;

            Bitmap bitmap = new Bitmap(file);                       //Create a bitmap.
            Console.WriteLine("Beginning conversion to " + file);   //Annouce conversion.
#if (DEBUG)
            Console.WriteLine("File name: " + GetFileName(file));
            Console.WriteLine("Image width: " + bitmap.Width);
            Console.WriteLine("Image height: " + bitmap.Height);
            Console.WriteLine("Minimum height: " + minHeight);
            Console.WriteLine("Maximum height: " + maxHeight);
#endif
            //Resize bitmap.
            using (Bitmap tempBitmap = new Bitmap(bitmap, new Size(16, 16)))
            {
                bitmap = new Bitmap(tempBitmap);
            }
#if (DEBUG)
            Console.WriteLine("New image width: " + bitmap.Width);
            Console.WriteLine("New image height: " + bitmap.Height);
#endif
            //Stuff for the for loop.
            byte pixel = 0;                             //Current pixel being read.
            byte firstPixel = bitmap.GetPixel(0, 15).R; //Top left pixel in the image.
            byte maxLevel = 0;

            Stack levels = new Stack();

            /*
             * This for loop starts from the top left corner.
             * This loop finds the difference between the first pixel in the image
             * (top left), and finds the "level", then pushes that "level" to the stack.
             * 
             * Keep in mind that I'm reading off of only one color value, which is red. 
             * This is essensially the same as reading of a greyscale image.
             */
            for (int y = bitmap.Height - 1; y >= 0; y--)    //HOLY SHIT THE ACTUAL FOR LOOPS LETS FUCKING GOOOOOO
                for (int x = 0; x <= bitmap.Width - 1; x++)
                {
                    pixel = bitmap.GetPixel(x, y).R;                    //Get the current value of the pixel.
                    byte iLevel = (byte)Math.Abs(pixel - firstPixel);
                    maxLevel = (byte)Math.Max(maxLevel, iLevel);
                    levels.Push(iLevel);
                }
            /*
             * Now that we found the levels, we'll convert them to .cgp acceptable values, while including
             * the max and min height the user specified, if they did.
             */
            Stack CGPLevels = new Stack(levels.Count);
            foreach (Object obj in levels)
            {
                int cgpLevel = SwitchRatio((byte)obj, 0, maxLevel, minHeight, maxHeight);
                Console.WriteLine(cgpLevel);
                CGPLevels.Push(cgpLevel);
            }

            //TODO: Make the program write to a file.

            bitmap.Dispose();

            Console.WriteLine("Completed conversion!");
            return 0;
        }
    }
}
