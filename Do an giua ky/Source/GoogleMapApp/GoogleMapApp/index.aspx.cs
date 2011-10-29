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
    public partial class index : System.Web.UI.Page
    {
        public static NguoiDungDTO CurrentUser;
        public static string FilePath;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kiểm tra đăng nhập
            if (Session["User"] == null)
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
            {
                CurrentUser = (NguoiDungDTO)Session["User"];
                FilePath = Server.MapPath("~/App_Data/GoogleAPI.xml");

                //Nạp treeview
                LoadMyTreeView();
            }
        }


        /*public static void LoadTreeView(ref TreeView treeView)
        {
            NguoiDungDTO nguoiDung = index.CurrentUser;
            treeView.Nodes.Clear();
            XmlDocument doc = new XmlDocument();

            doc.Load(index.FilePath);
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
                    treeView.Nodes.Add(nguoiDungTreeNode);
                }
            }
            doc.Save(index.FilePath);
        }*/

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

        [System.Web.Services.WebMethod]
        public static bool ThemDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu, string tenDanhMuc)
        {
            try
            {
                DanhMucDTO danhMuc = new DanhMucDTO(tenDanhMuc, CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(tenDiaDiem, viDo, kinhDo, ghiChu, danhMuc);
                if (DanhMucDAO.TimDanhMuc(danhMuc.TenDanhMuc, CurrentUser) == null)
                {
                    if (DanhMucDAO.ThemDanhMuc(danhMuc))
                    {
                        //Thông báo thất bại
                        return false;
                    }
                }
                if (DiaDiemDAO.ThemDiaDiem(diaDiem))
                {
                    //Thêm thành công                    
                    return true;
                }
                //Thông báo thất bại
                return false;
            }
            catch (Exception ex) { return false; }
        }

        [System.Web.Services.WebMethod]
        public static bool CapNhatDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu)
        {
            try
            {
                DanhMucDTO danhMuc = new DanhMucDTO("", CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(tenDiaDiem, viDo, kinhDo, ghiChu, danhMuc);

                if (DiaDiemDAO.CapNhatDiaDiem(diaDiem))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [System.Web.Services.WebMethod]
        public static bool XoaDiaDiem(string tenDiaDiem)
        {
            try
            {
                DanhMucDTO danhMuc = new DanhMucDTO("", CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(tenDiaDiem, 0, 0, "", danhMuc);

                if (DiaDiemDAO.XoaDiaDiem(diaDiem))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void LoadMyTreeView()
        {
            NguoiDungDTO nguoiDung = index.CurrentUser;
            XmlDocument doc = new XmlDocument();

            doc.Load(index.FilePath);
            for (int k = 0; k < doc.DocumentElement.ChildNodes.Count; ++k)
            {
                if (doc.DocumentElement.ChildNodes[k].Attributes[0].Value == nguoiDung.Username)
                {
                    XmlNode nguoiDungNode = doc.DocumentElement.ChildNodes[k];
                    for (int i = 0; i < nguoiDungNode.ChildNodes.Count; ++i)
                    {
                        Label l = new Label();
                        l.Text = "<strong>" + nguoiDungNode.ChildNodes[i].Attributes[0].Value + "</strong>";
                        CayDiaDiem.Controls.Add(l);

                        Literal li = new Literal();
                        li.Text = "<br/>";
                        CayDiaDiem.Controls.Add(li);
                        
                        for (int j = 0; j < nguoiDungNode.ChildNodes[i].ChildNodes.Count; ++j)
                        {
                            Literal l1 = new Literal();
                            l1.Text = "&nbsp&nbsp+&nbsp";
                            CayDiaDiem.Controls.Add(l1);

                            HyperLink a = new HyperLink();
                            a.Text = nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes[0].Value;
                            a.NavigateUrl = "javascript:(findMyLocation('" + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes[0].Value
                                + "', false, '"
                                + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["ghichu"].Value + "'))";
                            CayDiaDiem.Controls.Add(a);

                            Literal l2 = new Literal();
                            l2.Text = "<br/>";
                            CayDiaDiem.Controls.Add(l2);
                        }
                    }
                }
            }
        }
    }
}