using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleMapApp
{
    public partial class XyLy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btThemDiaDiem_Click(object sender, EventArgs e)
        {
            string myStringVariable = "Thêm địa điểm thất bại. :)";
            string ten = Request.QueryString["ten"].ToString();
            float viDo = float.Parse(Request.QueryString["lat"].ToString());
            float kinhDo = float.Parse(Request.QueryString["lgn"].ToString());
            DiaDiemDTO diaDiem = new DiaDiemDTO();
            DanhMucDTO danhMuc = new DanhMucDTO();
            NguoiDungDTO nguoiDung = (NguoiDungDTO)Session["User"];
            danhMuc.TenDanhMuc = tbTenDanhMuc.Text;
            danhMuc.NguoiDung = new NguoiDungDTO();
            danhMuc.NguoiDung = nguoiDung;
            diaDiem.DanhMuc = danhMuc;
            diaDiem.TenDiaDiem = ten;
            diaDiem.ViDo = viDo;
            diaDiem.KinhDo = kinhDo;
            if (DanhMucDAO.TimDanhMuc(tbTenDanhMuc.Text, nguoiDung) == null)
            {
                if (DanhMucDAO.ThemDanhMuc(danhMuc))
                {
                    //Thông báo thất bại                    
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);                    
                }
            }
            if (DiaDiemDAO.ThemDiaDiem(diaDiem))
            {
                //Thông báo thành công
                Response.Redirect("index.aspx");
            }
            //Thông báo thất bại            
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);                    
        }
    }
}