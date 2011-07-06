using System.IO;

namespace Wpec.Terminal
{
   public class Paths
   {
      private FileInfo input;

      public FileInfo Input
      {
         get { return this.input; }
      }

      private FileInfo output;

      public FileInfo Output
      {
         get { return this.output; }
      }

      public bool SetOutput(string outputPath)
      {
         if (outputPath == null)
            return false;

         this.output = new FileInfo(outputPath);

         return OutputIsValid;
      }

      public bool SetInput(string inputPath)
      {
         if (inputPath == null)
            return false;

         this.input = new FileInfo(inputPath);

         return InputIsValid;
      }

      public bool InputIsValid
      {
         get { return this.input != null && this.input.Exists; }
      }

      public bool OutputIsValid
      {
         get { return this.output != null && this.output.Directory.Exists; }
      }
   }
}