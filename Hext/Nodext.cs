using HtmlAgilityPack;
using Hext.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hext
{
    public static class Nodext
    {
        #region Attachment
        public static void AppendChildren(this HtmlNode node, IEnumerable<HtmlNode> newChildren)
        {
            foreach (var child in newChildren)
            {
                node.AppendChild(child);
            }
        }

        public static void PrependChildren(this HtmlNode node, IEnumerable<HtmlNode> newChildren)
        {
            foreach (var child in newChildren)
            {
                node.PrependChild(child);
            }
        }
        #endregion

        #region Ancestors
        public static HtmlNode Ancestor(this HtmlNode node, string name)
        {
            foreach (var ancestor in node.Ancestors(name))
            {
                // .Ancestors(name) works by yield so 
                // this performs just as well as .FirstOrDefault
                return ancestor;
            }

            return null;
        }

        public static HtmlNode AncestorAnamed(this HtmlNode node, string name)
        {
            return node.FirstAncestor(AnameMatches(name));
        }

        public static IEnumerable<HtmlNode> AncestorsAnamed(this HtmlNode node, string name)
        {
            return node.SortAncestors(AnameMatches(name));
        }
        #endregion

        #region Children
        // Child() and Children() DELETED. Use Element() or Elements() instead.

        public static HtmlNode ChildAnamed(this HtmlNode node, string name)
        {
            return node.FirstElement(AnameMatches(name));
        }

        public static IEnumerable<HtmlNode> ChildrenAnamed(this HtmlNode node, string name)
        {
            return node.SortChildren(AnameMatches(name));
        }
        #endregion

        #region Descendants
        public static HtmlNode Descendant(this HtmlNode node, string name)
        {
            foreach (var descendant in node.Descendants(name))
            {
                return descendant;
            }

            return null;
        }

        public static HtmlNode DescendantAnamed(this HtmlNode node, string name)
        {
            return node.FirstDescendant(AnameMatches(name));
        }

        public static IEnumerable<HtmlNode> DescendantsAnamed(this HtmlNode node, string name)
        {
            return node.SortDescendants(AnameMatches(name));
        }
        #endregion

        #region Common attributes
        public static string Attribute(this HtmlNode node, string name)
        {
            return node.GetAttributeValue(name, String.Empty);
        }

        public static string Aname(this HtmlNode node)
        {
            return node.Attribute("name");
        }

        public static void SetAname(this HtmlNode node, string value)
        {
            node.SetAttributeValue("name", value);
        }

        public static string Value(this HtmlNode node)
        {
            return node.Attribute("value");
        }

        public static void SetValue(this HtmlNode node, string value)
        {
            node.SetAttributeValue("value", value);
        }

        public static string Form(this HtmlNode node)
        {
            return node.Attribute("form");
        }

        public static void SetForm(this HtmlNode node, string value)
        {
            node.SetAttributeValue("form", value);
        }

        public static string Class(this HtmlNode node)
        {
            return node.Attribute("class");
        }

        public static void SetClass(this HtmlNode node, string value)
        {
            node.SetAttributeValue("class", value);
        }
        #endregion

        public static HtmlNode DocumentNode(this HtmlNode node)
        {
            return node.OwnerDocument.DocumentNode;
        }

        public static HtmlNode DocumentHtml(this HtmlNode node)
        {
            return node.OwnerDocument.Html();
        }

        public static HtmlNode DocumentHead(this HtmlNode node)
        {
            return node.OwnerDocument.Head();
        }

        public static HtmlNode DocumentBody(this HtmlNode node)
        {
            return node.OwnerDocument.Body();
        }

        public static bool IsEmpty(this HtmlNode node)
        {
            return String.IsNullOrWhiteSpace(node.InnerHtml);
        }

        private static Func<HtmlNode, bool> AnameMatches(string name)
        {
            return node => node.Aname() == name;
        }
    }
}
