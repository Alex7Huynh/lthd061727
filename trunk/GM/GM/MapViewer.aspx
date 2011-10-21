<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapViewer.aspx.cs" Inherits="GM.MapViewer" %>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>
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
                <asp:Button id="btnFind" runat="server" Text="Find Address" onclick="btnFind_Click"/>
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
                        <cc1:GMap ID="GMap1" runat="server" Height="500px" Width="800px" />
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
