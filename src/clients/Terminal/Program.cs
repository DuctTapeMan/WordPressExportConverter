using System;

using Wpec.Core;

namespace Wpec.Terminal
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         string inpath = @"C:\Users\Dave\Desktop\wordpress.input.xml";
         string outpath = @"C:\Users\Dave\Desktop\wordpress.output.xml";

         try
         {
            WordPressExportConverter converter = new WordPressExportConverter();
            converter.Convert(inpath, outpath);
         }
         catch (Exception exception)
         {
            Console.WriteLine("An unexpected error occurred while trying to convert the file {0}.", inpath);
            Console.WriteLine();
            Console.WriteLine(exception);
         }
      }
   }
}