using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace SumUpApp.Util.Engine
{
    public class SearchEngine
    {
        public static List<Article> SearchGoogle(String aKeyword, int aPageNumber, int aQuantity)
        {
            List<Article> articles = new List<Article>();
            //Create search string            
            int startIndex = (aPageNumber - 1) * 10 + 1;
            string urlTemplate = "https://www.googleapis.com/customsearch/v1?key="
                    + KeyManager.GoogleKey + "&cx=" + KeyManager.CustomSearchID + "&q=" + aKeyword + "&num=" + aQuantity + "&start=" + startIndex + "&alt=json";
            //Get JSON content
            WebClient c = new WebClient();
            string jsonString = Encoding.UTF8.GetString(c.DownloadData(urlTemplate)).Replace("\r\n", "").Trim();
            JObject jResults = JObject.Parse(jsonString);
            IList<JToken> results = jResults["items"].Children().ToList();
            foreach (JToken result in results)
            {
                Article article = new Article(
                    result["title"].ToString(), result["link"].ToString(), result["snippet"].ToString(), "Google");
                articles.Add(article);
            }

            return articles;
        }

        public static List<Article> SearchBing(String aKeyword, int aPageNumber, int aQuantity)
        {
            List<Article> articles = new List<Article>();
            //Create search string            
            string urlTemplate = "http://api.bing.net/json.aspx?Appid="
                    + KeyManager.BingID + "&query=" + aKeyword + "&web.count=" + aQuantity + "&web.offset=" + (aPageNumber - 1) * 10 + "&sources=web";
            //Get JSON content
            WebClient c = new WebClient();
            string jsonString = Encoding.UTF8.GetString(c.DownloadData(urlTemplate)).Replace("\r\n", "").Trim();
            JObject jResults = JObject.Parse(jsonString);
            IList<JToken> results = jResults["SearchResponse"]["Web"]["Results"].Children().ToList();
            foreach (JToken result in results)
            {
                string title, url, description;
                title = result["Title"] != null ? result["Title"].ToString() : "";
                url = result["Url"] != null ? result["Url"].ToString() : "";
                description = result["Description"] != null ? result["Description"].ToString() : "";

                Article article = new Article(title, url, description, "Bing");
                articles.Add(article);
            }

            return articles;
        }

        public static List<Article> Search(String aKeyword, int aPageNumber, int aQuantity)
        {
            List<Article> articles = new List<Article>();
            List<Article> articlesGoogle = SearchEngine.SearchGoogle(aKeyword, aPageNumber, aQuantity);
            List<Article> articlesBing = SearchEngine.SearchBing(aKeyword, aPageNumber, aQuantity);

            //Lấy phần chung
            for (int i = 0; i < articlesGoogle.Count; ++i)
            {
                bool flag = false;
                for (int j = 0; j < articlesBing.Count; ++j)
                {
                    if (articlesGoogle[i].Link == articlesBing[j].Link)
                    {
                        articlesGoogle[i].Source = "Google/Bing";
                        articles.Add(articlesGoogle[i]);
                        articlesBing.RemoveAt(j); j--;
                        flag = true;
                    }
                }
                if (flag)
                {
                    articlesGoogle.RemoveAt(i); i--;
                }
            }
            //Lấy phần riêng của Google
            for (int i = 0; i < articlesGoogle.Count; ++i)
            {
                articles.Add(articlesGoogle[i]);
            }
            //Lấy phần riêng của Bing
            for (int i = 0; i < articlesBing.Count; ++i)
            {
                articles.Add(articlesBing[i]);
            }

            return articles;
        }
    }
}