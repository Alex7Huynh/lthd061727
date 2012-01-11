using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;

namespace SumUpApp.RSS
{
    public partial class SportNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string day, time;
            Article art = new Article();
            Article art1 = new Article();
            Article art2 = new Article();
            Article art3 = new Article();
            Article art4 = new Article();
            List<Article> la = new List<Article>();
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument hd = hw.Load("http://vnexpress.net/gl/the-thao/");
            HtmlNode hn = hd.DocumentNode;

            HtmlNode parentNode = hd.DocumentNode.SelectSingleNode("//div[@class='folder-top']");
            art.Url = parentNode.SelectSingleNode(".//a").Attributes["href"].Value.Trim();
            art.Title = parentNode.SelectSingleNode(".//p").SelectSingleNode(".//a").InnerText.Trim();
            day = parentNode.SelectSingleNode(".//p").SelectSingleNode(".//label[@class='item-date']").InnerText.Trim();
            time = parentNode.SelectSingleNode(".//p").SelectSingleNode(".//label[@class='item-time']").InnerText.Trim();
            try
            {
                art.Content = parentNode.ChildNodes[5].InnerText.Trim();
                art.Image = parentNode.SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value.Trim();
            }
            catch
            {
                art.Content = parentNode.ChildNodes[3].InnerText.Trim();
                art.Image = null;
            }
            art.Url = "http://vnexpress.net/" + art.Url;
            art.Image = "http://vnexpress.net/" + art.Image;
            day = day.Replace("&nbsp;", "");
            //day = day.Replace("|", "");
            art.Date = time + day;
            la.Add(art);

            HtmlAgilityPack.HtmlDocument hd1 = hw.Load("http://baodatviet.vn/Home/thethao.datviet");
            HtmlNode hn1 = hd1.DocumentNode;
            HtmlNode parentNode1 = hd1.DocumentNode.SelectSingleNode("//div[@class='categories_item_small']");
            art1.Url = parentNode1.SelectSingleNode(".//a").Attributes["href"].Value.Trim();
            art1.Title = parentNode1.SelectSingleNode(".//div[@class='categories_item_small_title']").InnerText.Trim();
            art1.Date = parentNode1.SelectSingleNode(".//div[@class='categories_item_small_datetime']").InnerText.Trim();
            art1.Content = parentNode1.SelectSingleNode(".//div[@class='categories_item_small_summary']").InnerText.Trim();
            art1.Image = parentNode1.SelectSingleNode(".//a").SelectSingleNode(".//img").Attributes["src"].Value.Trim();
            la.Add(art1);

            HtmlAgilityPack.HtmlDocument hd2 = hw.Load("http://www.tienphong.vn/The-Thao/Index.html");
            HtmlNode hn2 = hd2.DocumentNode;
            HtmlNode parentNode2 = hd2.DocumentNode.SelectSingleNode("//div[@class='blockItemNews']");
            art2.Url = parentNode2.SelectSingleNode(".//div[@class='bold fontsize13']").SelectSingleNode(".//a").Attributes["href"].Value.Trim();
            art2.Title = parentNode2.SelectSingleNode(".//div[@class='bold fontsize13']").InnerText.Trim();
            art2.Date = parentNode2.SelectSingleNode(".//div[@class='fontsize11 paddingBt6']").InnerText.Trim();
            art2.Content = parentNode2.SelectSingleNode(".//div[@class='textLeft paddingBt10']").InnerText.Trim();
            art2.Image = parentNode2.SelectSingleNode(".//img[@class='imgListNews']").Attributes["src"].Value.Trim();
            la.Add(art2);

            HtmlAgilityPack.HtmlDocument hd3 = hw.Load("http://www.baodongnai.com.vn/thethao/");
            HtmlNode hn3 = hd3.DocumentNode;
            HtmlNode parentNode3 = hd3.DocumentNode.SelectSingleNode("//li[@class='cate-list-1 first']");
            art3.Url = parentNode3.SelectSingleNode(".//a").Attributes["href"].Value.Trim();
            art3.Title = parentNode3.SelectSingleNode(".//a[@class='title']").InnerText.Trim();
            art3.Date = parentNode3.SelectSingleNode(".//div[@class='date']").InnerText.Trim();
            art3.Content = parentNode3.SelectSingleNode(".//div[@class='lead']").InnerText.Trim();
            art3.Image = parentNode3.SelectSingleNode(".//img").Attributes["src"].Value.Trim();

            art3.Url = "http://www.baodongnai.com.vn/" + art3.Url;
            art3.Image = "http://www.baodongnai.com.vn/" + art3.Image;
            la.Add(art3);

            HtmlAgilityPack.HtmlDocument hd4 = hw.Load("http://www.baoquangninh.com.vn/the-thao/");
            HtmlNode hn4 = hd4.DocumentNode;
            HtmlNode parentNode4 = hd4.DocumentNode.SelectSingleNode("//li[@class='item first']");
            art4.Url = parentNode4.SelectSingleNode(".//a").Attributes["href"].Value.Trim();
            art4.Title = parentNode4.SelectSingleNode(".//a[@class='title']").InnerText.Trim();
            art4.Date = parentNode4.SelectSingleNode(".//div[@class='date']").InnerText.Trim();
            art4.Content = parentNode4.SelectSingleNode(".//div[@class='lead']").InnerText.Trim();
            art4.Image = parentNode4.SelectSingleNode(".//img").Attributes["src"].Value.Trim();

            art4.Url = "http://www.baoquangninh.com.vn/" + art4.Url;
            art4.Image = "http://www.baoquangninh.com.vn/" + art4.Image;
            la.Add(art4);

            BingleResultRepeater.DataSource = la;
            BingleResultRepeater.DataBind();
        }
    }
}