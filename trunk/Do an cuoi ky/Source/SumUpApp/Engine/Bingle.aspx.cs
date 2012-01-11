using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Configuration;
using SumUpApp.Util.Engine;

namespace SumUpApp
{
    public partial class Bingle : System.Web.UI.Page
    {
        int nResults = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Bingle";
            if (!IsPostBack)
            {
                btnNext.Visible = false;
                btnPrev.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text;
            List<Article> articles = SearchEngine.Search(keyword, 1, nResults);
            BingleResultRepeater.DataSource = articles;
            BingleResultRepeater.DataBind();
            Page.Session["page"] = 1;
            Page.Session["keyword"] = keyword;
            btnNext.Visible = true;
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string keyword = Page.Session["keyword"].ToString();
            int page = int.Parse(Page.Session["page"].ToString());
            if (page == 2)
                btnPrev.Visible = false;

            List<Article> articles = SearchEngine.Search(keyword, --page, nResults);
            BingleResultRepeater.DataSource = articles;
            BingleResultRepeater.DataBind();
            Page.Session["page"] = page;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string keyword = Page.Session["keyword"].ToString();
            int page = int.Parse(Page.Session["page"].ToString());
            if (page == 1)
                btnPrev.Visible = true;
            //Google limits 100 results for a keyword
            if (page == (100/nResults - 1))
                btnNext.Visible = false;

            List<Article> articles = SearchEngine.Search(keyword, ++page, nResults);
            BingleResultRepeater.DataSource = articles;
            BingleResultRepeater.DataBind();
            Page.Session["page"] = page;
        }
    }
}