# Hext
A set of **ext**ensions to the **H**TML Agility Pack library designed to make your code more readable, maintainable, and concise.

Hext allows you to quickly find the value of common attributes, jump straight to the document body, and tersely sort a node's relatives.

## Example
Let's try scraping headlines from the front page of Reddit.

### With Hext

    public IEnumerable<string> RedditHeadLines()
    {
        string html;
        using (var client = new WebClient())
            html = client.DownloadString("http://reddit.com");
        
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        var table = doc.Body()
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

### Without Hext

    public IEnumerable<string> RedditHeadLines()
    {
        string html;
        using (var client = new WebClient())
            client.DownloadString("http://reddit.com");
    
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

If you were able to read and understand that last snippet without your eyes glazing over, then I *sincerely* congratulate you.

## Get started
Hext comprises 3 main classes: `Docext`, `Nodext`, and `Linqext`. The first two are in namespace `Hext`, but to use the LINQ extensions you must add a `using` directive for `Hext.Linq`. There is no need to call the methods directly- if your IDE is set up correctly then you should see the extension methods when you type a `.` after the name of your `HtmlNode`.

Happy scraping!
