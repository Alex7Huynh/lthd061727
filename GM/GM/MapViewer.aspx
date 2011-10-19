<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapViewer.aspx.cs" Inherits="GM.MapViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link type="text/css" href="Styles/MyStyle.css" rel="stylesheet" media="all" />
    <style type="text/css">
        #DiaDiem
        {
            width: 461px;
        }
    </style>
    <script src="Scripts/maps.js" type="text/javascript"></script><script src="Scripts/main.js" type="text/javascript"></script><style type="text/css">@media print{.gmnoprint{display:none}}@media screen{.gmnoscreen{display:none}}</style>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA" type="text/javascript"></script>
    <script type="text/javascript">

        function initialize() {
            if (GBrowserIsCompatible()) {
                var map = new GMap2(document.getElementById("map"));
                map.setCenter(new GLatLng(37.4419, -122.1419), 13);
                map.setUIToDefault();
            }
        }

    </script>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            You need to login to view this page!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h1>
                ỨNG DỤNG Google API V3</h1>
            <p>
                <asp:TextBox ID="DiaDiem" runat="server" value="Nguyễn Văn Cừ"></asp:TextBox>
                <input id="TimDiaDiem" class="button" onclick="btnDiaDiem_Click()" type="button" value="Tìm địa điểm" />
                <asp:Button ID="setMarker1" runat="server" Text="Set marker" onclick="setMarker1_Click" />
                <asp:Button ID="setLocation" runat="server" Text="Set Location" onclick="setLocation_Click" />
                <input id="MyLocation" class="button" onclick="btnMyLocation_Click()" type="button" value="Tìm vi tri cua minh" />
            </p>
            <p>
                Vĩ độ: <asp:TextBox ID="viDo" runat="server" value="10.75918"></asp:TextBox>        
                Kinh độ: <asp:TextBox ID="kinhDo" runat="server" value="106.662498"></asp:TextBox>        
                <input id="TimDiaDiemMoi" class="button" onclick="btnDiaDiemMoi_Click()" type="button" value="Tìm địa điểm theo tọa độ" />
                <input id="setMarker2" class="button" onclick="setMarker()" type="button" value="Set marker" />
            </p>
            <div id="searchresult"></div>
            <div id="myplacenavigation">
                <asp:TreeView ID="MyLocationTreeView" runat="server" Font-Bold="False" 
        ImageSet="XPFileExplorer" ShowLines="True" ToolTip="Choose Your Location!">
                    <Nodes>
                        <asp:TreeNode Text="New Node" Value="New Node">
                            <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                            <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
            </div>
            <div id="map"></div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
