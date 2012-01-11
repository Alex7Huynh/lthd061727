<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CaroSocialNetwork._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%">
        <tr>
            <td align="center" valign="top">
                <%-- Currency Converter --%>
                <script type="text/javascript" src="//www.gmodules.com/ig/ifr?url=http://www.pixelmedia.nl/gmodules/ucc.xml&amp;up_fromcur=USD&amp;up_tocur=VND&amp;synd=open&amp;w=320&amp;h=110&amp;title=Currency+Converter&amp;lang=en&amp;country=IN&amp;border=%23ffffff%7C1px%2C1px+solid+%2366ccff%7C1px%2C2px+solid+%2366ccff&amp;output=js"></script>
                <%-- NYTimes.com - Top Stories --%>
                <%--<script type="text/javascript" src="//www.gmodules.com/ig/ifr?url=http://widgets.nytimes.com/packages/html/igoogle/topstories.xml&amp;up_headlineCount=5&amp;up_summaryCount=1&amp;synd=open&amp;w=320&amp;h=200&amp;title=NYTimes.com+-+Top+Stories&amp;border=%23ffffff%7C3px%2C1px+solid+%23999999&amp;output=js"></script>--%>
            </td>
            <td align="center" valign="top">
                <%-- Stock market--%>
                <div align="center" style="font-size: 12px; width: 210px;">
                    <iframe width="210" height="390" style="margin-bottom: 5px" src="http://widgets.wallstreetsurvivor.com/WorldWatch.aspx?market=all"
                        frameborder="0" marginwidth="0" marginheight="0" scrolling="no"></iframe>
                    <br />
                    <a href="http://www.wallstreetsurvivor.com/" target="_top">Play a stock market game</a>
                    <br />
                    <a href="http://www.wallstreetsurvivor.com/Public/Research/Quotes.aspx?symbol=GOOG"
                        target="_top">Get a stock quote</a>
                </div>
            </td>
            <td align="center" valign="top">
                <%-- Youtube search --%>
                <script type="text/javascript" src="//www.gmodules.com/ig/ifr?url=http://www.gstatic.com/ig/modules/youtube/v3/youtube.xml&amp;up_channel=&amp;up_channel_url_to_preload=http%3A%2F%2Fgdata.youtube.com%2Ffeeds%2Fapi%2Fstandardfeeds%2FUS%2Frecently_featured%3Falt%3Djson&amp;up_current_channel_id=0&amp;up_history=&amp;up_historyEnabled=true&amp;up_prefs_version=0&amp;up_rawQuery=&amp;up_searchTerm=&amp;up_search_channel_name0=&amp;up_search_channel_name1=&amp;up_search_channel_name2=&amp;up_search_channel_url0=&amp;up_search_channel_url1=&amp;up_search_channel_url2=&amp;up_search_channels=0&amp;up_showPromo=true&amp;up_title=YouTube&amp;up_userHasSeenSharedActivities=false&amp;up_username=&amp;synd=open&amp;w=300&amp;h=320&amp;title=Youtube+Video+Search&amp;lang=en&amp;country=UK&amp;border=%23ffffff%7C0px%2C1px+solid+%23993333%7C0px%2C1px+solid+%23bb5555%7C0px%2C1px+solid+%23DD7777%7C0px%2C2px+solid+%23EE8888&amp;output=js"></script>
            </td>
        </tr>
    </table>
</asp:Content>
