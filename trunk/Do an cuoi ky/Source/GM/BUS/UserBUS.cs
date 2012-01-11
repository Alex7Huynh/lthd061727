using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaroSocialNetwork.DAO;

namespace CaroSocialNetwork.BUS
{
    public class UserBUS
    {
        internal static Guid FindUser(Guid locationid)
        {
            return UserDAO.FindUser(locationid);
        }

        internal static string GetUserName(Guid iduser)
        {
            return UserDAO.GetUserName(iduser);
        }
    }
}