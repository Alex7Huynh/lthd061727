<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapViewer.aspx.cs" Inherits="GM.MapViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 825px;
        }
    </style>
    <script src="Scripts/maps.js" type="text/javascript"></script><script src="Scripts/main.js" type="text/javascript"></script>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA" type="text/javascript"></script>
    <script type="text/javascript">
        var map;
        var geocoder;
        function initialize() {
            if (GBrowserIsCompatible()) {
                map = new GMap2(document.getElementById("map"));
                map.setCenter(new GLatLng(51.5, -0.1), 10);
                map.setUIToDefault();

                geocoder = new GClientGeocoder();
            }
        }
        function showAddress() {
            var ispostback = "<%=Page.IsPostBack %>";
            if (ispostback) {
                var txtAddress = document.getElementById("<%=((TextBox)LoginView1.FindControl("txtAddress")).ClientID%>");
                var address = txtAddress.value;

                geocoder.getLatLng(
                address,
                function (point) {
                    if (!point) {
                        alert(address + " not found");
                    }
                    else {
                        map.setCenter(point, 15);
                        var marker = new GMarker(point, { draggable: true });
                        map.addOverlay(marker);
                        marker.openInfoWindow(address);
                        GEvent.addListener(marker, "dblclick", function call() { gmarkerdblclick() });
                    }
                }
            );
            }
        }

        function addMarker() {
            var center = map.getCenter();
            var marker = new GMarker(center, { draggable: true });
            map.addOverlay(marker);
            marker.openInfoWindow("Drag the marker to a specific position");

            GEvent.addListener(marker, "dblclick", function call() { gmarkerdblclick() });
        }

        function gmarkerdblclick() {
            alert("Hello this is an Alert");
            var confirm = confirm('Are you want to add this location?');
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            You need to login to view this page!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h1>
                ỨNG DỤNG Google API V3</h1>
            <p>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <input type="button" value="Find" onclick="showAddress();" />
            </p>
            <p>
                <input type="button" id="btnAddMarker" value="Add marker" onclick="addMarker();" />
                <input type="button" id="btnDeleteAllMarker" value="Delete all marker" onclick="deleteAllMarker()" />
                <asp:Button id="btnAddCategory" runat="server" Text="Add Category" onclick="btnAddCategory_Click"/>
            </p>
            <p>
                <asp:Label ID="lblCurrentLocation" runat="server"></asp:Label>
                <br/>
                <asp:Button id="btnUpdateMovementCurrentLocation" runat="server" Text="Update Movement Current Location" onclick="btnUpdateMovementCurrentLocation_Click"/>
                <asp:Button id="btnCancelMovementCurrentLocation" runat="server" Text="Cancel Movement Current Location" onclick="btnCancelMovementCurrentLocation_Click"/>
            </p>
            <table class="style1">
                <tr>
                    <td>
                        <asp:TreeView ID="MyLocationTreeView" runat="server" Font-Bold="False" 
                            ImageSet="XPFileExplorer" ShowLines="True" ToolTip="Choose Your Location!">
                            <Nodes>
                                <asp:TreeNode Text="New Node" Value="New Node">
                                    <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                    <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                                </asp:TreeNode>
                            </Nodes>
                        </asp:TreeView>
                    </td>
                    <td class="style2">
                        <div id="map" style="width: 800px; height: 800px"></div>
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
