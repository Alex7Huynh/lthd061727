<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SportNews.aspx.cs" Inherits="CaroSocialNetwork.RSS.SportNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>5 TIN TỨC THỂ THAO MỚI NHẤT TỪ 5 TRANG WEB NỔI TIẾNG:</h1>
    <h1>
        <asp:HyperLink ID="HyperLink1" runat="server" Text='http://vnexpress.net/gl/the-thao/' NavigateUrl='http://vnexpress.net/gl/the-thao/'></asp:HyperLink>

        </h1>
    <h1>

        <asp:HyperLink ID="HyperLink2" runat="server" Text='http://baodatviet.vn/Home/thethao.datviet' NavigateUrl='http://baodatviet.vn/Home/thethao.datviet'></asp:HyperLink>
    </h1>
    <h1>

        <asp:HyperLink ID="HyperLink3" runat="server" Text='http://www.tienphong.vn/The-Thao/Index.html' NavigateUrl='http://www.tienphong.vn/The-Thao/Index.html'></asp:HyperLink>
    </h1>
    <h1>

        <asp:HyperLink ID="HyperLink4" runat="server" Text='http://www.baodongnai.com.vn/thethao/' NavigateUrl='http://www.baodongnai.com.vn/thethao/'></asp:HyperLink>
    </h1>
    <h1>

        <asp:HyperLink ID="HyperLink5" runat="server" Text='http://www.baoquangninh.com.vn/the-thao/' NavigateUrl='http://www.baoquangninh.com.vn/the-thao/'></asp:HyperLink>
    </h1>
    
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
