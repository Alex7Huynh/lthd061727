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
using CaroSocialNetwork.Util.Engine;

namespace CaroSocialNetwork
{
    public partial class BingTranslate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Translation";
            if (!IsPostBack)
            {
                Dictionary<string, string> language = TranslateEngine.InitLanguage();
                ddlSource.Items.Clear();
                ddlDest.Items.Clear();
                for (int i = 0; i < language.Keys.Count; ++i)
                {
                    ddlSource.Items.Add(new ListItem(language.Keys.ElementAt(i), language.Values.ElementAt(i)));
                    ddlDest.Items.Add(new ListItem(language.Keys.ElementAt(i), language.Values.ElementAt(i)));
                }
            }
        }

        protected void btnTranslate_Click(object sender, EventArgs e)
        {
            string src = ddlSource.SelectedValue.ToString();
            string des = ddlDest.SelectedValue.ToString();
            string textSource = txtSource.Text;
            string textDest = TranslateEngine.Translate(textSource, src, des);
            txtDest.Text = textDest;
        }
    }
}