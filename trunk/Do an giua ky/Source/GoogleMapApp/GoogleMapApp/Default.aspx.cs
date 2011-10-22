using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleMapApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            NguoiDungDTO nguoiDung = NguoiDungDAO.DangNhap(username, password);
            if (nguoiDung != null)
            {
                Session.Add("User", nguoiDung);
                Response.Redirect("index.aspx");                
            }
            string myStringVariable = "Đăng nhập thất bại! Vui lòng thử lại. :)";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            NguoiDungDTO nguoiDung = NguoiDungDAO.DangKy(username, password);
            if (nguoiDung != null)
            {
                Session.Add("User", nguoiDung);
                Response.Redirect("index.aspx");                
            }
            string myStringVariable = "Đăng ký thất bại! Vui lòng thử lại. :)";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
        }
    }
}