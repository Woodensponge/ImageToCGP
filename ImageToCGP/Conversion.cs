using System.Collections;
using System.Drawing;

namespace ImageToCGP;

class Conversion
{
    static int SwitchRatio(byte value, byte oldMin, byte oldMax, int newMin, int newMax)
    {
        int oldRange = oldMin - oldMax;
        int newRange = newMin - newMax;
        return ((value - oldMin) * newRange / oldRange) + newMin;
    }

    // FIXME: This method is awful by itself, like splitPath!!!!!!
    static string GetFileName(string path, bool noExtension = false)
    {
        string[]? splitPath = null;

        if (!path.Contains('\\') && !path.Contains('/'))
        {
            if (noExtension)
            {
                return path.Split('.')[0];
            }
            else if (!noExtension)
            {
                return "";
            }
        }
        else if (path.Contains('\\'))
        {
            splitPath = path.Split('\\');
        }
        else if (path.Contains('/'))
        {
            splitPath = path.Split('/');
        }

        if (noExtension && splitPath != null)
        {
            return splitPath[^1].Split('.')[0];
        }

        return splitPath?[^1] ?? "";
    }

    public static int BeginConversion(string file, bool doInversion = false, int minHeight = 0, int maxHeight = 20)
    {
        //Readjust min and max heights if the user exceeded cyber grind limitations.
        if (maxHeight > 50)
        {
            maxHeight = 50;
        }
        if (minHeight < -50)
        {
            minHeight = -50;
        }

        Bitmap bitmap = new(file);
        Console.WriteLine("Beginning conversion to " + file);

        //Resize bitmap.
        using (Bitmap tempBitmap = new(bitmap, new Size(16, 16)))
        {
            bitmap = new Bitmap(tempBitmap);
        }

        //Stuff for the for loop.
        byte firstPixel = bitmap.GetPixel(0, 15).R;
        byte maxLevel = 0;

        Stack levels = new();

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
                byte avgPixel = (byte)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);

                if (doInversion)
                {
                    avgPixel = (byte)(255 - avgPixel);
                }

                byte iLevel = (byte)Math.Abs(avgPixel - firstPixel);
                maxLevel = Math.Max(maxLevel, iLevel);
                levels.Push(iLevel);
            }
        }

        bitmap.Dispose();

        Stack CGPLevels = new(levels.Count);
        foreach (var obj in levels)
        {
            int cgpLevel = SwitchRatio((byte)obj, 0, maxLevel, minHeight, maxHeight);
            CGPLevels.Push(cgpLevel);
        }

        List<string> lines = [];
        string? line = null;

        int iterator = 1;

        foreach (var obj in CGPLevels)
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
        {
            lines.Add("0000000000000000");
        }
        Console.WriteLine("Completed conversion!");
        if (File.Exists(file))
        {
            Console.WriteLine("Creating a new cgp file...");
        }
        else
        {
            Console.WriteLine("Writing to a file...");
        }

        using (StreamWriter CGPFile = File.CreateText(GetFileName(file, true) + ".cgp"))
        {
            int lineNum = 1;
            foreach (string str in lines)
            {
                if (lineNum == lines.Count)
                {
                    CGPFile.Write(str);
                    break;
                }
                CGPFile.Write(str + "\n");
                lineNum++;
            }
        }

        if (File.Exists(GetFileName(file, true) + ".cgp"))
        {
            Console.WriteLine("CGP successfully written/created!");
        }
        else if (!File.Exists(GetFileName(file, true) + ".cgp"))
        {
            Console.WriteLine("File creation/writing failed!");
        }

        return 0;
    }
}