using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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

        public List<Room> GetRoomList(string username)
        {
            List<Room> result = new List<Room>();
            foreach (Room room in rooms)
            {
                if (!room.IsEmpty() && !room.IsFull() && room.HasPlayer(username))
                    result.Add(room);
            }
            return result;
        }

        public void CreateRoom(string username, bool playwithmachine, out int roomId)
        {
            Room room = new Room();
            room.PlayWithMachine = playwithmachine;
            room.AddUserPlayer(username);
            
            if (playwithmachine)
            {
                room.AddMachine();
            }
            currentIndex++;
            room.Id = currentIndex;
            rooms.Add(room);
            roomId = room.Id;
        }

        public void JoinRoom(int roomid, string username)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].AddUserPlayer(username);
            }
        }

        private int FindRoom(int roomid)
        {
            for (int i = 0; i < rooms.Count; i++)
                if (rooms[i].Id == roomid)
                    return i;
            return -1;
        }

        public void LeaveRoom(int roomid, string username)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].RemoveUserPlayer(username);
            }

            if (rooms[index].IsEmpty())
            {
                rooms.RemoveAt(index);
            }
        }

        public void Move(int roomid, string username, int userX, int userY)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].Move(username, userX, userY);
            }
        }

        internal bool IsMyTurn(int roomid, string userName)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].IsMyTurn(userName);
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

        internal bool IsWin(int roomId, string username)
        {
            int index = FindRoom(roomId);
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index].IsWin(username);
            }
            return false;
        }
        #endregion
    }
}
