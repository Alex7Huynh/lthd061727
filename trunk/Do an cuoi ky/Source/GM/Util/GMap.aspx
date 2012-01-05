<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GMap.aspx.cs" Inherits="CaroSocialNetwork.GMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" href="../Styles/MyStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/gears_init.js"></script>
    <script type="text/javascript" src="../Scripts/MyGoogleMap.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=vi"></script>
    <style type="text/css">
        #DiaDiem
        {
        }
        #CayDiaDiem
        {
            width: 181px;
            height: 285px;
        }
        #TimViTriGanNhat
        {
            width: 124px;
        }
        #TimDiaDiem
        {
            width: 93px;
        }
        #MyLocation
        {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <h1>
            DỊCH VỤ TÌM KIẾM ĐỊA ĐIỂM</h1>
        <p style="width: 100%">
            <asp:TextBox ID="DiaDiem" runat="server" Width="36%"></asp:TextBox>            
            <input id="TimDiaDiem" onclick="btnDiaDiem_Click()" type="button" value="Tìm địa điểm" />
            <input id="MyLocation" onclick="btnMyLocation_Click()" type="button" value="Vị trí hiện tại" />
            <input type="button" value="Chia sẻ địa điểm" onclick="window.open('ChiaSeDiaDiem.aspx')"style="width: 122px" />
            <asp:TextBox ID="DanhMucTimKiem" runat="server" Width="84px" Text="Trường"></asp:TextBox>
            <input id="TimViTriGanNhat" onclick="timDiaDiemGanNhat()" type="button" value="Tìm vị trí gần nhất" />
        </p>
        <div id="map">
        </div>
        <div id="diadiempanel" style="position: absolute; top: 200px; left: 10px; width: 300px; height: 260px;">
        </div>
        <div id="CayDiaDiem" runat="server" style="position: absolute; top: 460px; left: 10px; 
            width: 300px; height: 269px; overflow: scroll; float: left;border: 1px solid #000; background-color:White;">
        </div>
    </div>
</asp:Content>