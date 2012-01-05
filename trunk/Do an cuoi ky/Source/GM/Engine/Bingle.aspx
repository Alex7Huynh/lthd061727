<%@ Page Title="Map Viewer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Bingle.aspx.cs" Inherits="CaroSocialNetwork.Bingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../Styles/bingle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        
        <table width="95%">
            <tr>
                <td width="95%">
                <img alt="" src="../images/bingle.png" style="width: 175px; height: 70px" />
                    <asp:TextBox ID="txtKeyword" runat="server" Width="50%" CssClass="searchbox"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchbox" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnPrev" runat="server" Text="Previous Page" CssClass="searchbox" 
                        onclick="btnPrev_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Next Page" 
                        CssClass="searchbox" onclick="btnNext_Click" />
                </td>
                <td>
                <%--[Begin] Bing translation widget--%>
    <div id="MicrosoftTranslatorWidget" style="width: 360px; min-height: 57px; border-color: #703211;
        background-color: #F07522;">
        <noscript>
            <a href="http://www.microsofttranslator.com/bv.aspx?a=http%3a%2f%2fvnexpress.com%2f">
                Translate this page</a><br />
            Powered by <a href="http://www.microsofttranslator.com">Microsoft® Translator</a></noscript>
    </div>
    <script type="text/javascript"> /* <![CDATA[ */ setTimeout(function() { var s = document.createElement("script"); s.type = "text/javascript"; s.charset = "UTF-8"; s.src = ((location && location.href && location.href.indexOf('https') == 0) ? "https://ssl.microsofttranslator.com" : "http://www.microsofttranslator.com" ) + "/ajax/v2/widget.aspx?mode=manual&from=en&layout=ts"; var p = document.getElementsByTagName('head')[0] || document.documentElement; p.insertBefore(s, p.firstChild); }, 0); /* ]]> */ </script>
    <%--[End] Bing translation widget--%>
                </td>
            </tr>
        </table>
        <br />
        <asp:Repeater ID="BingleResultRepeater" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <h2>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("Link", "{0}") %>'
                            meta:resourcekey="HyperLink1Resource1">
                        </asp:HyperLink>
                    </h2>
                    <asp:Label ID="lbl1" runat="server" ForeColor="Green" Text='<%# Eval("Link") %>' /><br />
                    <asp:Label ID="lbl2" runat="server" ForeColor="Black" Text='<%# Eval("Description") %>' /><br />
                    Source:
                    <asp:Label ID="lbl3" runat="server" ForeColor="Red" Text='<%# Eval("Source") %>' />
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
