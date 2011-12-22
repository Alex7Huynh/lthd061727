<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapViewer.aspx.cs" Inherits="CaroSocialNetwork.MapViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/MyStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/gears_init.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.6.1.min.js"></script>  
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false">
    </script>
    <script type="text/javascript" src="Scripts/gmap3.min.js"></script>
    <script type="text/javascript" src="Scripts/MyScript.js">
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Your Address</h1>
    <p>
        <input id="address" type="text" />
        <input type="button" value="Find" onclick="showAddress();" />
        <input type="button" id="btnAddMarker" value="Add marker" onclick="addMarker();" />
        <input type="button" id="btnDeleteAllMarker" value="Delete all marker" onclick="deleteAllMarker();" />
        <asp:Button id="btnAddCategory" runat="server" Text="Add Category" onclick="btnAddCategory_Click"/>
    </p>
    <div id="panel" style="width: 300px; float: left">
        <div id="listaddress" style="width: 100%; height: 250px;">
        </div>
        <div id="locationtree" style="width: 100%; height: 250px;">
            <asp:TreeView ID="MyLocationTreeView" runat="server" Font-Bold="False" ImageSet="XPFileExplorer" ShowLines="True" Height="171px" Width="123px">
            </asp:TreeView>
        </div>
    </div>
    <div id="map" style="width: 900px; height: 500px; float: left"></div>
</asp:Content>
