<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThemDiaDiem.aspx.cs" Inherits="GoogleMapApp.XyLy"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2" align="center">
                    <strong>THÊM ĐỊA ĐIỂM</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Tên địa điểm:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbTenDiaDiem" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Nhập danh mục:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbTenDanhMuc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nhập ghi chú:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbGhiChu" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btThemDiaDiem" runat="server" Text="Thêm" OnClick="btThemDiaDiem_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
