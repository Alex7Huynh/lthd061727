using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroSocialNetwork.DAO
{
    public class LocationDAO
    {
        public static Guid AddLocation(Location location)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                db.Locations.InsertOnSubmit(location);
                db.SubmitChanges();
                return location.LocationID;
            }
            catch (Exception ex)
            {
                return new Guid("");
            }

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
        public static bool RemoveLocation(string ID)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                Location newLocation = new Location();
                newLocation = db.Locations.Single(t => t.LocationID == new Guid(ID));
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
        public static Guid UpdateLocation(Location location)
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
                return location.LocationID;
            }
            catch (Exception ex)
            { return new Guid(""); }

        }
        public static Location FindLocationByName(string name)
        {
            Location location = new Location();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                var results = from q in db.Locations
                              where location.Name.Contains(name)
                              select q;
                location = results.Single();
            }
            catch (Exception ex)
            { return null; }
            return location;
        }
        public static Location FindLocation(Guid id)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                foreach (Location location in db.Locations)
                {
                    if (location.LocationID == id)
                        return location;
                }
                return null;
            }
            catch (Exception ex)
            { return null; }
        }
        public static Location FindLocation(string ID)
        {
            List<Location> locations = GetAll();
            foreach (Location l in locations)
            {
                if (l.LocationID.ToString() == ID)
                    return l;
            }
            return null;
        }
        public static List<Location> FindLocation(LocationCategory category)
        {
            List<Location> dsDiaDiem = new List<Location>();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                var results = from q in db.Locations
                              where q.CategoryID.ToString() == category.CategoryID.ToString() && q.Deleted == false
                              select q;
                dsDiaDiem = results.ToList<Location>();
            }
            catch (Exception ex)
            { return null; }
            return dsDiaDiem;
        }
        public static List<Location> GetAll()
        {
            List<Location> dsDiaDiem = new List<Location>();
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                var dsNguoiDung = from q in db.Locations
                                  select q;
                dsDiaDiem = dsNguoiDung.ToList<Location>();
            }
            catch (Exception ex)
            { return null; }
            return dsDiaDiem;
        }

        public static List<Location> FindNearbyLocation(Location centerlocation)
        {
            MashDataClassesDataContext db = new MashDataClassesDataContext();
            List<Location> returnLocations = new List<Location>();
            foreach (Location location in db.Locations)
            {
                if (FindDistance(location, centerlocation) <= 2 && location.LocationID != centerlocation.LocationID)
                    returnLocations.Add(location);
            }
            return returnLocations;
        }
        public static double FindDistance(Location p1, Location p2)
        {
            double R = 6371; // earth's mean radius in km
            double dLat = rad(p2.Latitude.Value - p1.Latitude.Value);
            double dLong = rad(p2.Longitude.Value - p1.Longitude.Value);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(rad(p1.Latitude.Value)) * Math.Cos(rad(p2.Latitude.Value)) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }
        public static double rad(double x)
        {
            return x * Math.PI / 180;
        }
    }
}