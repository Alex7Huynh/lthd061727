using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleMapApp
{
    public partial class CapNhatDiaDiem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ten = Request.QueryString["ten"].ToString();
                DiaDiemDTO diaDiem = new DiaDiemDTO();
                DanhMucDTO danhMuc = new DanhMucDTO();
                danhMuc.NguoiDung = (NguoiDungDTO)Session["User"];
                diaDiem.TenDiaDiem = ten;                
                diaDiem.DanhMuc = danhMuc;

                diaDiem = DiaDiemDAO.TimDiaDiem(diaDiem);
                if (diaDiem != null)
                {
                    tbTenDiaDiem.Text = diaDiem.TenDiaDiem;
                    tbViDo.Text = diaDiem.ViDo.ToString();
                    tbKinhDo.Text = diaDiem.KinhDo.ToString();
                    tbGhiChu.Text = diaDiem.GhiChu;
                }
            }
        }

        protected void tbCapNhat_Click(object sender, EventArgs e)
        {
            DiaDiemDTO diaDiem = new DiaDiemDTO();
            DanhMucDTO danhMuc = new DanhMucDTO();
            danhMuc.NguoiDung = (NguoiDungDTO)Session["User"];
            diaDiem.TenDiaDiem = tbTenDiaDiem.Text;
            diaDiem.ViDo = float.Parse(tbViDo.Text);
            diaDiem.KinhDo = float.Parse(tbKinhDo.Text);
            diaDiem.GhiChu = tbGhiChu.Text;
            diaDiem.DanhMuc = danhMuc;

            if (DiaDiemDAO.CapNhatDiaDiem(diaDiem))
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                string myStringVariable = "Cập nhật địa điểm thất bại. :)";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            }
        }
    }
}