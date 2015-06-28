#if DEBUG
using Hext.Linq;
using HtmlAgilityPack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hext.Tests
{
    [TestFixture]
    public class Reddit
    {
        [TestCase]
        public void TopRedditHeadlines()
        {
            string with = String.Join(Environment.NewLine, TopRedditHeadlines_WithHext());
            string without = String.Join(Environment.NewLine, TopRedditHeadlines_WithoutHext());

            Debug.WriteLine(with);
            Debug.WriteLine(without);

            Assert.That(with == without);
        }

        private IEnumerable<string> TopRedditHeadlines_WithHext()
        {
            using (var client = new HttpClient())
            {
                string html = Task.Run(() => client.GetStringAsync("http://reddit.com")).Result;

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var table = doc
                    .Body()
                    .ChildNodes.WithClass("content")
                    .ChildNodes.Last(node => node.Class() == "spacer")
                    .ChildNodes.WithId("siteTable");

                var posts = table.SortChildren(child => child.Class().Contains("thing"));

                foreach (var post in posts)
                {
                    string title = post
                        .ChildNodes.WithClass("entry unvoted")
                        .Element("p")
                        .Element("a")
                        .InnerText;

                    yield return title;
                }
            }
        }

        private IEnumerable<string> TopRedditHeadlines_WithoutHext()
        {
            using (var client = new HttpClient())
            {
                string html = Task.Run(() => client.GetStringAsync("http://reddit.com")).Result;
                
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var table = doc
                    .DocumentNode
                    .Element("html")
                    .Element("body")
                    .ChildNodes.First(node => node.Attributes["class"]?.Value == "content")
                    .ChildNodes.Last(node => node.Attributes["class"]?.Value == "spacer")
                    .ChildNodes.First(node => node.Id == "siteTable");

                var posts = table.ChildNodes.Where(child =>
                {
                    string @class = child.Attributes["class"]?.Value;

                    if (@class == null)
                        return false;

                    return @class.Contains("thing");
                });

                foreach (var post in posts)
                {
                    string title = post
                        .ChildNodes.First(node => node.Attributes["class"]?.Value == "entry unvoted")
                        .Element("p")
                        .Element("a")
                        .InnerText;

                    yield return title;
                }
            }
        }
    }
}
#endif