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

namespace CaroSocialNetwork
{
    public partial class MapViewer : System.Web.UI.Page, ICallbackEventHandler
    {
        protected String returnValue;
        protected void Page_Load(object sender, EventArgs e)
        {
            String cbReference = ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
            String callbackScript;
            callbackScript = "function CallServer(arg, context)" +
                "{ " + cbReference + ";}";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                            "CallServer", callbackScript, true);

            HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("pageBody");
            body.Attributes.Add("onload", "initialize()");

            LoadUserLocation();
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

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
        }

        public String GetCallbackResult()
        {
            return returnValue;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            try
            {
                string[] strSplit = eventArgument.Split(';');
                double lng = double.Parse(strSplit[0]);
                double lat = double.Parse(strSplit[1]);
                Location location = new Location();
                location.Longitude = lng;
                location.Latitude = lat;

                DataClasses1DataContext context = new DataClasses1DataContext();
                context.Locations.InsertOnSubmit(location);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                returnValue = string.Empty;
            }
        }
    }
}