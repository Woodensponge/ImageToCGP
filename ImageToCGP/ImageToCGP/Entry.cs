using System;

namespace ImageToCGP
{
    class Entry
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || args == null)
            {
                ConsoleOutputPresets.Info();
                return;
            }
            else
            {
                string[] splitArgs = args[0].Split('.');
                Console.WriteLine("File is a " + splitArgs[splitArgs.Length - 1]);
                Conversion.BeginConversion(splitArgs[splitArgs.Length - 1]);
            }

            Console.WriteLine("Press any key to close the process. :)");
            Console.ReadKey();
        }
    }
}
