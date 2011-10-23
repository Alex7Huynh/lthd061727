<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapNhatDiaDiem.aspx.cs"
    Inherits="GoogleMapApp.CapNhatDiaDiem" %>

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
                    <strong>CẬP NHẬT ĐỊA ĐIỂM</strong>
                </td>
            </tr>
            <tr>
                <td>
                    Tên địa điểm:
                </td>
                <td>
                    <asp:TextBox ID="tbTenDiaDiem" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Vĩ độ:
                </td>
                <td>
                    <asp:TextBox ID="tbViDo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Kinh độ:
                </td>
                <td>
                    <asp:TextBox ID="tbKinhDo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Ghi chú:
                </td>
                <td>
                    <asp:TextBox ID="tbGhiChu" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="tbCapNhat" runat="server" Text="Cập nhật" OnClick="tbCapNhat_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
