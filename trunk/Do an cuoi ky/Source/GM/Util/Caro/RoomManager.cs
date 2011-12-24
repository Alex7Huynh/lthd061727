using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CaroSocialNetwork
{
    public class RoomManager
    {
        List<Room> rooms = new List<Room>();

        public event PlayerEvent PlayerJoin;
        public event PlayerEvent PlayerLeave;
        public event YourTurnEvent YourTurn;
        public event GameOverEvent GameOver;
        public delegate void PlayerEvent();
        public delegate void YourTurnEvent(int lastX, int lastY);
        public delegate void GameOverEvent();

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

        public bool CreateRoom(string username, bool playwithmachine, out int roomIndex)
        {
            Room room = new Room();
            Player player = new Player() { Name = username };
            if (room.AddPlayer(player))
            {
                PlayerJoin();
                if (playwithmachine)
                {
                    if (!room.IsFull())
                    {
                        Player machine = new Machine();
                        room.AddPlayer(machine);
                        rooms.Add(room);
                        roomIndex = rooms.IndexOf(room);
                        return true;
                    }
                }
                else
                {
                    rooms.Add(room);
                    roomIndex = rooms.IndexOf(room);
                    return true;
                }
            }

            roomIndex = -1;

            return false;
        }

        public bool JoinRoom(int roomid, string username)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                if (rooms[roomid].AddPlayer(new Player() { Name = username }))
                {
                    PlayerJoin();
                    return true;
                }
            }

            return false;
        }

        public void LeaveRoom(ref int roomid, string username)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                rooms[roomid].RemovePlayer(username);
                PlayerLeave();
            }

            if (rooms[roomid].IsEmpty())
            {
                rooms.RemoveAt(roomid);
            }
            roomid = -1;
        }

        public void Move(int roomid, string username, int userX, int userY)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                if (rooms[roomid].GameStarted && !rooms[roomid].GameOver)
                    rooms[roomid].Move(username, userX, userY);
            }
        }

        public int[] WaitingForOpponent(int roomid, string username)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                return rooms[roomid].WaitingForOpponent(username);
            }

            return new int[] { -1, -1};
        }

        public bool CheckGameOver(int roomIndex, string username, out bool win)
        {
            if (roomIndex >= 0 && roomIndex < rooms.Count)
            {
                return rooms[roomIndex].CheckGameOver(username, out win);
            }

            win = false;
            return false;
        }
    }
}
