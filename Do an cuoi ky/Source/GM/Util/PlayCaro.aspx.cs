using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Web.Security;
using System.Windows.Forms;

namespace CaroSocialNetwork
{
    public partial class PlayCaro : System.Web.UI.Page
    {
        static RoomManager roomManager;

        public PlayCaro()
        {
            roomManager = new RoomManager();
            roomManager.PlayerJoin += new RoomManager.PlayerEvent(roomManager_PlayerJoin);
            roomManager.PlayerLeave += new RoomManager.PlayerEvent(roomManager_PlayerLeave);
            Ajax.Utility.GenerateMethodScripts(this);
        }

        void roomManager_PlayerLeave()
        {
            UpdateForm();
        }

        void roomManager_PlayerJoin()
        {
            UpdateForm();
        }

        private void UpdateForm()
        {

            UpdateRoomView();
            UpdateListRooms();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CurrentRoom"] == null)
                    Session["CurrentRoom"] = -1;

                if (!ClientScript.IsStartupScriptRegistered("loadForm"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "load", "loadForm();", true);
                }
            }
            else
            {
                if (!ClientScript.IsStartupScriptRegistered("reloadForm"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "load", "reloadForm();", true);
                }
            }
        }

        private void UpdateListRooms()
        {
            Room[] rooms = roomManager.GetRoomList().ToArray();
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
            return roomManager.WaitingForOpponent(int.Parse(Session["CurrentRoom"].ToString()), Membership.GetUser().UserName);
        }

        [Ajax.AjaxMethod]
        public void UserMove(int userX, int userY)
        {
            roomManager.Move(int.Parse(Session["CurrentRoom"].ToString()), Membership.GetUser().UserName, userX, userY);
        }

        [Ajax.AjaxMethod("CheckGameOver", "gameOver", null, "Loading...")]
        public bool CheckGameOver()
        {
            bool win;
            bool gameOver = roomManager.CheckGameOver(int.Parse(Session["CurrentRoom"].ToString()), Membership.GetUser().UserName, out win);

            if (gameOver)
            {
                if (win)
                    MessageBox.Show("You won!");
                else
                    MessageBox.Show("You lose!");
            }
            return gameOver;
        }

        protected void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (ddlRooms.SelectedIndex != -1)
            {
                roomManager.JoinRoom(ddlRooms.SelectedIndex, Membership.GetUser().UserName);
                Session["CurrentRoom"] = ddlRooms.SelectedIndex;
            }
            else
                MessageBox.Show("It seem to be server does not have any room");
        }

        protected void btnCreateRoomMachine_Click(object sender, EventArgs e)
        {
            int roomIndex;
            roomManager.CreateRoom(Membership.GetUser().UserName, true, out roomIndex);
            Session["CurrentRoom"] = roomIndex;
        }

        protected void btnCreateRoomPlayer_Click(object sender, EventArgs e)
        {
            int roomIndex;
            roomManager.CreateRoom(Membership.GetUser().UserName, false, out roomIndex);
            Session["CurrentRoom"] = roomIndex;
        }

        protected void btnLeaveTheRoom_Click(object sender, EventArgs e)
        {
            int roomIndex = int.Parse(Session["CurrentRoom"].ToString());
            roomManager.LeaveRoom(ref roomIndex, Membership.GetUser().UserName);
            Session["CurrentRoom"] = roomIndex;
        }
    }
}