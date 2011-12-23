using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using CaroSocialNetwork.CaroWebService;
using System.Web.Security;
using System.Windows.Forms;

namespace CaroSocialNetwork
{
    public partial class PlayCaro : System.Web.UI.Page
    {
        int roomIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("loadForm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "load", "loadForm();", true);
            }
            Ajax.Utility.GenerateMethodScripts(this);
        }

        [Ajax.AjaxMethod("WaitingForOpponent", "opponentMove", null, "Loading...")]
        public int[] WaitingForOpponent()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            int[] x = service.WaitingForOpponent(roomIndex, Membership.GetUser().UserName);
            return x;
        }

        [Ajax.AjaxMethod]
        public void UserMove(int userX, int userY)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
        }

        [Ajax.AjaxMethod]
        public void PlayWithMachine()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, true, out roomIndex);
        }

        [Ajax.AjaxMethod]
        public void PlayWithOpponent()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, false, out roomIndex);
        }

        [Ajax.AjaxMethod("CheckGameOver", "gameOver", null, "Loading...")]
        public bool CheckGameOver()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            bool win;
            bool gameOver = service.CheckGameOver(roomIndex, Membership.GetUser().UserName, out win);
            if (gameOver)
            {
                return win;
            }
            return false;
        }
    }
}