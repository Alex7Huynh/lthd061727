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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CurrentRoom"] == null)
                    Session["CurrentRoom"] = -1;
                UpdateRoomView();
                UpdateListRooms();
            }

            if (!ClientScript.IsStartupScriptRegistered("loadForm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "load", "loadForm();", true);
            }
            Ajax.Utility.GenerateMethodScripts(this);
        }

        private void UpdateListRooms()
        {

            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            Room[] rooms = service.GetRoomList();
            ddlRooms.DataTextField = "Name";
            ddlRooms.DataSource = rooms;
            ddlRooms.DataBind();
        }

        private void UpdateRoomView()
        {
            if (int.Parse(Session["CurrentRoom"].ToString()) == -1)
            {
                RoomMultiView.SetActiveView(NoRoomView);
            }
            else
            {
                RoomMultiView.SetActiveView(InRoomView);
            }
        }

        [Ajax.AjaxMethod("WaitingForOpponent", "opponentMove", null, "Loading...")]
        public int[] WaitingForOpponent()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            int[] x = service.WaitingForOpponent(int.Parse(Session["CurrentRoom"].ToString()), Membership.GetUser().UserName);
            return x;
        }

        [Ajax.AjaxMethod]
        public void UserMove(int userX, int userY)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
        }

        [Ajax.AjaxMethod("CheckGameOver", "gameOver", null, "Loading...")]
        public bool CheckGameOver()
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            bool win;
            bool gameOver = service.CheckGameOver(int.Parse(Session["CurrentRoom"].ToString()), Membership.GetUser().UserName, out win);
            if (gameOver)
            {
                return win;
            }
            return false;
        }

        protected void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (ddlRooms.SelectedIndex != -1)
            {
                CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
                service.JoinRoom(ddlRooms.SelectedIndex, Membership.GetUser().UserName);
                Session["CurrentRoom"] = ddlRooms.SelectedIndex;

                UpdateRoomView();
                UpdateListRooms();
            }
            else
                MessageBox.Show("It seem to be server does not have any room");
        }

        protected void btnCreateRoomMachine_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            int roomIndex;
            service.CreateRoom(Membership.GetUser().UserName, true, out roomIndex);
            Session["CurrentRoom"] = roomIndex;

            UpdateRoomView();
            UpdateListRooms();
        }

        protected void btnCreateRoomPlayer_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            int roomIndex;
            service.CreateRoom(Membership.GetUser().UserName, false, out roomIndex);
            Session["CurrentRoom"] = roomIndex;

            UpdateRoomView();
            UpdateListRooms();
        }

        protected void btnLeaveTheRoom_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            int roomIndex = int.Parse(Session["CurrentRoom"].ToString());
            service.LeaveRoom(ref roomIndex, Membership.GetUser().UserName);
            Session["CurrentRoom"] = roomIndex;

            UpdateRoomView();
            UpdateListRooms();
        }
    }
}