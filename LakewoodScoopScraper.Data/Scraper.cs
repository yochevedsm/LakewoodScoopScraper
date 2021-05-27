using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace LakewoodScoopScraper.Data
{
    public static class Scraper
    {

            public static List<LSElement> ScrapeLakewoodScoop()
            {
                var results = new List<LSElement>();
                var html = GetHtml();
                var parser = new HtmlParser();
                var document = parser.ParseDocument(html);
                IHtmlCollection<IElement> searchResultElements = document.QuerySelectorAll(".post");
                foreach (IElement result in searchResultElements)
                {
                var LSElement = new LSElement();
                var titleSpan = result.QuerySelector("h2");
                   
                  
                    LSElement.Title = titleSpan.TextContent;

                    var url = titleSpan.QuerySelector("a");
                    if (url != null)
                    {
                        LSElement.Url = url.Attributes["href"].Value; ;
                    }

                    var imageElement = result.QuerySelector("img");
                    if (imageElement != null)
                    {
                        var imageSrcValue = imageElement.Attributes["src"].Value;
                        LSElement.Image = imageSrcValue;
                    }

                var textBuiltUp = "";
                var textSpan = result.QuerySelectorAll("p");
                foreach (var text in textSpan)
                {
                    textBuiltUp += text.TextContent.Replace("Read more ›", String.Empty);
                }

                LSElement.Blurb = textBuiltUp;

                var comments = result.QuerySelector("div.backtotop");
                LSElement.Comments = comments.TextContent;

               

                results.Add(LSElement);


                }

                return results;
            }

        public static string GetHtml()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            string url = "https://www.thelakewoodscoop.com/";
            var client = new HttpClient(handler);
            var html = client.GetStringAsync(url).Result;
            return html;
        }


    }
}
