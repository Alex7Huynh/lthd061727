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

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Load TreeView from XML file
        /// </summary>
        /// <param name="treeView"></param>
        public static void LoadTreeView(ref TreeView treeView)
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
        }

        /// <summary>
        /// Event when node on treeview clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            TreeView t = (TreeView)(sender);
            DiaDiem.Text = t.SelectedNode.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + t.SelectedNode.Value + "');", true);
        }

        /// <summary>
        /// Event when button DangXuat is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDangXuat_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }

        /// <summary>
        /// Insert new location
        /// </summary>
        /// <param name="tenDiaDiem"></param>
        /// <param name="viDo"></param>
        /// <param name="kinhDo"></param>
        /// <param name="ghiChu"></param>
        /// <param name="tenDanhMuc"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static int ThemDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu, int maDanhMuc, string tenDanhMuc)
        {
            try
            {
                Random rand = new Random();
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

        /// <summary>
        /// Update a location
        /// </summary>
        /// <param name="maDiaDiem"></param>
        /// <param name="tenDiaDiem"></param>
        /// <param name="viDo"></param>
        /// <param name="kinhDo"></param>
        /// <param name="ghiChu"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove a location
        /// </summary>
        /// <param name="maDiaDiem"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Load data like treeview to div tag from XML file
        /// </summary>
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

        /// <summary>
        /// Find nearest location
        /// </summary>
        /// <param name="viDoHienTai"></param>
        /// <param name="kinhDoHienTai"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string TimDiaDiemGanNhat(float viDoHienTai, float kinhDoHienTai, string danhMucTimKiem)
        {
            DiaDiemDTO diaDiemHienTai = new DiaDiemDTO();
            diaDiemHienTai.ViDo = viDoHienTai;
            diaDiemHienTai.KinhDo = kinhDoHienTai;
            DanhMucDTO danhMuc = DanhMucDAO.TimDanhMuc(danhMucTimKiem, CurrentUser);
            List<DiaDiemDTO> dsDiaDiem = DiaDiemDAO.TimDiaDiem(danhMuc);

            double minDistance = FindDistance(diaDiemHienTai, dsDiaDiem[0]);
            int index = 0;
            for (int i = 0; i < dsDiaDiem.Count; ++i)
            {
                if (FindDistance(diaDiemHienTai, dsDiaDiem[i]) < minDistance)
                {
                    minDistance = FindDistance(diaDiemHienTai, dsDiaDiem[i]);
                    index = i;
                }
            }

            return dsDiaDiem[index].TenDiaDiem;
        }

        /// <summary>
        /// Find distance between 2 locations
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double FindDistance(DiaDiemDTO p1, DiaDiemDTO p2)
        {
            double R = 6371; // earth's mean radius in km
            double dLat = rad(p2.ViDo - p1.ViDo);
            double dLong = rad(p2.KinhDo - p1.KinhDo);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(rad(p1.ViDo)) * Math.Cos(rad(p2.ViDo)) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }

        /// <summary>
        /// Calculate radian
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double rad(double x)
        {
            return x * Math.PI / 180;
        }

    }
}