using System;
using System.Web;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web.Script.Services;

namespace RSSReader
{
    [WebService(Namespace = "http://www.ADefwebserver.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class RssService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<RssToolkit.Rss.RssItem> GetRSSFeed(string RSSUrl1, string RSSUrl2, string RSSUrl3)
        {
            RssToolkit.Rss.RssDocument rss = new RssToolkit.Rss.RssDocument();
            
            if (RSSUrl1 != "")
            {
                rss = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl1));
                rss.Channel.Items.Clear();
                RssToolkit.Rss.RssDocument rss1 = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl1));
                foreach (RssToolkit.Rss.RssItem item in rss1.Channel.Items)
                {
                    rss.Channel.Items.Add(item);
                }
            }
            if (RSSUrl2 != "")
            {
                if (RSSUrl1 == "")
                {
                    rss = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl2));
                    rss.Channel.Items.Clear();
                }
                RssToolkit.Rss.RssDocument rss2 = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl2));
                foreach (RssToolkit.Rss.RssItem item in rss2.Channel.Items)
                {
                    rss.Channel.Items.Add(item);
                }
            }
            if (RSSUrl3 != "")
            {
                if (RSSUrl1 == "" && RSSUrl2 == "")
                {
                    rss = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl3));
                    rss.Channel.Items.Clear();
                }
                RssToolkit.Rss.RssDocument rss3 = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl3));
                foreach (RssToolkit.Rss.RssItem item in rss3.Channel.Items)
                {
                    rss.Channel.Items.Add(item);
                }
            }
            rss.Channel.Items.Sort(CompareByPublishDate);

            return rss.Channel.Items;
        }

        private static int CompareByPublishDate(RssToolkit.Rss.RssItem a, RssToolkit.Rss.RssItem b)
        {
            if (a.PubDateParsed > b.PubDateParsed)
                return -1;
            if (a.PubDateParsed < b.PubDateParsed)
                return 1;

            return 0;
        }
    }
}
