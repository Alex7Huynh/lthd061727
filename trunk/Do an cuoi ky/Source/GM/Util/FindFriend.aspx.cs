using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CaroSocialNetwork.BUS;

namespace CaroSocialNetwork
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Find Friend";

            LoadMyTreeView();
            ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: initialize(); ", true);
        }

        public void LoadMyTreeView()
        {
            MembershipUser user = Membership.GetUser();
            List<LocationCategory> categories = LocationCategoryBUS.GetAll(user);

            for (int i = 0; i < categories.Count; ++i)
            {
                LocationCategory category = categories[i];
                Label l = new Label();
                l.ID = "DM" + category.CategoryID.ToString();

                Literal li = new Literal();
                li.Text = "<strong>" + category.Name.ToString() + "</strong><br/>";
                l.Controls.Add(li);

                for (int j = 0; j < category.Locations.Count; ++j)
                {
                    // Inorge location which has been deleted
                    if (category.Locations[j].Deleted == true)
                        continue;

                    Location location = category.Locations[j];

                    HyperLink a = new HyperLink();
                    a.ID = "DD" + location.LocationID.ToString();
                    a.Text = "&nbsp&nbsp+&nbsp" + location.Name.ToString() + "<br/>";
                    a.NavigateUrl = "javascript:(findMyLocation('"
                        + location.LocationID.ToString()
                        + "', '" + location.Name.ToString() + "', '"
                        + location.Note.ToString() + "'))";
                    l.Controls.Add(a);
                }
                mylocations.Controls.Add(l);
            }
        }

        public class LocationTemp
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

            private Guid _userid;
            public Guid UserId
            {
                get { return _userid; }
                set { _userid = value; }
            }

            private string _username;
            public string UserName
            {
                get { return _username; }
                set { _username = value; }
            }

            private bool _isMyFriend;
            public bool IsMyFriend
            {
                get { return _isMyFriend; }
                set { _isMyFriend = value; }
            }
        }

        [System.Web.Services.WebMethod]
        public static List<LocationTemp> GetNearbyLocations(Guid idAddress)
        {
            List<Location> nbl = LocationBUS.FindNearbyLocation(LocationBUS.FindLocation(idAddress));

            List<LocationTemp> tempList = new List<LocationTemp>();
            foreach (Location location in nbl)
            {
                Guid userid = UserBUS.FindUser(location.LocationID);
                if (userid != (Guid)Membership.GetUser().ProviderUserKey)
                {
                    LocationTemp temp = new LocationTemp();
                    temp.Name = location.Name;
                    temp.Id = location.LocationID;
                    temp.UserId = userid;
                    temp.UserName = GetUserName(temp.UserId);
                    temp.IsMyFriend = CheckMyFriend(temp.UserId);
                    tempList.Add(temp);
                }
            }
            return tempList;
        }

        [System.Web.Services.WebMethod]
        public static void MakeFriend(Guid iduser)
        {
            Guid myid = (Guid)Membership.GetUser().ProviderUserKey;
            FriendBUS.MakeFriend(myid, iduser);
        }

        [System.Web.Services.WebMethod]
        public static string GetUserName(Guid iduser)
        {
            return UserBUS.GetUserName(iduser);
        }

        [System.Web.Services.WebMethod]
        public static bool CheckMyFriend(Guid iduser)
        {
            Guid myid = (Guid)Membership.GetUser().ProviderUserKey;
            return FriendBUS.CheckMyFriend(myid, iduser);
        }
    }
}