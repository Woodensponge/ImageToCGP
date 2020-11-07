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
                "\nConverts Image files to .cgp files. Used for Cyber Grind in ULTRAKILL." +
                "\nSupported files:" +
                "\n .dreamed" +
                "\n======================================================================");

        }
    }
}
