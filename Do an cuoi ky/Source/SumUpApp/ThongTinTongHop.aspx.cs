using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace SumUpApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Lấy ngày hiện tại, 
            // nếu ngày hiện tại chưa có thông tin rút trích trong database 
            // thì thực hiện rút trích và thêm vào database
            // không thì cập nhật thông tin
        }

        /// <summary>
        /// Hàm thực hiện rút trích thông tin từ một url cụ thể, chỉ (hỗ trợ) hoạt động với http://eboard.orstrade.vn:8888/client/index.htm
        /// </summary>
        /// <param name="url">http://eboard.orstrade.vn:8888/client/index.htm</param>
        /// <returns></returns>
        public virtual IList<ChungKhoan> ParseEboard(string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument hd = hw.Load(url);
            HtmlNode hn = hd.DocumentNode;
            HtmlNode parentNode = hd.DocumentNode.SelectSingleNode("//div[@id='matrix-cavan1']");
            HtmlNodeCollection anode = parentNode.SelectNodes(".//table");
            List<ChungKhoan> li = new List<ChungKhoan>();
            foreach (HtmlNode hnode in anode)
            {
                HtmlNode artnode = hnode.SelectSingleNode(".//td[@class='post_title']");
                ChungKhoan item = new ChungKhoan();
                //item.Title = artnode.InnerText.Trim();
                //string date = hnode.SelectSingleNode(".//td[@class='day_month']").InnerText.Trim();
                //string month = hnode.SelectSingleNode("(.//td[@class='day_month'])[2]").InnerText.Trim();
                //string year = hnode.SelectSingleNode(".//td[@class='post_year']").InnerText.Trim();
                //string pubdate = month + '/' + date + '/' + year + " 00:00:00 AM";
                //item.PubDate = DateTime.Parse(pubdate);
                li.Add(item);
            }
            return li;
        }
    }
}
