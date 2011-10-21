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
using Subgurim.Controles;

namespace GM
{
    public partial class MapViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUserLocation();
                //var adddress = document.getElementById('<%=(((TextBox)LoginView1.FindControl("txtAddress")).ClientID)%>').value;
        }

        private void LoadUserLocation()
        {
            // Get a reference to the currently logged on user 
            MembershipUser currentUser = Membership.GetUser();

            if (currentUser != null)
            {
                // Determine the currently logged on user's UserId value 
                Guid currentUserId = (Guid)currentUser.ProviderUserKey;
                TreeNode root = new TreeNode("My Locations", "");

                DataClasses1DataContext context = new DataClasses1DataContext();
                foreach (LocationCategory category in context.LocationCategories)
                {
                    if (category.UserID == currentUserId)
                    {
                        TreeNode sub = new TreeNode(category.Name, category.CategoryID.ToString());
                        LoadTreeViewLoop(sub, category, context);
                        root.ChildNodes.Add(sub);
                    }
                }


                TreeView myLocation = (TreeView)LoginView1.FindControl("MyLocationTreeView");
                myLocation.Nodes.Clear();
                myLocation.Nodes.Add(root);
            }
        }

        private void LoadTreeViewLoop(TreeNode parent, LocationCategory categoryParent, DataClasses1DataContext context)
        {
            foreach (Location location in context.Locations)
            {
                if (location.CategoryID == categoryParent.CategoryID)
                {
                    TreeNode leaf = new TreeNode(location.Name, location.LocationID.ToString());
                    parent.ChildNodes.Add(leaf);
                }
            }

            foreach (LocationCategory category in categoryParent.LocationCategories)
            {
                TreeNode children = new TreeNode(category.Name, category.CategoryID.ToString());
                parent.ChildNodes.Add(children);
                LoadTreeViewLoop(children, category, context);
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            GMap GMap1 = (GMap)LoginView1.FindControl("GMap1");
            TextBox txtAddress = (TextBox)LoginView1.FindControl("txtAddress");
            string fulladdress = string.Format("{0}", txtAddress.Text);
            string skey = ConfigurationManager.AppSettings["googlemaps.subgurim.net"];
            GeoCode geocode;
            geocode = GMap1.getGeoCodeRequest(fulladdress);
            var glatlng = new Subgurim.Controles.GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
            GMap1.setCenter(glatlng, 16, Subgurim.Controles.GMapType.GTypes.Normal);
            var oMarker = new Subgurim.Controles.GMarker(glatlng);
            GMap1.addGMarker(oMarker);
        }


        protected void setMarker1_Click(object sender, EventArgs e)
        {

        }

        protected void setLocation_Click(object sender, EventArgs e)
        {
            TextBox diadiem = (TextBox)LoginView1.FindControl("DiaDiem");
            string address = diadiem.Text;
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
    }
}