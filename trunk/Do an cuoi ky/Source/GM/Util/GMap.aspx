<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GMap.aspx.cs" Inherits="CaroSocialNetwork.GMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" href="../Styles/GMap.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/gears_init.js"></script>
    <script type="text/javascript" src="../Scripts/MyGoogleMap.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=vi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <h1>
            DỊCH VỤ TÌM KIẾM ĐỊA ĐIỂM</h1>
        <div style="width: 100%; ">
            <asp:TextBox ID="txtAddress" runat="server" Width="36%"></asp:TextBox>
            <input id="btnFindLocation" onclick="btnFindLocation_Click()" type="button" value="Tìm địa điểm" />
            <input id="MyLocation" onclick="btnMyLocation_Click()" type="button" value="Vị trí hiện tại" />
            <input type="button" value="Chia sẻ địa điểm" onclick="window.open('ChiaSeDiaDiem.aspx')"style="width: 122px" />
            <asp:TextBox ID="DanhMucTimKiem" runat="server" Width="84px" Text="Trường"></asp:TextBox>
            <input id="TimViTriGanNhat" onclick="timDiaDiemGanNhat()" type="button" value="Tìm vị trí gần nhất" />
        </div>
        <div style="width: 100%; ">
            <asp:TextBox ID="txtCategoryName" runat="server" Width="36%" Text="Category Name"></asp:TextBox>
            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" 
                onclick="btnAddCategory_Click" />
        </div>
        <div id="mapPanel">
            <div id="leftPanel">
                <div id="diadiempanel">
                </div>
                <div id="CayDiaDiem" runat="server">
                </div>
            </div>
            <div id="rightPanel">
                <div id="map">
                </div>
            </div>
        </div>
    </div>
</asp:Content>