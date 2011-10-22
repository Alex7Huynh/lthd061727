<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XuLy.aspx.cs" Inherits="GoogleMapApp.XyLy" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Nhập danh mục:"></asp:Label>        
        <asp:TextBox ID="tbTenDanhMuc" runat="server"></asp:TextBox>
        <asp:Button ID="btThemDiaDiem" runat="server" Text="Thêm" 
            onclick="btThemDiaDiem_Click" />
    
    </div>
    </form>
</body>
</html>
