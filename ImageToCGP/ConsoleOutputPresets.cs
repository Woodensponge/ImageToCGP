using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToCGP;

class ConsoleOutputPresets
{
    public static void Info()
    {
        Console.WriteLine
            ("ImageToCGP" +
            "\n======================================================================" +
            "\nConverts Image files to .cgp files. Used for The Cyber Grind in ULTRAKILL." +
            "\nKeep in mind that the default minimum and maximum pattern heights FOR THE PROGRAM" +
            " are 0 to 20. If your maximum pattern heights exceed or fall below cyber grind limits, " +
            " they will be AUTOMATICALLY ADJUSTED!" +
            "\n\nFOR [Inversion], TYPE IN \"true\" IN LOWERCASE!" +
            "\n\nUsage:" +
            "\n ImageToCGP.exe [File Name]" +
            "\n ImageToCGP.exe [File Name] [Inversion]" +
            "\n ImageToCGP.exe [File Name] [Minimum Pattern Height] [Maximum Pattern Height]" +
            "\n ImageToCGP.exe [File Name] [Minimum Pattern Height] [Maximum Pattern Height] [Inversion]" +
            "\n\nVerified supported files:" +
            "\n .png" +
            "\n .jpg" +
            "\n .bmp" +
            "\n======================================================================");
    }
}