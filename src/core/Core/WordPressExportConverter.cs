using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Wpec.Core
{
   public class WordPressExportConverter
   {
      public void Convert(string inpath, string outpath)
      {
         XmlDocument doc = new XmlDocument();
         doc.Load(inpath);

         XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
         nsmgr.AddNamespace("content", "http://purl.org/rss/1.0/modules/content/");

         XmlNodeList nodes = doc.SelectNodes("/rss/channel/item/content:encoded", nsmgr);

         foreach (XmlNode n in nodes)
         {
            string newText = ProcessBlogPost(n.InnerText);
            n.InnerText = null;
            n.AppendChild(doc.CreateCDataSection(newText));
         }

         doc.Save(outpath);

         Console.WriteLine("Done");
         Console.ReadLine();
      }

      private static Regex findImgTag = new Regex("<img", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      private static Regex findEndImg = new Regex("/>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      private static string ProcessBlogPost(string blogPost)
      {
         StringBuilder output = new StringBuilder();
         int pos = 0;
         while (true)
         {
            Match startImg = findImgTag.Match(blogPost, pos);
            if (!startImg.Success)
            {
               output.Append(blogPost.Substring(pos));
               break;
            }
            else
            {
               output.Append(blogPost.Substring(pos, startImg.Index - pos));
               Match endImg = findEndImg.Match(blogPost, startImg.Index);
               pos = endImg.Index + endImg.Length;
               string imgTag = blogPost.Substring(startImg.Index, pos - startImg.Index);

               ImgTagProcessor p = new ImgTagProcessor(imgTag);
               output.Append(p.Process());
            }
         }
         return output.ToString();
      }
   }
}