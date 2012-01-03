using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroSocialNetwork.DAO
{
    public class LocationDAO
    {
        public static bool AddLocation(Location location)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                db.Locations.InsertOnSubmit(location);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool RemoveLocation(Location location)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                Location newLocation = new Location();
                newLocation = db.Locations.Single(t => t.LocationID == location.LocationID);
                //Update
                newLocation.Deleted = true;                
                //Submit
                db.SubmitChanges();
                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool UpdateLocation(Location location)
        {
            try
            {
                //Search
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                Location newLocation = new Location();
                newLocation = db.Locations.Single(t => t.LocationID == location.LocationID);
                //Update
                newLocation.Name = location.Name;
                newLocation.Longitude = location.Longitude;
                newLocation.Latitude = location.Latitude;
                newLocation.Note = location.Note;
                //Submit
                db.SubmitChanges();
            }
            catch (Exception ex)
            { return false; }
            return true;
        }
        public static Location FindLocation(string name)
        {
            Location location = new Location();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                var dsNguoiDung = from q in db.Locations
                                  where location.Name.Contains(name)
                                  select q;
                location = dsNguoiDung.Single();
            }
            catch (Exception ex)
            { return null; }
            return location;
        }
        public static List<Location> FindLocation(LocationCategory category)
        {
            List<Location> dsDiaDiem = new List<Location>();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                var dsNguoiDung = from q in db.Locations
                                  where q.LocationID.ToString() == category.ParentCategoryID.ToString()
                                  select q;
                dsDiaDiem = dsNguoiDung.ToList<Location>();
            }
            catch (Exception ex)
            { return null; }
            return dsDiaDiem;
        }
    }
}