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
using CaroSocialNetwork.DAO;

namespace CaroSocialNetwork
{
    public partial class GMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Map";

            LoadMyTreeView();
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: initialize(); ", true);
        }

        public class CategoryTemp
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private Guid _id;
            public Guid Id
            {
                get { return _id; }
                set { _id = value; }
            }
        }


        [System.Web.Services.WebMethod]
        public static List<CategoryTemp> GetCategories()
        {
            List<LocationCategory> lc = LocationCategoryDAO.GetAll(Membership.GetUser());
            List<CategoryTemp> tempList = new List<CategoryTemp>();
            foreach (LocationCategory category in lc)
            {
                CategoryTemp temp = new CategoryTemp();
                temp.Name = category.Name;
                temp.Id = category.CategoryID;
                tempList.Add(temp);
            }
            return tempList;
        }

        [System.Web.Services.WebMethod]
        public static void ThemDiaDiem(string tenDiaDiem, float viDo, float kinhDo, string ghiChu, Guid maDanhMuc)
        {
            Location location = new Location();
            location.Name = tenDiaDiem;
            location.Deleted = false;
            location.Note = ghiChu;
            location.CategoryID = maDanhMuc;
            location.Latitude = viDo;
            location.Longitude = kinhDo;
            LocationDAO.AddLocation(location);
        }

        [System.Web.Services.WebMethod]
        public static bool CapNhatDiaDiem(int maDiaDiem, string tenDiaDiem, float viDo, float kinhDo, string ghiChu)
        {
            return true;
        }

        [System.Web.Services.WebMethod]
        public static bool XoaDiaDiem(int maDiaDiem)
        {
            return true;
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            LocationCategory category = new LocationCategory();
            category.Name = txtCategoryName.Text;
            category.Deleted = false;
            category.UserID = (Guid)Membership.GetUser().ProviderUserKey;
            LocationCategoryDAO.AddCategory(category);
        }

        public void LoadMyTreeView()
        {
            MembershipUser user = Membership.GetUser();
            //treeView.Nodes.Clear();
            List<LocationCategory> categories = LocationCategoryDAO.GetAll(user);

            for (int i = 0; i < categories.Count; ++i)
            {
                LocationCategory category = categories[i];
                Label l = new Label();
                l.ID = "DM" + category.CategoryID.ToString();

                //CayDiaDiem.Controls.Add(l);

                Literal li = new Literal();
                //li.Text = "<br/>";
                li.Text = "<strong>" + category.Name.ToString() + "</strong><br/>";
                l.Controls.Add(li);
                //CayDiaDiem.Controls.Add(li);

                for (int j = 0; j < category.Locations.Count; ++j)
                {
                    Location location = category.Locations[j];
                    /*Literal l1 = new Literal();
                    l1.Text = "&nbsp&nbsp+&nbsp";
                    CayDiaDiem.Controls.Add(l1);*/

                    HyperLink a = new HyperLink();
                    a.ID = "DD" + location.LocationID.ToString();
                    a.Text = "&nbsp&nbsp+&nbsp" + location.Name.ToString() + "<br/>";
                    a.NavigateUrl = "javascript:(findMyLocation('"
                        + location.LocationID.ToString()
                        + "', '" + location.Name.ToString() + "', '"
                        + location.Note.ToString() + "'))";
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

        [System.Web.Services.WebMethod]
        public static string TimDiaDiemGanNhat(float viDoHienTai, float kinhDoHienTai, string danhMucTimKiem)
        {
            return String.Empty;
        }

        public static double FindDistance(Location p1, Location p2)
        {
            double R = 6371; // earth's mean radius in km
            double dLat = rad(p2.Latitude.Value - p1.Latitude.Value);
            double dLong = rad(p2.Longitude.Value - p1.Longitude.Value);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(rad(p1.Latitude.Value)) * Math.Cos(rad(p2.Latitude.Value)) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }

        public static double rad(double x)
        {
            return x * Math.PI / 180;
        }
    }
}