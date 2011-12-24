<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BingTranslate.aspx.cs" Inherits="CaroSocialNetwork.BingTranslate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    
        <asp:DropDownList ID="ddlSource" runat="server" Height="20px" Width="274px">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlDest" runat="server" Height="17px" Width="267px">
        </asp:DropDownList>
        <asp:Button ID="btnTranslate" runat="server" Height="28px" Text="Translate" 
            Width="174px" onclick="btnTranslate_Click" />
        <br />
        <asp:TextBox ID="txtSource" runat="server" Height="200px" TextMode="MultiLine" 
            Width="360px"></asp:TextBox>
        <asp:TextBox ID="txtDest" runat="server" Height="200px" TextMode="MultiLine" 
            Width="360px"></asp:TextBox>    
    </div>
</asp:Content>
