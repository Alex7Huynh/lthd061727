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
        public static int ThemDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu, string tenDanhMuc)
        {
            try
            {
                Random rand = new Random();
                int maDanhMuc = rand.Next();
                int maDiaDiem = rand.Next();
                DanhMucDTO danhMuc = new DanhMucDTO(maDanhMuc, tenDanhMuc, CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(maDiaDiem, tenDiaDiem, viDo, kinhDo, ghiChu, danhMuc);
                if (DanhMucDAO.TimDanhMuc(danhMuc.TenDanhMuc, CurrentUser) == null)
                {
                    if (!DanhMucDAO.ThemDanhMuc(danhMuc))
                    {
                        //Thông báo thất bại
                        return -1;
                    }
                }
                if (DiaDiemDAO.ThemDiaDiem(diaDiem))
                {
                    //Thêm thành công                    
                    return maDiaDiem;
                }
                //Thông báo thất bại
                return -1;
            }
            catch (Exception ex) { return -1; }
        }

        [System.Web.Services.WebMethod]
        public static bool CapNhatDiaDiem(int maDiaDiem, string tenDiaDiem, float viDo, float kinhDo, string ghiChu)
        {
            try
            {
                DanhMucDTO danhMuc = new DanhMucDTO(0, "", CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(maDiaDiem, tenDiaDiem, viDo, kinhDo, ghiChu, danhMuc);

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
        public static bool XoaDiaDiem(int maDiaDiem)
        {
            try
            {
                Random rand = new Random();
                DanhMucDTO danhMuc = new DanhMucDTO(0, "", CurrentUser);
                DiaDiemDTO diaDiem = new DiaDiemDTO(maDiaDiem, "", 0, 0, "", danhMuc);

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
                        l.ID = "DM" + nguoiDungNode.ChildNodes[i].Attributes["madanhmuc"].Value;
                        
                        
                        //CayDiaDiem.Controls.Add(l);

                        Literal li = new Literal();
                        //li.Text = "<br/>";
                        li.Text = "<strong>" + nguoiDungNode.ChildNodes[i].Attributes["tendanhmuc"].Value + "</strong><br/>";
                        l.Controls.Add(li);
                        //CayDiaDiem.Controls.Add(li);

                        for (int j = 0; j < nguoiDungNode.ChildNodes[i].ChildNodes.Count; ++j)
                        {
                            /*Literal l1 = new Literal();
                            l1.Text = "&nbsp&nbsp+&nbsp";
                            CayDiaDiem.Controls.Add(l1);*/

                            HyperLink a = new HyperLink();
                            a.ID = "DD" + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["madiadiem"].Value;
                            a.Text = "&nbsp&nbsp+&nbsp" + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["tendiadiem"].Value + "<br/>";
                            a.NavigateUrl = "javascript:(findMyLocation('"
                                + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["madiadiem"].Value
                                + "', '" + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["tendiadiem"].Value + "', '"
                                + nguoiDungNode.ChildNodes[i].ChildNodes[j].Attributes["ghichu"].Value + "'))";
                            l.Controls.Add(a);
                            //CayDiaDiem.Controls.Add(a);

                            /*Literal l2 = new Literal();
                            l2.Text = "<br/>";
                            CayDiaDiem.Controls.Add(l2);*/
                        }
                        //l.Text = "<strong>" + nguoiDungNode.ChildNodes[i].Attributes["tendanhmuc"].Value + "</strong><br/>";
                        CayDiaDiem.Controls.Add(l);
                    }
                }
            }
        }
    }
}