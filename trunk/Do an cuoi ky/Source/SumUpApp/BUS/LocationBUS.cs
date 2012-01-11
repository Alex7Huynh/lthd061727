using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumUpApp.BUS
{
    public class LocationBUS
    {
        public static Guid AddLocation(Location location)
        {
            return DAO.LocationDAO.AddLocation(location);

        }
        public static bool RemoveLocation(Location location)
        {
            return DAO.LocationDAO.RemoveLocation(location);
        }
        public static bool RemoveLocation(string ID)
        {
            return DAO.LocationDAO.RemoveLocation(ID);
        }
        public static Guid UpdateLocation(Location location)
        {
            return DAO.LocationDAO.UpdateLocation(location);

        }
        public static Location FindLocationByName(string name)
        {
            return DAO.LocationDAO.FindLocationByName(name);
        }
        public static Location FindLocation(Guid id)
        {
            return DAO.LocationDAO.FindLocation(id);
        }
        public static Location FindLocation(string ID)
        {
            return DAO.LocationDAO.FindLocation(ID);
        }
        public static List<Location> FindLocation(LocationCategory category)
        {
            return DAO.LocationDAO.FindLocation(category);
        }
        public static List<Location> GetAll()
        {
            return DAO.LocationDAO.GetAll();
        }
        public static List<Location> FindNearbyLocation(Location centerlocation)
        {
            return DAO.LocationDAO.FindNearbyLocation(centerlocation);
        }
        public static double FindDistance(Location p1, Location p2)
        {
            return DAO.LocationDAO.FindDistance(p1, p2);
        }
    }
}