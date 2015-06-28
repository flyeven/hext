using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hext.Linq
{
    public static class Linqext
    {
        #region Predicate wrappers
        public static HtmlAttribute FirstAttribute(this HtmlNode node, Func<HtmlAttribute, bool> pred)
        {
            return node.Attributes.FirstOrDefault(pred);
        }

        public static HtmlNode FirstAncestor(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.Ancestors().FirstOrDefault(pred);
        }

        public static HtmlNode FirstElement(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.ChildNodes.FirstOrDefault(pred);
        }

        public static HtmlNode FirstDescendant(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.Descendants().FirstOrDefault(pred);
        }

        public static IEnumerable<HtmlAttribute> SortAttributes(this HtmlNode node, Func<HtmlAttribute, bool> pred)
        {
            return node.Attributes.Where(pred);
        }

        public static IEnumerable<HtmlNode> SortAncestors(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.Ancestors().Where(pred);
        }

        public static IEnumerable<HtmlNode> SortChildren(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.ChildNodes.Where(pred);
        }

        public static IEnumerable<HtmlNode> SortDescendants(this HtmlNode node, Func<HtmlNode, bool> pred)
        {
            return node.Descendants().Where(pred);
        }
        #endregion

        #region IEnumerable filters
        public static IEnumerable<T> OfClass<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Class() == value);
        }

        public static IEnumerable<T> OfForm<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Form() == value);
        }

        public static IEnumerable<T> OfName<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Name == value);
        }

        public static IEnumerable<T> OfAname<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Aname() == value);
        }

        public static IEnumerable<T> OfId<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Id == value);
        }

        public static IEnumerable<T> OfValue<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.Where(node => node.Value() == value);
        }
        #endregion

        #region IEnumerable selectors
        public static T WithClass<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Class() == value);
        }

        public static T WithForm<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Form() == value);
        }

        public static T WithName<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Name == value);
        }

        public static T WithAname<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Aname() == value);
        }

        public static T WithId<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Id == value);
        }

        public static T WithValue<T>(
            this IEnumerable<T> nodes, string value) where T : HtmlNode
        {
            return nodes.FirstOrDefault(node => node.Value() == value);
        }
        #endregion
    }
}
