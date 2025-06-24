using System;
using System.IO;

namespace ImageToCGP;

class Entry
{
    public static int Main(string[] args)
    {
        // TODO: Add GUI
        Console.WriteLine("");

        //End user handling
        if (args.Length == 0 || args == null)
        {
            ConsoleOutputPresets.Info();
            return -1;
        }

        if (args[0] == "help") 
        {
            ConsoleOutputPresets.Info();
            return 0;
        }

        // Invalid first argument
        if (!File.Exists(args[0]))
        {
            Console.WriteLine("File cannot be found.");
            ConsoleOutputPresets.Info();
            return -1;
        }

        // TODO: GET RID OF THESE GODDAMN COMMENTS. IT LOOKS LIKE AN AI'S TRYING
        //       TO EXPLAIN .NET FRAMEWORK BULLSHIT TO A 2 YEAR OLD TEETHING THEIR
        //       KEYBOARD. FUCK.
#if (DEBUG)
        string[] splitArgs = args[0].Split('.');
        Console.WriteLine("File is a " + splitArgs[splitArgs.Length - 1]);
#endif
        int minHeight = 0;
        int maxHeight = 20;

        switch (args.Length)                        //Command switch case (With some end user handling)
        {
            case 1:                                 //If min and max aren't specified...
                //Begin conversion with prespecified min maxes.
                Conversion.BeginConversion(args[0]);
                break;
            case 2:                                 //If a second arg is specified...
                if (args[1] == "true")              //If args[1] is "true"
                    //Begin conversion with inversion.
                    Conversion.BeginConversion(args[0], true);
                else if (args[1] != "true")         //If something else is args[1]...
                {
                    Console.WriteLine("Didn't get \"true\" for inversion. Got other stuff instead.");   //Specify they have to put "true"
                    ConsoleOutputPresets.Info();                                        //Print program information.
                    return -1;                                                          //Exit program prematurely.
                }
                else
                    //Begin Conversion.
                    Conversion.BeginConversion(args[0]);
                break;
            case 3:                                 //If minimum and maximum height are specified...
            case 4:                                 //If minimum and maximum height are specified AND Inversion is true...
                if (!int.TryParse(args[1], out minHeight))                              //If args[1] isn't a number...
                {
                    Console.WriteLine("Please specify an number after [File Name]");    //Specify that they have to use a number.
                    ConsoleOutputPresets.Info();                                        //Print program information.
                    return -1;                                                          //End program prematurely.
                }
                else if (!int.TryParse(args[2], out maxHeight))                         //If args[2] isn't a number...
                {
                    Console.WriteLine("Please specify an number after [File Name]");    //Specify that they have to use a number.
                    ConsoleOutputPresets.Info();                                        //Print program information.
                    return -1;                                                          //End program prematurely.
                }
                if (minHeight >= maxHeight)
                {
                    //Specify that their minimum pattern height is greater then maximum pattern height.
                    Console.WriteLine("Minimum pattern height is greater than or equal to maximum pattern height");
                    ConsoleOutputPresets.Info();                                        //Print program information.
                    return -1;                                                          //End program prematurely.
                }

                if (args.Length >= 4)                                                   //If args.Length is equal to or more then 4...
                {
                    if (args[3] == "true")                                              //If args[3] is true...
                        //Begin conversion with heights that were specified and inversion.
                        Conversion.BeginConversion(args[0], true, minHeight, maxHeight);
                    break;
                }
                //Begin conversion with heights that were specified.
                Conversion.BeginConversion(args[0], false, minHeight, maxHeight);
                break;
        }
        return 0;
    }
}
