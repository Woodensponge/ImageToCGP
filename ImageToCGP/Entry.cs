namespace ImageToCGP;

class Entry
{
    public static void PrintHelp()
    {
        Console.WriteLine
        (
            "ImageToCGP" +
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
            "\n======================================================================"
        );
    }

    public static int Main(string[] args)
    {
        // TODO: Add GUI
        Console.WriteLine("");

        //End user handling
        if (args.Length == 0 || args == null)
        {
            PrintHelp();
            return -1;
        }

        if (args[0] == "help")
        {
            PrintHelp();
            return 0;
        }

        // Invalid first argument
        if (!File.Exists(args[0]))
        {
            Console.WriteLine("File cannot be found.");
            PrintHelp();
            return -1;
        }

        switch (args.Length)
        {
            case 1:
                Conversion.BeginConversion(args[0]);
                break;
            case 2:
                if (args[1] == "true")
                    Conversion.BeginConversion(args[0], true);
                else if (args[1] != "true")
                {
                    Console.WriteLine("Didn't get \"true\" for inversion. Got other stuff instead.");
                    PrintHelp();
                    return -1;
                }
                else
                {
                    Conversion.BeginConversion(args[0]);
                }
                break;
            case 3:
            case 4:
                int minHeight;
                int maxHeight;
                if (!int.TryParse(args[1], out minHeight))
                {
                    Console.WriteLine("Please specify an number after [File Name]");
                    PrintHelp();
                    return -1;
                }
                else if (!int.TryParse(args[2], out maxHeight))
                {
                    Console.WriteLine("Please specify an number after [File Name]");
                    PrintHelp();
                    return -1;
                }
                if (minHeight >= maxHeight)
                {
                    //Specify that their minimum pattern height is greater then maximum pattern height.
                    Console.WriteLine("Minimum pattern height is greater than or equal to maximum pattern height");
                    PrintHelp();
                    return -1;
                }

                if (args.Length >= 4)
                {
                    if (args[3] == "true")
                    {
                        Conversion.BeginConversion(args[0], true, minHeight, maxHeight);
                    }
                    break;
                }
                Conversion.BeginConversion(args[0], false, minHeight, maxHeight);
                break;
        }
        return 0;
    }
}
