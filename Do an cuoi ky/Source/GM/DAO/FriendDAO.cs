using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroSocialNetwork.DAO
{
    public class FriendDAO
    {
        internal static void MakeFriend(Guid myid, Guid iduser)
        {
            try
            {
                MashDataClassesDataContext db = new MashDataClassesDataContext();
                Friend friend = new Friend();
                friend.UserId = myid;
                friend.FriendId = iduser;
                db.Friends.InsertOnSubmit(friend);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}