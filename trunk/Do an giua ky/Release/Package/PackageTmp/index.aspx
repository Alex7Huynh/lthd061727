<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="GoogleMapApp.index"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Services Finder</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link type="text/css" href="Styles/MyStyle.css" rel="stylesheet" media="all" />
    <script type="text/javascript" src="Scripts/gears_init.js"></script>
    <script type="text/javascript" src="Scripts/MyGoogleMap.js"></script>
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
</head>
<body onload="initialize()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <h1>
        DỊCH VỤ TÌM KIẾM ĐỊA ĐIỂM</h1>
    <p style="width: 100%">
        <asp:TextBox ID="DiaDiem" runat="server" Width="36%"></asp:TextBox>
        <input id="TimDiaDiem" onclick="btnDiaDiem_Click()" type="button" value="Tìm địa điểm" />
        <input id="MyLocation" onclick="btnMyLocation_Click()" type="button" value="Vị trí hiện tại" />
        <asp:Button ID="btnDangXuat" runat="server" Text="Đăng xuất" OnClick="btnDangXuat_Click"
            Width="83px" />
        <input type="button" value="Chia sẻ địa điểm" onclick="window.open('ChiaSeDiaDiem.aspx')"
            style="width: 122px" />
        <asp:TextBox ID="DanhMucTimKiem" runat="server" Width="84px" Text="Trường"></asp:TextBox>
        <input id="TimViTriGanNhat" onclick="timDiaDiemGanNhat()" type="button" value="Tìm vị trí gần nhất" />
    </p>

    <div id="map" style="position: absolute; top: 100px; left: 315px; width: 700px; height: 510px;">
    </div>
    <div id="diadiempanel" style="position: absolute; top: 100px; left: 10; width: 300px;
        height: 250px;">
    </div>
    <div id="CayDiaDiem" runat="server" style="position: absolute; top: 360px; left: 10;
        width: 300px; height: 250px;">
    </div>
    </form>
</body>
</html>
