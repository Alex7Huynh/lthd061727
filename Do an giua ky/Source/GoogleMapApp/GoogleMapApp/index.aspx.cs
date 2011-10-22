using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Xml;

namespace GoogleMapApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                string ten = Request.QueryString["ten"].ToString();
                float viDo = float.Parse(Request.QueryString["lat"].ToString());
                float kinhDo = float.Parse(Request.QueryString["lgn"].ToString());
                
                MyClass.ds.Add(new DiaDiemDTO(ten, viDo, kinhDo));
                foreach (DiaDiemDTO d in MyClass.ds)
                {
                    general.Text += d.tenDiaDiem + "  " + d.viDo + "   " + d.kinhDo + "\n";
                }
            }*/
            if (!IsPostBack)
                LoadTreeView();
        }
        private void LoadTreeView()
        {
            TreeView1.Nodes.Clear();
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/GoogleAPI.xml");
            doc.Load(path);
            XmlNode nguoiDung = doc.DocumentElement.ChildNodes[0];
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
            doc.Save(path);
        }
        protected void setMarker1_Click(object sender, EventArgs e)
        {

        }

        protected void setLocation_Click(object sender, EventArgs e)
        {
            string address = DiaDiem.Text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=true");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            XDocument data = XDocument.Load(sr);
            var status = from c in data.Descendants("status")
                         select c.Value;

            if (status.Contains("OK"))
            {
                var TenDiaDiem = (from c in data.Descendants("result")
                                  select c.Element("formatted_address").Value).ToArray();

                var lat = (from b in data.Descendants("location")
                           select b.Element("lat").Value).ToArray();

                var lng = (from b in data.Descendants("location")
                           select b.Element("lng").Value).ToArray();

                int count = TenDiaDiem.Count();
                if (lat.Count() == count && lng.Count() == count)
                {
                    //for (int i = 0; i < count; i++)
                    //{
                    //    dataGridView1.Rows.Add(TenDiaDiem[i].ToString(), lat[i].ToString(), lng[i].ToString());
                    //}
                    //viDo.Text = lat[0].ToString();
                    //kinhDo.Text = lng[0].ToString();
                }
            }
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            TreeView t = (TreeView)(sender);
            DiaDiem.Text = t.SelectedNode.Value;
        }
    }
}