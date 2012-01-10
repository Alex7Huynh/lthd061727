<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SportNews.aspx.cs" Inherits="CaroSocialNetwork.RSS.SportNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="BingleResultRepeater" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <h2>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Url", "{0}") %>'
                            meta:resourcekey="HyperLink1Resource1">
                        </asp:HyperLink>
                    </h2>
                    <asp:Label ID="lbl1" runat="server" ForeColor="Green" Text='<%# Eval("Url") %>' /><br />
                    <asp:Image ID="imgNew" runat="server" ImageUrl='<%# Eval("Image")%>' /><br />
                    Content:
                    <asp:Label ID="lbl2" runat="server" ForeColor="Black" Text='<%# Eval("Content") %>' /><br />
                    Date:
                    <asp:Label ID="lbl3" runat="server" ForeColor="Red" Text='<%# Eval("Date") %>' />
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
