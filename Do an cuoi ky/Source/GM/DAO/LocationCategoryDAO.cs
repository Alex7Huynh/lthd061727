using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CaroSocialNetwork.DAO
{
    public class LocationCategoryDAO
    {
        public static bool AddCategory(LocationCategory category)
        {
            try
            {                
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                db.LocationCategories.InsertOnSubmit(category);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool UpdateCategory(LocationCategory category)
        {
            try
            {
                //Search
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                LocationCategory newCategory = new LocationCategory();
                newCategory = db.LocationCategories.Single(t => t.CategoryID == category.CategoryID);
                //Update
                newCategory.Name = category.Name;
                newCategory.ParentCategoryID = category.ParentCategoryID;
                //Submit
                db.SubmitChanges();
            }
            catch (Exception ex)
            { return false; }
            return true;
        }
        public static bool RemoveCategory(LocationCategory category)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                LocationCategory newCategory = new LocationCategory();
                newCategory = db.LocationCategories.Single(t => t.CategoryID == category.CategoryID);
                //Update
                newCategory.Deleted = true;
                //Submit
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static LocationCategory FindCategory(string name, MembershipUser user)
        {
            LocationCategory category = new LocationCategory();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                category = db.LocationCategories.Single(t => t.Name == name & t.UserID.ToString() == user.ProviderUserKey.ToString());
            }
            catch (Exception ex)
            { return null; }

            return category;
        }
    }
}