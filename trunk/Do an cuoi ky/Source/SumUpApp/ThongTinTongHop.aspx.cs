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
            // Link cần rút trích
            string url = "http://eboard.orstrade.vn:8888/client/index.htm";
            // Lấy ngày hiện tại,
            // thực hiện rút trích 
            List<ChungKhoan> dsChungKhoan = ParseEboard(url);
            // nếu ngày hiện tại chưa có thông tin rút trích trong database thì thêm vào database
            // không thì cập nhật thông tin

            //Hiển thị thông tin lên một table, (giống trang web trên cũng được)
        }

        /// <summary>
        /// Hàm thực hiện rút trích thông tin từ một url cụ thể, chỉ (hỗ trợ) hoạt động với http://eboard.orstrade.vn:8888/client/index.htm
        /// </summary>
        /// <param name="url">http://eboard.orstrade.vn:8888/client/index.htm</param>
        /// <returns></returns>
        public List<ChungKhoan> ParseEboard(string url)
        {
            HtmlWeb doc = new HtmlWeb();
            HtmlDocument hd = doc.Load(url);
            HtmlNode hn = hd.DocumentNode;
            HtmlNode parentNode = hd.DocumentNode.SelectSingleNode("//div[@id='matrix-cavan1']");
            HtmlNodeCollection tables = parentNode.SelectNodes("./table");
            List<ChungKhoan> dsChungKhoan = new List<ChungKhoan>();
            foreach (HtmlNode table in tables)
            {
                foreach (HtmlNode row in table.SelectNodes(".//tr"))
                {
                    ChungKhoan chungKhoan = new ChungKhoan();

                    //id
                    HtmlNode idCell = row.SelectSingleNode("./td[@class='matrix-cell-CODE']");
                    chungKhoan.MaChungKhoan = idCell.SelectSingleNode("./a").InnerText;

                    // Các giá trị tiếp theo như giá, khối lượng... như trên, chú ý không có thẻ a

                    dsChungKhoan.Add(chungKhoan);
                }
            }
            return dsChungKhoan;
        }
    }
}
