<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapViewer.aspx.cs" Inherits="GM.MapViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="Scripts/MyScript.js"/>
    <script type="text/javascript" src="Scripts/gears_init.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <input id="hdnLat" type="hidden" />
    <input id="hdnLng" type="hidden" />
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            You need to login to view this page!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h1>Google API V3</h1>
            <p>
                <input id="address" type="text" />
                <input type="button" value="Find" onclick="showAddress();" />
            </p>
            <p>
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
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
