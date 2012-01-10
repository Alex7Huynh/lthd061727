<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RSS.aspx.cs" Inherits="CaroSocialNetwork.RSS.RSS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <title>RSS Web Service</title>

    <script type="text/javascript">

        // This function calls the Web Service method.
        function GetRSSFeed() {
            var strRSSUrl1 = document.getElementById("RSSUrl1");
            var strRSSUrl2 = document.getElementById("RSSUrl2");
            var strRSSUrl3 = document.getElementById("RSSUrl3");
            RSSReader.RssService.GetRSSFeed(strRSSUrl1.value, strRSSUrl2.value, strRSSUrl3.value, SucceededRSSCallback);           
        }

        // This is the callback function that
        // processes the Web Service return value.
        function SucceededRSSCallback(result) {
            var RssResults = '';
            var RsltElem = document.getElementById("Results");
            var myObject = eval(result);

            var Title;
            var Date;
            var RssDescription;
            var RssLink;

            for (var i = 0, len = myObject.length; i < len; ++i) {
                Title = "<a href='" + myObject[i].Link + "' >" + myObject[i].Title + "</a>";
                Date = myObject[i].PubDate;
                RssDescription = myObject[i].Description;
                RssLink = "<a id='lnk" + i + "' href='" + myObject[i].Link + "' >" + myObject[i].Link + "</a>";

                RssResults += Title + '<br />' + Date + '<br />' + RssDescription + '<br />' + RssLink + '<br /><br />';
            }

            RsltElem.innerHTML = RssResults;
        }

    </script>
    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    <asp:ServiceReference Path="RssService.asmx"></asp:ServiceReference>
                </Services>
            </asp:ScriptManager>
            <br />
            <asp:HyperLink ID="lnkWebService" runat="server" NavigateUrl="~/RSS/RssService.asmx"
                Font-Size="16pt">RSS Web Service</asp:HyperLink></div>
    <div>
            URL1<br />
            <input id="RSSUrl1" size="100" type="text" value="http://vnexpress.net/rss/gl/xa-hoi.rss" /><br />
            URL2<br />
            <input id="RSSUrl2" size="100" type="text" value="http://vnexpress.net/rss/gl/the-gioi.rss" /><br />
            URL3<br />
            <input id="RSSUrl3" size="100" type="text" value="http://vnexpress.net/rss/gl/kinh-doanh.rss" /><br />
            <input id="RssButton" type="button" value="Get RSS" onclick="GetRSSFeed()" onclick="return RssButton_onclick()" onclick="return RssButton_onclick()" />
            <br />
            <br />
            <hr />
        </div>
        <div>
            <span id="Results"></span>
        </div>
</asp:Content>
