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
        public List<RssToolkit.Rss.RssItem> GetRSSFeed(string RSSUrl1, string RSSUrl2)
        {
            RssToolkit.Rss.RssDocument rss1 = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl1));
            RssToolkit.Rss.RssDocument rss2 = RssToolkit.Rss.RssDocument.Load(new System.Uri(RSSUrl2));
            foreach (RssToolkit.Rss.RssItem item in rss2.Channel.Items)
            {
                rss1.Channel.Items.Add(item);
            }
            return rss1.Channel.Items;
        }
    }
}
