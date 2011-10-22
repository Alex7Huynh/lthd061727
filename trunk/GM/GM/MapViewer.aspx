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
    <script type="text/javascript">
        function gmarkerdblclick()
        {
            var button = document.getElementByID("btnGMKDblclickHandler.ClientID");
            alert("Hello this is an Alert");
            var confirm = confirm('Are you want to add this location?');
            button.click();
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: none;">
       <asp:Button ID="btnGMKDblclickHandler" runat="server" 
            OnClick="btnGMKDblclickHandler_Click" />
    </div>

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
            <p>
                <input type="button" id="btnAddMarker" value="Add marker" onclick="addMarker()" />
                <input type="button" id="btnDeleteAllMarker" value="Delete all marker" onclick="deleteAllMarker()" />
                <asp:Button id="btnAddCategory" runat="server" Text="Add Category" onclick="btnAddCategory_Click"/>
            </p>
            <p>
                <asp:Label ID="lblCurrentLocation" runat="server"></asp:Label>
                <br/>
                <asp:Button id="btnUpdateMovementCurrentLocation" runat="server" Text="Update Movement Current Location" onclick="btnUpdateMovementCurrentLocation_Click"/>
                <asp:Button id="btnCancelMovementCurrentLocation" runat="server" Text="Cancel Movement Current Location" onclick="btnCancelMovementCurrentLocation_Click"/>
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
                        <cc1:GMap ID="GMap1" runat="server" Height="500px" Width="800px"/>
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
