using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroSocialNetwork.BUS
{
    public class FriendBUS
    {
        internal static void MakeFriend(Guid myid, Guid iduser)
        {
            DAO.FriendDAO.MakeFriend(myid, iduser);
        }

        internal static bool CheckMyFriend(Guid myid, Guid iduser)
        {
            return DAO.FriendDAO.CheckMyFriend(myid, iduser);
        }

        internal static List<Guid> GetFriendIds(Guid myid)
        {
            return DAO.FriendDAO.GetFriendIds(myid);
        }
    }
}