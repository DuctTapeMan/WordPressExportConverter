using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wpec.Core
{
   class ImgTagProcessor
   {
      static Regex findAtts = new Regex(@"(?<Att>\w+)=""(?<Value>[^""]*)""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      static Regex queryW = new Regex(@"w=(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      static Regex queryH = new Regex(@"h=(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      string imgTag;
      string width;
      string height;


      internal ImgTagProcessor(string imgTag)
      {
         this.imgTag = imgTag;
      }

      internal string Process()
      {
         // Extract width and height info
         foreach (Match m in findAtts.Matches(imgTag))
         {
            switch (m.Groups["Att"].Value)
            {
               case "width":
                  this.width = m.Groups["Value"].Value;
                  break;
               case "height":
                  this.height = m.Groups["Value"].Value;
                  break;
               case "src":
                  Uri uri = new Uri(m.Groups["Value"].Value);
                  string query = uri.Query;
                  if (!String.IsNullOrEmpty(query))
                  {
                     Match matchW = queryW.Match(query);
                     Match matchH = queryH.Match(query);
                     if (matchW.Success)
                        width = matchW.Groups[1].Value;
                     if (matchH.Success)
                        height = matchH.Groups[1].Value;
                  }
                  break;
            }
         }
         return findAtts.Replace(imgTag, new MatchEvaluator(EvaluateAttributeMatch));
      }

      string EvaluateAttributeMatch(Match m)
      {
         switch (m.Groups["Att"].Value)
         {
            case "src":
               UriBuilder uri = new UriBuilder(m.Groups["Value"].Value);
               List<string> queryItems = new List<string>();
               if (width != null)
                  queryItems.Add("w=" + width);
               if (height != null)
                  queryItems.Add("h=" + height);
               uri.Query = String.Join("&", queryItems.ToArray());
               return "src=\"" + uri.ToString() + "\"";
            default:
               return m.Value;
         }
      }
   }
}