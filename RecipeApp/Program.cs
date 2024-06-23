using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    internal class Program
    {
        /// <summary>
        /// Entry point for this program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Declare and instantiate a WorkerClass object.
            WorkerClass worker = new WorkerClass();

            // Check if the blue screen must be enabled on startup.
            if(args.Length > 0 && args[0] == "-ENABLE_BLUE_SCREEN")
                worker.EnableBlueScreen();

            // Execute/Run the WorkerClass object.
            worker.Run();
        }
    }
}
