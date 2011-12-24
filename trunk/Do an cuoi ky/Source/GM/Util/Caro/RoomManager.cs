using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CaroSocialNetwork
{
    public class RoomManager
    {
        static List<Room> rooms = new List<Room>();
        static int currentIndex = -1;

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

        public void CreateRoom(string username, bool playwithmachine, out int roomId)
        {
            Room room = new Room();
            room.PlayWithMachine = playwithmachine;
            Player player = new Player() { Name = username };
            if (room.AddPlayer(player))
            {
                if (playwithmachine)
                {
                    if (!room.IsFull())
                    {
                        Player machine = new Machine();
                        room.AddPlayer(machine);
                    }
                }
                currentIndex++;
                room.Id = currentIndex;
                rooms.Add(room);
                roomId = room.Id;
            }
            else
                roomId = -1;
        }

        public bool JoinRoom(int roomid, string username)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                if (rooms[index].AddPlayer(new Player() { Name = username }))
                {
                    return true;
                }
            }

            return false;
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
                rooms[index].RemovePlayer(username);
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
                if (!rooms[index].GameOver)
                    rooms[index].Move(username, userX, userY);
            }
        }

        public void WaitingForOpponent(int roomid, string username)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].WaitingForOpponent(username);
            }
        }

        public void RegistryComplete(int roomid, string username, MoveWaitingEvent method)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                rooms[index].WaitingComplete += new MoveWaitingEvent(method);
            }
        }

        internal bool IsMyTurn(int roomid, string userName)
        {
            int index = FindRoom(roomid);
            if (index >= 0 && index < rooms.Count)
            {
                if (rooms[index].GetCurrentTurn() == userName)
                    return true;
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
    }
}
