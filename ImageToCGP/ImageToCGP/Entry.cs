using System;
using System.IO;

namespace ImageToCGP
{
    class Entry
    {
        public static int Main(string[] args)
        {
            //End user handling
            if (args.Length == 0 || args == null)                       //If no arguments were specified...
            {
                ConsoleOutputPresets.Info();                            //Print program information.
                return -1;                                              //End program prematurely.
            }

            if (args[0] == "help")                                      //IF arg is "help"...
            {
                ConsoleOutputPresets.Info();                            //Print program information.
                return 0;                                               //End program.
            }

            if (!File.Exists(args[0]))                                  //If path given is defunct...
            {
                Console.WriteLine("File cannot be found.");             //Specify that the file can't be found.
                ConsoleOutputPresets.Info();                            //Print program information.
                return -1;                                              //End program prematurely.
            }

#if(DEBUG)
            string[] splitArgs = args[0].Split('.');
            Console.WriteLine("File is a " + splitArgs[splitArgs.Length - 1]);
#endif
            int minHeight = 0;
            int maxHeight = 20;

            switch (args.Length)
            {
                case 1:                                 //If minimum height isn't specified...
                    //Begin conversion with pre specified min maxes.
                    Conversion.BeginConversion(args[0]);
                    break;
                case 2:                                 //If minimum height is specified but maximum height isn't...
                    //Attempt to parse args[1]
                    if (!int.TryParse(args[1], out minHeight))                              //If args[1] isn't a number...
                    {
                        Console.WriteLine("Please specify an number after [File Name]");    //Specify that they have to use a number.
                        ConsoleOutputPresets.Info();                                        //Print program information.
                        return -1;                                                          //End program prematurely.
                    }

                    //Begin conversion with minimum height.
                    Conversion.BeginConversion(args[0], minHeight);
                    break;
                case 3:                                 //If minimum and maximum height is specified...
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

                    //Begin conversion with heights that were specified.
                    Conversion.BeginConversion(args[0], minHeight, maxHeight);
                    break;
            }
            Console.WriteLine("Press any key to close the process. :)");
            Console.ReadKey();
            return 0;
        }
    }
}
