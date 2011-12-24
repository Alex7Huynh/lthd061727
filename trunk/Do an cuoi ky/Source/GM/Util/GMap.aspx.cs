using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace CaroSocialNetwork
{
    public partial class GMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Map";
        }

        public static void LoadTreeView(ref TreeView treeView)
        { 
        
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        { 
        
        }

        protected void btnDangXuat_Click(object sender, EventArgs e)
        { 
        
        }

        [System.Web.Services.WebMethod]
        public static int ThemDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu, int maDanhMuc, string tenDanhMuc)
        {
            return 1;
        }

        [System.Web.Services.WebMethod]
        public static bool CapNhatDiaDiem(int maDiaDiem, string tenDiaDiem, float viDo, float kinhDo, string ghiChu)
        {
            return true;
        }

        [System.Web.Services.WebMethod]
        public static bool XoaDiaDiem(int maDiaDiem)
        {
            return true;
        }

        public void LoadMyTreeView()
        { 
        
        }

        [System.Web.Services.WebMethod]
        public static string TimDiaDiemGanNhat(float viDoHienTai, float kinhDoHienTai, string danhMucTimKiem)
        {
            return String.Empty;
        }

        //public static double FindDistance(DiaDiemDTO p1, DiaDiemDTO p2)
        //{
        //    double R = 6371; // earth's mean radius in km
        //    double dLat = rad(p2.ViDo - p1.ViDo);
        //    double dLong = rad(p2.KinhDo - p1.KinhDo);

        //    double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
        //            Math.Cos(rad(p1.ViDo)) * Math.Cos(rad(p2.ViDo)) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
        //    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        //    double d = R * c;

        //    return d;
        //}
        
        public static double rad(double x)
        {
            return x * Math.PI / 180;
        }
    }
}