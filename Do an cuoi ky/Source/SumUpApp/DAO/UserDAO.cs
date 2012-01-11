using System;

namespace SumUpApp.DAO
{
    public class UserDAO
    {
        internal static Guid FindUser(Guid locationid)
        {
            MashDataClassesDataContext db = new MashDataClassesDataContext();
            foreach (aspnet_User user in db.aspnet_Users)
            {
                foreach (LocationCategory category in user.LocationCategories)
                {
                    foreach (Location location in category.Locations)
                    {
                        if (location.LocationID == locationid)
                            return user.UserId;
                    }
                }
            }
            return new Guid();
        }

        internal static string GetUserName(Guid iduser)
        {
            MashDataClassesDataContext db = new MashDataClassesDataContext();
            foreach (aspnet_User user in db.aspnet_Users)
            {
                if (user.UserId == iduser)
                    return user.UserName;
            }
            return "";
        }
    }
}