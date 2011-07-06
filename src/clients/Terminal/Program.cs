using Wpec.Core;

namespace Wpec.Terminal
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         string inpath = @"C:\Users\Dave\Desktop\wordpress.input.xml";
         string outpath = @"C:\Users\Dave\Desktop\wordpress.output.xml";

         WordPressExportConverter converter = new WordPressExportConverter();
         converter.Convert(inpath, outpath);
      }
   }
}