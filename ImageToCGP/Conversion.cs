using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
        static string GetFileName(string path, bool noExtension = false)
        {
            string[] array = null;

            if (!path.Contains("\\") && !path.Contains("/"))
            {
                if (noExtension)
                    return path.Split('.')[0];
                else if (!noExtension)
                    return null;
            }

            else if (path.Contains("\\"))
                array = path.Split('\\');

            else if (path.Contains("/"))
                array = path.Split('/');

            if(noExtension)
                return array[array.Length - 1].Split('.')[0];

            return array[array.Length - 1];
        }

        public static int BeginConversion(string file, bool doInversion = false, int minHeight = 0, int maxHeight = 20)
        {
            //Readjust min and max heights if the user exceeded cyber grind limitations.
            if (maxHeight > 50)
                maxHeight = 50;
            if (minHeight < -50)
                minHeight = -50;

            Bitmap bitmap = new Bitmap(file);                       //Create a bitmap.
            Console.WriteLine("Beginning conversion to " + file);   //Annouce conversion.

            //Resize bitmap.
            using (Bitmap tempBitmap = new Bitmap(bitmap, new Size(16, 16)))
            {
                bitmap = new Bitmap(tempBitmap);
            }

            //Stuff for the for loop.
            byte pixel = 0;                             //Current pixel being read.
            byte firstPixel = bitmap.GetPixel(0, 15).R; //Top left pixel in the image.
            byte maxLevel = 0;

            Stack levels = new Stack();

            /*
             * This for loop starts from the bottom right corner.
             * This loop finds the difference between the first pixel in the image
             * (top left), and finds the "level", then pushes that "level" to the stack.
             * 
             * Keep in mind that I'm NOT reading off of only one color value, which is red. 
             * This is NOT the same as reading of a greyscale image FUCK FUCK FUCK FUCK FUCK FUCK FUCK FUCK
             */
            for (int y = 0; y <= bitmap.Height - 1; y++)
            {
                for (int x = 0; x <= bitmap.Width - 1; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    pixel = (byte)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);                    //Get the average value of the pixel.

                    if (doInversion)
                    {
                        pixel = (byte)(255 - pixel);
                    }

                    byte iLevel = (byte)Math.Abs(pixel - firstPixel);
                    maxLevel = (byte)Math.Max(maxLevel, iLevel);
                    levels.Push(iLevel);
                }
            }

            bitmap.Dispose();                                           //We don't need the bitmap object anymore.

            /*
             * Now that we found the levels, we'll convert them to .cgp acceptable values, while including
             * the max and min height the user specified, if they did.
             */
            Stack CGPLevels = new Stack(levels.Count);
            foreach (Object obj in levels)
            {
                int cgpLevel = SwitchRatio((byte)obj, 0, maxLevel, minHeight, maxHeight);
                CGPLevels.Push(cgpLevel);
            }

            List<string> lines = new List<string>();
            string line = null;

            int iterator = 1;

            foreach (Object obj in CGPLevels)
            {
                string level = ((int)obj).ToString();
                if ((int)obj >= 10 || (int)obj < 0)
                {
                    line += String.Format("({0})", level);
                }
                else
                {
                    line += level;
                }

                if (iterator == 16)
                {
                    iterator = 1;
                    lines.Add(line);
                    line = null;
                    continue;
                }

                iterator++;
            }

            //Rest of the stuff is prefabs. Make everything else blank.

            lines.Add("");

            for (int i = 0; i <= 15; i++)
                lines.Add("0000000000000000");

                Console.WriteLine("Completed conversion!");
            if (File.Exists(file))
                Console.WriteLine("Creating a new cgp file...");
            else
                Console.WriteLine("Writing to a file...");

            //For some reason, lines.ToString() comes up with System.Collections.Generic.List`1[System.String]. Fucking whatever.
            using (StreamWriter CGPFile = File.CreateText(GetFileName(file, true) + ".cgp"))
            {
                int lineNum = 1;                    //Fuck you WriteLine()
                foreach (String str in lines)
                {
                    if (lineNum == lines.Count)     //If lineNum is equal to lines.Count...
                    {
                        CGPFile.Write(str);         //Write a line without adding another fucking space.
                        break;
                    }
                    CGPFile.Write(str + "\n");
                    lineNum++;
                }
            }

            if (File.Exists(GetFileName(file, true) + ".cgp"))
                Console.WriteLine("CGP successfully written/created!");

            else if (!File.Exists(GetFileName(file, true) + ".cgp"))
                Console.WriteLine("File creation/writing failed!");

            return 0;
        }
    }
}
