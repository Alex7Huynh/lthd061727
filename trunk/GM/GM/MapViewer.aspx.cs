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
        GListener dblclickListener;

        protected void Page_Load(object sender, EventArgs e)
        {
            mOpts.draggable = true;
            if (!Page.IsPostBack)
            {
                LoadUserLocation();
                InitMap();
            }
            else
            {
                Find();
            }
        }

        private void Find()
        {
            //Find and add new location
            GMap GMap1 = (GMap)LoginView1.FindControl("GMap1");
            TextBox txtAddress = (TextBox)LoginView1.FindControl("txtAddress");

            GeoCode geocode;
            geocode = GMap1.getGeoCodeRequest(txtAddress.Text);
            var glatlng = new Subgurim.Controles.GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
            GMap1.setCenter(glatlng, 16, Subgurim.Controles.GMapType.GTypes.Normal);

            GMarker mkr = new GMarker();
            dblclickListener = new GListener(mkr.ID, GListener.Event.dblclick, "gmarkerdblclick()");
            GMap1.addListener(dblclickListener);
            mkr.point = glatlng;
            mkr.options = mOpts;
            GMap1.addGMarker(mkr);
        }

        private void InitMap()
        {
            GMap GMap1 = (GMap)LoginView1.FindControl("GMap1");

            if (GMap1 != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("function addMarker()");
                sb.Append("{");
                GMarker marker = new GMarker(GMap1.GMap_Id + ".getCenter()");
                marker.options = mOpts;
                dblclickListener = new GListener(marker.ID, GListener.Event.dblclick, "gmarkerdblclick()");
                GMap1.addListener(dblclickListener);
                sb.Append(marker.ToString(GMap1.GMap_Id));
                sb.Append("}");

                sb.Append("function deleteAllMarker()");
                sb.Append("{");
                sb.AppendFormat("{0}.clearOverlays();", GMap1.GMap_Id);
                sb.Append("}");

                GMap1.Add(sb.ToString());
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
            Find();
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
        }

        protected void btnUpdateMovementCurrentLocation_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelMovementCurrentLocation_Click(object sender, EventArgs e)
        {

        }

        protected void btnGMKDblclickHandler_Click(object sender, EventArgs e)
        {
            Location location = new Location();
            int i = 0;
        }
    }
}