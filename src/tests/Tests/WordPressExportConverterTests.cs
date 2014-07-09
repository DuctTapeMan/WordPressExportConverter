using System;

using NUnit.Framework;

using Wpec.Core;

namespace Wpec.Tests
{
   [TestFixture]
   public class WordPressExportConverterTests
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         this.converter = new WordPressExportConverter();
      }

      #endregion

      private WordPressExportConverter converter;


      [Test]
      public void Convert_NullInPath_ThrowsArgumentNullException()
      {
         TestDelegate throwing = () => this.converter.Convert(null, "/path/to/out/file");

         Assert.That(
            throwing,
            Throws.TypeOf<ArgumentNullException>()
               .With.Property("ParamName").StringContaining("inpath"));
      }


      [Test]
      public void Convert_NullOutPath_ThrowsArgumentNullException()
      {
         TestDelegate throwing = () => this.converter.Convert("/path/to/in/file", null);

         Assert.That(
            throwing,
            Throws.TypeOf<ArgumentNullException>()
               .With.Property("ParamName").StringContaining("outpath"));
      }
   }
}