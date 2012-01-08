using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CaroSocialNetwork.DAO;

namespace CaroSocialNetwork
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static List<Location> myLocations = new List<Location>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ListMyPlaces();
        }

        private void ListMyPlaces()
        {
            MembershipUser user = Membership.GetUser();
            List<LocationCategory> categories = LocationCategoryDAO.GetAll(user);

            for (int i = 0; i < categories.Count; ++i)
            {
                LocationCategory category = categories[i];

                for (int j = 0; j < category.Locations.Count; ++j)
                {
                    // Inorge location which has been deleted
                    if (category.Locations[j].Deleted == true)
                        continue;

                    Location location = category.Locations[j];

                    myLocations.Add(location);
                }
            }

            lstboxMyPlaces.DataSource = myLocations;
            lstboxMyPlaces.DataTextField = "Name";
            lstboxMyPlaces.DataValueField = "LocationId";
            lstboxMyPlaces.DataBind();
        }

        protected void lstboxMyPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Location> nearbyLocations = LocationDAO.FindNearbyLocation(myLocations[lstboxMyPlaces.SelectedIndex]);
            List<aspnet_User> users = new List<aspnet_User>();

            foreach (Location location in nearbyLocations)
            {
                if (location.LocationCategory.aspnet_User.UserId != (Guid)Membership.GetUser().ProviderUserKey)
                {
                    users.Add(location.LocationCategory.aspnet_User);
                }
            }

            lstBoxPeoplesNearby.DataSource = users;
            lstBoxPeoplesNearby.DataTextField = "UserName";
            lstBoxPeoplesNearby.DataValueField = "UserId";
            lstBoxPeoplesNearby.DataBind();
        }

        protected void lstBoxPeoplesNearby_SelectedIndexChanged(object sender, EventArgs e)
        {
            string username = lstBoxPeoplesNearby.SelectedItem.Text;
            List<Room> roomlist = Global.roomManager.GetRoomList(username);

            lstRooms.DataSource = roomlist;
            lstRooms.DataTextField = "Name";
            lstRooms.DataValueField = "Id";
            lstRooms.DataBind();
        }
    }
}