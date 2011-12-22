using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace CaroSocialNetwork
{
    public partial class PlayCaro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("loadForm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "load", "loadForm();", true);
            }
        }

        [Ajax.AjaxMethod(null, true, false, null)]
        public Point GetMachineMove(int userX, int userY)
        {
            Point point = new Point();
            point.X = 0;
            point.Y = 0;
            return point;
        }
    }
}