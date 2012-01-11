#region includes
using System;
using System.Xml;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Globalization;
using System.Linq;
#endregion

namespace SumUpApp.BUS
{
    // Exposing the service contract    
    [ServiceContract]
    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    public interface INewsFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetNews?s1={s1}&s2={s2}")]
        SyndicationFeedFormatter GetNews(string s1,string s2);
    }

    public class FeedService : INewsFeed
    {
        public SyndicationFeedFormatter GetNews(string s1, string s2)
        {
            XmlDocument xmlDoc1 = new XmlDocument();
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc1.Load(s1);
            xmlDoc2.Load(s2);
            XmlNodeList fnames1 = xmlDoc1.GetElementsByTagName("item");
            XmlNodeList fnames2 = xmlDoc2.GetElementsByTagName("item");

            //Setting up the feed formatter.
            SyndicationFeed feed = new SyndicationFeed("Technical News Feed", "Technical News Feed", new Uri("http://WcfInsiders.com"));
            feed.Authors.Add(new SyndicationPerson("adnanmasood@gmail.com"));
            feed.Categories.Add(new SyndicationCategory("Technical News"));
            feed.Description = new TextSyndicationContent("Technical News demo for RSS and ATOM publishing via WCF");
            
            //Adding Items
            List<SyndicationItem> items = new List<SyndicationItem>();
            SyndicationItem item;
            for(int i=0;i<fnames1.Count;i++)
            {
                item = new SyndicationItem(fnames1[i].ChildNodes[0].InnerText, fnames1[i].ChildNodes[1].InnerText, new Uri(fnames1[i].ChildNodes[2].InnerText), System.Guid.NewGuid().ToString(), DateTimeOffset.Parse(fnames1[i].ChildNodes[3].InnerText));
                items.Add(item);
            }
            
            for (int i = 0; i < fnames2.Count; i++)
            {
                item = new SyndicationItem(fnames2[i].ChildNodes[0].InnerText, fnames2[i].ChildNodes[1].InnerText, new Uri(fnames2[i].ChildNodes[2].InnerText), System.Guid.NewGuid().ToString(), DateTimeOffset.Parse(fnames2[i].ChildNodes[3].InnerText));
                items.Add(item);
            }
            
            items = items.OrderByDescending(x => x.PublishDate).ToList();
          
            
            feed.Items = items;

            // Processing and serving the feed according to the required format
            // i.e. either RSS or Atom.
            return new Rss20FeedFormatter(feed);
        }
    }

} //Ends Namespace
