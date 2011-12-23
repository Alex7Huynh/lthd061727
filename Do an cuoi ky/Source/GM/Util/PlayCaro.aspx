<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayCaro.aspx.cs" Inherits="CaroSocialNetwork.PlayCaro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Play Caro With Your Friend</title>
    <script src="../Scripts/caro.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>  
    <link type="text/css" href="../Styles/Caro.css" rel="stylesheet"/>
    <script type="text/javascript">
        function loadForm() {
            //Just Draw Caro Board
            initGame();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
    <div style="width: 60%; float: left; ">
        <canvas onclick="javascript: clickHandler(event);" id="board" height="600" width="600" position="relative"/>
    </div>
    <div style="width: 35%; float: right; ">
    
        <asp:MultiView id="RoomMultiView" runat="server" ActiveViewIndex=0>
            <asp:View id="NoRoomView" runat="server">
                Join exist room or create new room...
                <br /> &nbsp;
                <asp:DropDownList ID="ddlRooms" runat="server" 
                    onselectedindexchanged="ddlRooms_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="btnJoinRoom" runat="server" Text="Join Room" 
                    onclick="btnJoinRoom_Click" />
                <br /> &nbsp;
                <asp:Button ID="btnCreateRoomMachine" runat="server" 
                    Text="Create Room With Machine" onclick="btnCreateRoomMachine_Click" />
                <br /> &nbsp;
                <asp:Button ID="btnCreateRoomPlayer" runat="server" 
                    Text="Create Room, Wait For Orther Player" 
                    onclick="btnCreateRoomPlayer_Click" />
            </asp:View>
            <asp:View id="InRoomView" runat="server">
                You are in room...
                <asp:Button ID="btnLeaveTheRoom" runat="server" Text="Leave The Room" />
            </asp:View>
        </asp:MultiView>
    </div>
&nbsp;
</asp:Content>
