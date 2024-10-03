using HtmlAgilityPack;

namespace Shared.Helpers
{
    public static class HTMLAgilityPackHelpers
    {

        public static string GetTextOnlyFromDiv(HtmlNode node)
        { 
            var result = node?.ChildNodes
                .Where(n => n.NodeType == HtmlNodeType.Text)
                .Select(n => n.InnerText.Trim())
                .Where(text => !string.IsNullOrEmpty(text))
                .FirstOrDefault() ?? "";

            //If the inner text is empty, we probably have nested elements, try using .Descendants to filter them out
            if (String.IsNullOrEmpty(result))
            { 
                result = node?.ChildNodes
                .Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Text)
                .Select(n => n.InnerText.Trim())
                .Where(text => !string.IsNullOrEmpty(text))
                .FirstOrDefault() ?? "";
            }

            return result;
        }
    }
}
