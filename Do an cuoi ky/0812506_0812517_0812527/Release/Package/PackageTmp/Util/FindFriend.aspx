<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FindFriend.aspx.cs" Inherits="SumUpApp.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" href="../Styles/GMap.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/gears_init.js"></script>
    <script type="text/javascript" src="../Scripts/FindFriendScript.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=vi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="mapPanel">
        <div id="leftPanel">
            <div id="mylocations" runat="server">
            </div>
        </div>
        <div id="rightPanel">
            <div id="map">
            </div>
        </div>
    </div>
</asp:Content>
