using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hext
{
    public static class Docext
    {
        public static HtmlNode Html(this HtmlDocument doc)
        {
            return doc.DocumentNode.Element("html");
        }

        public static HtmlNode Head(this HtmlDocument doc)
        {
            return doc.Html().Element("head");
        }

        public static HtmlNode Body(this HtmlDocument doc)
        {
            return doc.Html().Element("body");
        }
    }
}
