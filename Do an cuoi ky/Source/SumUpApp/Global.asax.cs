using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;
using HtmlAgilityPack;

namespace SumUpApp
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            //Syndicate every day
            /*ScheduleTimer TickTimer = new ScheduleTimer();
            TickTimer.AddEvent(new SumUpApp.ScheduledTime("Daily", "5:00  PM"));
            TickTimer.Elapsed += new ScheduledEventHandler(TickTimer_Elapsed);
            TickTimer.Start();*/
            doSyndicate();
        }

        void TickTimer_Elapsed(object sender, ScheduledEventArgs e)
        {
            doSyndicate();
        }

        private void doSyndicate()
        {
            // Link cần rút trích
            string url = "http://eboard.orstrade.vn:8888/client/index.htm";
            // thực hiện rút trích 
            ParseEboard(url);

        }

        /// <summary>
        /// Hàm thực hiện rút trích thông tin từ một url cụ thể, chỉ (hỗ trợ) hoạt động với http://eboard.orstrade.vn:8888/client/index.htm
        /// </summary>
        /// <param name="url">http://eboard.orstrade.vn:8888/client/index.htm</param>
        /// <returns></returns>
        public void ParseEboard(string url)
        {
            try
            {
                HtmlWeb doc = new HtmlWeb();
                HtmlDocument hd = doc.Load(url);
                HtmlNode hn = hd.DocumentNode;
                HtmlNode table = hd.DocumentNode.SelectSingleNode("//table[@class='matrix-table']");
                foreach (HtmlNode row in table.SelectNodes("//tr"))
                {
                    ChungKhoan chungKhoan = new ChungKhoan();

                    //id
                    HtmlNode idCell = row.SelectSingleNode("./td[@class='matrix-cell-CODE']");
                    chungKhoan.MaChungKhoan = idCell.SelectSingleNode("./a").InnerText;

                    // Các giá trị tiếp theo như giá, khối lượng... như trên, chú ý không có thẻ a

                    //Giá
                    chungKhoan.GiaTran = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-CEIL']").InnerText);
                    chungKhoan.GiaSan = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-FLOR']").InnerText);
                    chungKhoan.GiaThamChieu = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BASIC']").InnerText);

                    //Mua
                    chungKhoan.TKLMua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BTOTAL']").InnerText);
                    chungKhoan.KL3Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BVOL_3']").InnerText);
                    chungKhoan.KL3Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BPRC_3']").InnerText);
                    chungKhoan.KL2Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BVOL_2']").InnerText);
                    chungKhoan.Gia2Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BPRC_2']").InnerText);
                    chungKhoan.KL1Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BVOL_1']").InnerText);
                    chungKhoan.Gia1Mua = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-BPRC_1']").InnerText);

                    //Khớp Lệnh
                    chungKhoan.GiaKhopLenhGanNhat = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-PRC']").InnerText);
                    chungKhoan.KLKhopLenhGanNhat = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-VOL']").InnerText);
                    chungKhoan.TKLKhopLenhHienTai = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-TNVOL']").InnerText);
                    chungKhoan.ChenhLechGiaKhopLenh = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-CHANGE']").InnerText);

                    //Bán
                    chungKhoan.TKLBan = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-STOTAL']").InnerText);
                    chungKhoan.Gia3Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SVOL_3']").InnerText);
                    chungKhoan.KL3Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SPRC_3']").InnerText);
                    chungKhoan.KL2Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SVOL_2']").InnerText);
                    chungKhoan.Gia2Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SPRC_2']").InnerText);
                    chungKhoan.KL2Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SVOL_1']").InnerText);
                    chungKhoan.Gia1Ban = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-SPRC_1']").InnerText);

                    //Thay Đổi
                    chungKhoan.GiaKhopTrungBinh = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-AVGP']").InnerText);
                    chungKhoan.GiaKhopCaoNhat = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-HIGHT']").InnerText);
                    chungKhoan.GiaKhopThapNhat = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-LOW']").InnerText);

                    //Phiên 1
                    chungKhoan.KhoiLuongPhien1 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-VOL_1']").InnerText);
                    chungKhoan.GiaPhien1 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-PRC_1']").InnerText);

                    //Phiên 2
                    chungKhoan.GiaKhopTrungBinhPhien2 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-AVGP_2']").InnerText);
                    chungKhoan.GiaKhopCaoNhatPhien2 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-HIGHT_2']").InnerText);
                    chungKhoan.GiakhopThapNhatPhien2 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-LOW_2']").InnerText);
                    chungKhoan.TKLKhopPhien2 = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-VOL_2']").InnerText);

                    //Nước Ngoài
                    chungKhoan.KLMuaNuocNgoai = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-FBVOL']").InnerText);
                    chungKhoan.KLBanNuocNgoai = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-FSVOL']").InnerText);
                    chungKhoan.KLConLaiNuocNgoai = double.Parse(row.SelectSingleNode(".td[@class='matrix-cell-FCROOM']").InnerText);

                    //Ngày
                    chungKhoan.NgayGiaoDich = DateTime.Now;

                    // thêm vào database
                    DataClasses1DataContext dc = new DataClasses1DataContext();
                    dc.ChungKhoans.InsertOnSubmit(chungKhoan);
                }
            }
            catch (Exception e)
            {
                // Check network
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
