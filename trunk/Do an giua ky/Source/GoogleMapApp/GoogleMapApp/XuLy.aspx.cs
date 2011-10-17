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
            /*if (!IsPostBack)
            {
                string ten = Request.QueryString["ten"].ToString();
                float viDo = float.Parse(Request.QueryString["lat"].ToString());
                float kinhDo = float.Parse(Request.QueryString["lgn"].ToString());
                
                MyClass.ds.Add(new DiaDiemDTO(ten, viDo, kinhDo));
                foreach (DiaDiemDTO d in MyClass.ds)
                {
                    general.Text += d.tenDiaDiem + "  " + d.viDo + "   " + d.kinhDo + "\n";
                }
            }*/
        }
    }
}