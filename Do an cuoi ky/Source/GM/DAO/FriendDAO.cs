using System;
using System.Collections.Generic;

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

        internal static bool CheckMyFriend(Guid myid, Guid iduser)
        {
            MashDataClassesDataContext db = new MashDataClassesDataContext();
            foreach (Friend friend in db.Friends)
            {
                if (friend.UserId == myid && friend.FriendId == iduser)
                    return true;
            }
            return false;
        }

        internal static List<Guid> GetFriendIds(Guid myid)
        {
            MashDataClassesDataContext db = new MashDataClassesDataContext();
            List<Guid> idList = new List<Guid>();
            foreach (Friend friend in db.Friends)
            {
                if (friend.UserId == myid)
                    idList.Add(friend.FriendId);
            }
            return idList;
        }
    }
}