<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FindRoom.aspx.cs" Inherits="CaroSocialNetwork.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 30%; float: left; ">
        <asp:Label ID="lblMyPlaces" runat="server" Text="My Places: "></asp:Label>
        <asp:ListBox ID="lstboxMyPlaces" runat="server" Height="331px" Width="388px" 
            AutoPostBack="True" EnableViewState="True" onselectedindexchanged="lstboxMyPlaces_SelectedIndexChanged">
        </asp:ListBox>
    </div>
    <div style="width: 30%; float: left; ">
        <asp:Label ID="lblPeoplesNearThisPlaces" runat="server" 
            Text="Peoples Near This Places: "></asp:Label>
        <asp:ListBox ID="lstBoxPeoplesNearby" runat="server" Height="330px" 
            Width="390px" onselectedindexchanged="lstBoxPeoplesNearby_SelectedIndexChanged">
        </asp:ListBox>
    </div>
    <div style="width: 30%; float: left; ">
        <asp:Label ID="lblRooms" runat="server" 
            Text="Available Rooms: "></asp:Label>
        <asp:ListBox ID="lstRooms" runat="server" Height="330px" Width="383px">
        </asp:ListBox>
    </div>
</asp:Content>
