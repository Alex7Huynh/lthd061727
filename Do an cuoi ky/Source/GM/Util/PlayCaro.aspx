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
    <canvas onclick="javascript: clickHandler(event);" id="board" height="600" width="600" position="relative"/>
    <input type="button" onclick="javascript: resetGame();" value="reset" name="Reset"/>
    <asp:Button ID="btnPlayWithMachine" runat="server" Text="Play With Machine" 
        onclick="btnPlayWithMachine_Click" />
    <asp:Button ID="btnPlayWithOpponent" runat="server" Text="Play With Opponent" 
        onclick="btnPlayWithOpponent_Click" />
&nbsp;
</asp:Content>
