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
        GMarkerOptions mOpts = new GMarkerOptions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                LoadUserLocation();
            }
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

            GMarker mkr = new GMarker();

            mOpts.draggable = true;

            mkr.point = glatlng;
            mkr.options = mOpts;
            GMap1.addGMarker(mkr);
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            Location location = new Location();
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
        }

        protected string GMap1_MapLoad(object s, GAjaxServerEventArgs e)
        {
            GMap GMap1 = (GMap)LoginView1.FindControl("GMap1");
            if (GMap1 != null)
            {
                GMap1.setCenter(new GLatLng(12.6844893, 108.0859396), 16, Subgurim.Controles.GMapType.GTypes.Normal);
            }
            return "";
        }

        protected string GMap1_Click(object s, GAjaxServerEventArgs e)
        {
            GMarker marker = new GMarker(e.point);

            mOpts.draggable = true;
            marker.options = mOpts;

            GInfoWindow window = new GInfoWindow(marker,
                string.Format(@"
                            <b>GLatLngBounds</b><br />
                            SW = {0}<br/>
                            NE = {1}
                            ",
                e.bounds.getSouthWest().ToString(),
                e.bounds.getNorthEast().ToString())
            , true);

            return window.ToString(e.map);
        }

        protected string GMap1_MarkerClick(object s, GAjaxServerEventArgs e)
        {
            return string.Format("alert('MarkerClick: {0} - {1}')", e.point.ToString(), DateTime.Now);
        }

        protected string GMap1_MoveStart(object s, GAjaxServerEventArgs e)
        {
            return "document.getElementById('messages1').innerHTML= 'MoveStart at " + e.point.ToString() + " - " + DateTime.Now.ToString() + "';";
        }

        protected string GMap1_MoveEnd(object s, GAjaxServerEventArgs e)
        {
            return "document.getElementById('messages2').innerHTML= 'MoveEnd at " + e.point.ToString() + " - " + DateTime.Now.ToString() + "';";
        }

        protected string GMap1_DragStart(object s, GAjaxServerEventArgs e)
        {
            GMarker marker = new GMarker(e.point);
            GInfoWindow window = new GInfoWindow(marker, "DragStart - " + DateTime.Now.ToString(), false);
            return window.ToString(e.map);
        }

        protected string GMap1_DragEnd(object s, GAjaxServerEventArgs e)
        {
            GMarker marker = new GMarker(e.point);
            GInfoWindow window = new GInfoWindow(marker, "DragEnd - " + DateTime.Now.ToString(), false);
            return window.ToString(e.map);
        }

        protected string GMap1_ZoomEnd(object s, GAjaxServerEventZoomArgs e)
        {
            return string.Format("alert('oldLevel/newLevel: {0}/{1} - {2}')", e.oldLevel, e.newLevel, DateTime.Now);
        }

        protected string GMap1_MapTypeChanged(object s, GAjaxServerEventMapArgs e)
        {
            return string.Format("alert('{0}')", e.mapType.ToString());
        } 
    }
}