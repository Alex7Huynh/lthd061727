using System;
using System.Collections.Generic;
using System.Web.Security;

namespace CaroSocialNetwork.BUS
{
    public class LocationCategoryBUS
    {
        public static List<LocationCategory> GetAll(MembershipUser user)
        {
            return DAO.LocationCategoryDAO.GetAll(user);
        }
        public static Guid AddCategory(LocationCategory category)
        {
            return DAO.LocationCategoryDAO.AddCategory(category);

        }
        public static bool UpdateCategory(LocationCategory category)
        {
            return DAO.LocationCategoryDAO.UpdateCategory(category);
        }
        public static bool RemoveCategory(LocationCategory category)
        {
            return DAO.LocationCategoryDAO.RemoveCategory(category);
        }
        public static LocationCategory FindCategory(string name, MembershipUser user)
        {
            return DAO.LocationCategoryDAO.FindCategory(name, user);
        }
    }
}