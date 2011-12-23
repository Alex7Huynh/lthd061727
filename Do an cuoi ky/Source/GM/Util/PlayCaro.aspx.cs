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
        int currentRoomSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                roomIndex = -1;
                currentRoomSelected = -1;
                UpdateRoomView();

                CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
                Room[] rooms = service.GetRoomList();
                ddlRooms.DataTextField = "Name";
                ddlRooms.DataSource = rooms;
                ddlRooms.DataBind();

                if (!ClientScript.IsStartupScriptRegistered("loadForm"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "load", "loadForm();", true);
                }
                Ajax.Utility.GenerateMethodScripts(this);
            }
        }

        private void UpdateRoomView()
        {
            if (roomIndex == -1)
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
            int[] x = service.WaitingForOpponent(roomIndex, Membership.GetUser().UserName);
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
            bool gameOver = service.CheckGameOver(roomIndex, Membership.GetUser().UserName, out win);
            if (gameOver)
            {
                return win;
            }
            return false;
        }

        protected void btnJoinRoom_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.JoinRoom(currentRoomSelected, Membership.GetUser().UserName);
            roomIndex = currentRoomSelected;

            UpdateRoomView();
        }

        protected void ddlRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentRoomSelected = ddlRooms.SelectedIndex;
        }

        protected void btnCreateRoomMachine_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, true, out roomIndex);

            UpdateRoomView();
        }

        protected void btnCreateRoomPlayer_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.CreateRoom(Membership.GetUser().UserName, false, out roomIndex);

            UpdateRoomView();
        }

        protected void btnLeaveTheRoom_Click(object sender, EventArgs e)
        {
            CaroWebService.CaroWebService service = new CaroWebService.CaroWebService();
            service.LeaveRoom(ref roomIndex, Membership.GetUser().UserName);

            UpdateRoomView();
        }
    }
}