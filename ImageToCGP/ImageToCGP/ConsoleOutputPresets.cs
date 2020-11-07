using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToCGP
{
    class ConsoleOutputPresets
    {
        public static void Info()
        {
            //TODO: Add file formats as you develop this.
            Console.WriteLine
                ("ImageToCGP" +
                "\n======================================================================" +
                "\nConverts Image files to .cgp files. Used for The Cyber Grind in ULTRAKILL." +
                "\n\nUsage:" +
                "\n ImageToCGP.exe [File Name]" +
                "\n ImageToCGP.exe [File Name] [Minimum Pattern Height] [Maximum Pattern Height]" +
                "\n\nVerified supported files:" +
                "\n .png" +
                "\n .jpg" +
                "\n .bmp" +
                "\n======================================================================");
        }
    }
}
