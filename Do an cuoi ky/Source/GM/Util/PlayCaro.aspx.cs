using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CaroSocialNetwork.CaroWebService;
using System.Web.Security;

namespace CaroSocialNetwork
{
    public partial class PlayCaro : System.Web.UI.Page
    {
        int roomIndex;
        int userId;

        private System.Windows.Forms.WebBrowser wb;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("loadForm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "load", "loadForm();", true);
            }
        }

        [Ajax.AjaxMethod]
        public void WaitingForOpponent()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.WaitingForOpponentCompleted += new WaitingForOpponentCompletedEventHandler(service_WaitingForOpponentCompleted);
            service.WaitingForOpponentAsync(roomIndex, Membership.GetUser().UserName);
        }

        void service_WaitingForOpponentCompleted(object sender, WaitingForOpponentCompletedEventArgs e)
        {
            Object[] objs = { e.Result };
            wb.Document.InvokeScript("opponentMove", objs);
        }

        [Ajax.AjaxMethod]
        public void UserMove(int userX, int userY)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
        }

        protected void btnPlayWithMachine_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, true, out roomIndex);
        }

        protected void btnPlayWithOpponent_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, false, out roomIndex);
        }
    }
}