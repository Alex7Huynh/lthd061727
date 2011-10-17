<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GoogleMapApp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Google Map</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link type="text/css" href="Styles/MyStyle.css" rel="stylesheet" media="all" />
    <script type="text/javascript" src="Scripts/gears_init.js"></script>
    <script type="text/javascript" src="Scripts/MyGoogleMap.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="Scripts/google-maps-3-vs-1-0.js" ></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=vi"></script>
    <style type="text/css">
        #DiaDiem
        {
            width: 461px;
        }
    </style>
</head>
<body onload="initialize()">
    <form id="form1" runat="server">
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
    <div id="diadiempanel"></div>
    <div id="map"></div>
    </form>
</body>
</html>
