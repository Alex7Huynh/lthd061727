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
using Ajax;

namespace CaroSocialNetwork
{
    public partial class PlayCaro : System.Web.UI.Page
    {
        static RoomManager roomManager = new RoomManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentRoom"] == null)
                Session["CurrentRoom"] = -1;

            if (!ClientScript.IsStartupScriptRegistered("loadForm"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "load", "loadForm();", true);
            }

            Ajax.Utility.GenerateMethodScripts(this);

            UpdateForm();
        }

        private void UpdateForm()
        {
            UpdateRoomView();
            UpdateListRooms();
        }

        private void UpdateListRooms()
        {
            Room[] rooms = roomManager.GetRoomList().ToArray();
            ddlRooms.DataTextField = "Name";
            ddlRooms.DataValueField = "Id";
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

        protected void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (ddlRooms.SelectedIndex != -1)
            {
                int roomId = int.Parse(ddlRooms.SelectedItem.Value);
                roomManager.JoinRoom(roomId, Membership.GetUser().UserName);
                Session["CurrentRoom"] = roomId;

                UpdateForm();
            }
            else
                MessageBox.Show("It seem to be server does not have any room");
        }

        protected void btnCreateRoomMachine_Click(object sender, EventArgs e)
        {
            int roomId;
            roomManager.CreateRoom(Membership.GetUser().UserName, true, out roomId);
            Session["CurrentRoom"] = roomId;

            UpdateForm();
        }

        protected void btnCreateRoomPlayer_Click(object sender, EventArgs e)
        {
            int roomId;
            roomManager.CreateRoom(Membership.GetUser().UserName, false, out roomId);
            Session["CurrentRoom"] = roomId;

            UpdateForm();
        }

        protected void btnLeaveTheRoom_Click(object sender, EventArgs e)
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            roomManager.LeaveRoom(roomId, Membership.GetUser().UserName);
            Session["CurrentRoom"] = -1;

            UpdateForm();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateForm();
        }

        #region Ajax

        [Ajax.AjaxMethod(false)]
        public bool IsMyTurn()
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            return roomManager.IsMyTurn(roomId, Membership.GetUser().UserName);
        }

        [Ajax.AjaxMethod(false)]
        public int[] GetOpponentMove()
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            return roomManager.GetLastMove(roomId);
        }

        [Ajax.AjaxMethod(false)]
        public bool IsGameOver()
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            return roomManager.IsGameOver(roomId);
        }

        [Ajax.AjaxMethod(false)]
        public void UserMove(int x, int y)
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            roomManager.Move(roomId, Membership.GetUser().UserName, x, y);
        }

        [Ajax.AjaxMethod(false)]
        public bool IsWin()
        {
            int roomId = int.Parse(Session["CurrentRoom"].ToString());
            return roomManager.IsWin(roomId, Membership.GetUser().UserName);
        }

        #endregion
    }
}