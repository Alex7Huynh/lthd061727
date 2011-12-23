<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayCaro.aspx.cs" Inherits="CaroSocialNetwork.PlayCaro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Play Caro With Your Friend</title>
    <script src="../Scripts/caro.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>  
    <link type="text/css" href="../Styles/Caro.css" rel="stylesheet"/>
    <script type="text/javascript">
        function loadForm() {
            resetGame();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="AjaxScriptManager" runat="server">
        </asp:ScriptManager>
    <div style="width: 80%; float: left; ">
        <canvas onclick="javascript: clickHandler(event);" id="board" height="600" width="600" position="relative"/>
    </div>
    <div style="width: 15%; float: right; ">
        <a href="#" onclick="WaitingForOpponent()">Click Here</a>
        <input type="button" onclick="WaitingForOpponent()" value="reset" name="Reset"/>
        <asp:Button ID="btnPlayWithMachine" runat="server" Text="Play With Machine" 
            onclick="btnPlayWithMachine_Click" />
        <asp:Button ID="btnPlayWithOpponent" runat="server" Text="Play With Opponent" 
            onclick="btnPlayWithOpponent_Click" />
    </div>
&nbsp;
</asp:Content>
