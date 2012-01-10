using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CaroSocialNetwork.DAO;

namespace CaroSocialNetwork
{
    public class RoomManager
    {
        #region Attributes
        static List<Room> rooms = new List<Room>();
        static int currentIndex = -1;
        #endregion

        #region Methods
        public List<Room> GetRoomList()
        {
            List<Room> result = new List<Room>();
            foreach (Room room in rooms)
            {
                if (!room.IsEmpty() && !room.IsFull())
                    result.Add(room);
            }
            return result;
        }

        public List<Room> GetRoomList(Guid userid)
        {
            List<Room> result = new List<Room>();
            List<Guid> friendIdList = FriendDAO.GetFriendIds(userid);
            foreach (Room room in rooms)
            {
                foreach (Guid friendid in friendIdList)
                {
                    if (!room.IsEmpty() && !room.IsFull() && room.HasPlayer(friendid))
                        result.Add(room);
                }
            }
            return result;
        }

        public void CreateRoom(Guid id, string username, bool playwithmachine, out int roomId)
        {
            Room room = new Room();
            room.PlayWithMachine = playwithmachine;
            room.AddUserPlayer(id, username);
            
            if (playwithmachine)
            {
                room.AddMachine();
            }
            currentIndex++;
            room.Id = currentIndex;
            rooms.Add(room);
            roomId = room.Id;
        }

        public void JoinRoom(int roomid, Guid userid, string username)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].AddUserPlayer(userid, username);
            }
        }

        private int FindRoom(int roomid)
        {
            for (int i = 0; i < rooms.Count; i++)
                if (rooms[i].Id == roomid)
                    return i;
            return -1;
        }

        public void LeaveRoom(int roomid, Guid userid)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].RemoveUserPlayer(userid);
            }

            if (rooms[index].IsEmpty())
            {
                rooms.RemoveAt(index);
            }
        }

        public void Move(int roomid, Guid userid, int userX, int userY)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].Move(userid, userX, userY);
            }
        }

        internal bool IsMyTurn(int roomid, Guid userid)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].IsMyTurn(userid);
            }
            return false;
        }

        internal int[] GetLastMove(int roomid)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].GetLastMove();
            }
            return new int[] {-1, -1};
        }

        internal bool IsGameOver(int roomId)
        {
            int index = FindRoom(roomId);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].IsGameOver();
            }
            return true;
        }

        internal bool IsWin(int roomId, Guid userid)
        {
            int index = FindRoom(roomId);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].IsWin(userid);
            }
            return false;
        }
        #endregion
    }
}
