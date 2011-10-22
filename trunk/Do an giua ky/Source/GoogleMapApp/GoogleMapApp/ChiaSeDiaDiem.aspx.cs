using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleMapApp
{
    public partial class ChiaSeDiaDiem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTreeView();
        }
        private void LoadTreeView()
        {
            TreeView1.Nodes.Clear();
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/GoogleAPI.xml");
            doc.Load(path);
            for (int k = 0; k < doc.DocumentElement.ChildNodes.Count; ++k)
            {
                XmlNode nguoiDung = doc.DocumentElement.ChildNodes[k];
                TreeNode user = new TreeNode(nguoiDung.Attributes[0].Value);

                for (int i = 0; i < nguoiDung.ChildNodes.Count; ++i)
                {
                    TreeNode danhMuc = new TreeNode(nguoiDung.ChildNodes[i].Attributes[0].Value);
                    for (int j = 0; j < nguoiDung.ChildNodes[i].ChildNodes.Count; ++j)
                    {
                        TreeNode diaDiem = new TreeNode(nguoiDung.ChildNodes[i].ChildNodes[j].Attributes[0].Value);
                        danhMuc.ChildNodes.Add(diaDiem);
                    }
                    user.ChildNodes.Add(danhMuc);
                    //TreeView1.Nodes.Add(danhMuc);
                }
                TreeView1.Nodes.Add(user);
            }
            doc.Save(path);
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }
    }
}