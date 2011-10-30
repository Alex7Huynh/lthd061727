<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GoogleMapApp.Default" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 96px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center">
        <tr>
            <td class="style1" colspan="2" align="center">
                <asp:Label ID="Label2" runat="server" Text="ĐĂNG NHẬP" Font-Bold="True" 
                    Font-Size="20px" ForeColor="#0000CC" Width="221px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbUsername" runat="server">alice</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password">alice</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btnDangNhap" runat="server" Text="Đăng nhập" OnClick="btnDangNhap_Click" />
            </td>
            <td>
                <asp:Button ID="btnDangKy" runat="server" Text="Đăng ký" OnClick="btnDangKy_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
