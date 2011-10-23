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
            //Kiểm tra đăng nhập
            if (Session["User"] == null)
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
            {
                //Xử lý từ url
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"].ToString() == "Xoa")
                    {
                        DiaDiemDTO diaDiem = new DiaDiemDTO();
                        DanhMucDTO danhMuc = new DanhMucDTO();
                        danhMuc.NguoiDung = (NguoiDungDTO)Session["User"];
                        diaDiem.DanhMuc = danhMuc;
                        diaDiem.TenDiaDiem = Request.QueryString["ten"].ToString();
                        if (DiaDiemDAO.XoaDiaDiem(diaDiem))
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Xóa thành công" + "');", true);
                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "thất bại" + "');", true);
                    }
                    else if (Request.QueryString["action"].ToString() == "TimKiem")
                    {

                    }
                }
                //Nạp treeview
                LoadTreeView();
            }
        }
        private void LoadTreeView()
        {
            NguoiDungDTO nguoiDung = (NguoiDungDTO)Session["User"];
            TreeView1.Nodes.Clear();
            XmlDocument doc = new XmlDocument();
            string path = Server.MapPath("~/App_Data/GoogleAPI.xml");
            doc.Load(path);
            for (int k = 0; k < doc.DocumentElement.ChildNodes.Count; ++k)
            {
                if (doc.DocumentElement.ChildNodes[k].Attributes[0].Value == nguoiDung.Username)
                {
                    XmlNode nguoiDungNode = doc.DocumentElement.ChildNodes[k];
                    TreeNode nguoiDungTreeNode = new TreeNode(nguoiDungNode.Attributes[0].Value);

                    for (int i = 0; i < nguoiDungNode.ChildNodes.Count; ++i)
                    {
                        TreeNode danhMucTreeNode = new TreeNode(nguoiDungNode.ChildNodes[i].Attributes[0].Value);
                        for (int j = 0; j < nguoiDungNode.ChildNodes[i].ChildNodes.Count; ++j)
                        {
                            TreeNode diaDiemTreeNode = new TreeNode(nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes[0].Value);
                            //diaDiemTreeNode.NavigateUrl = "index.aspx?action=TimKiem&ten=" + diaDiemTreeNode.Value;
                            diaDiemTreeNode.NavigateUrl = "javascript:(findMyLocation('" + diaDiemTreeNode.Value 
                                + "', false, '"
                                + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["ghichu"].Value + "'))";
                            danhMucTreeNode.ChildNodes.Add(diaDiemTreeNode);
                        }
                        nguoiDungTreeNode.ChildNodes.Add(danhMucTreeNode);
                        //TreeView1.Nodes.Add(danhMuc);
                    }
                    TreeView1.Nodes.Add(nguoiDungTreeNode);
                }
            }
            doc.Save(path);
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
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + t.SelectedNode.Value + "');", true);
        }

        protected void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}