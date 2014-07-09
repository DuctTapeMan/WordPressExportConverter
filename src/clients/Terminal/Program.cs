using System;

using Wpec.Core;

namespace Wpec.Terminal
{
   internal static class Program
   {
      private static Paths GetPaths(string[] args)
      {
         Paths paths = new Paths();

         if (args != null && args.Length > 0)
         {
            paths.SetInput(args[0]);

            if (args.Length > 1)
               paths.SetOutput(args[1]);
         }

         while (!paths.InputIsValid)
         {
            if (paths.Input != null)
               Console.WriteLine("The file '{0}' could not be found.", paths.Input);

            Console.WriteLine("Please enter the path to the input XML file and press enter:");

            if (paths.SetInput(Console.ReadLine()))
               break;

            Console.WriteLine();
         }

         while (!paths.OutputIsValid)
         {
            Console.WriteLine();

            if (paths.Output != null)
               Console.WriteLine("The directory of '{0}' could not be found.", paths.Output);

            Console.WriteLine("Please enter the path to the output XML file and press enter:");

            if (paths.SetOutput(Console.ReadLine()))
               break;

            Console.WriteLine();
         }

         return paths;
      }


      private static void Main(string[] args)
      {
         Paths paths = GetPaths(args);

         try
         {
            WordPressExportConverter converter = new WordPressExportConverter();
            converter.Convert(paths.Input.FullName, paths.Output.FullName);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done");
         }
         catch (Exception exception)
         {
            Console.WriteLine("An unexpected error occurred while trying to convert the file {0}.", paths.Input);
            Console.WriteLine();
            Console.WriteLine(exception);
         }

         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine("Press any key to exit.");
         Console.ReadKey();
      }
   }
}