using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CaroWebServer
{
    /// <summary>
    /// Summary description for CaroWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CaroWebService : System.Web.Services.WebService
    {
        List<Room> rooms = new List<Room>();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool CreateRoom(string username, bool playwithmachine, out int roomIndex)
        {
            Room room = new Room();
            Player player = new Player() { Name = username };
            if (room.AddPlayer(player))
            {
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

        [WebMethod]
        public bool JoinRoom(int roomid, string username)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                if (rooms[roomid].AddPlayer(new Player() { Name = username}))
                    return true;
            }

            return false;
        }

        [WebMethod]
        public void Move(int roomid, string username, int userX, int userY)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                if (rooms[roomid].GameStarted && !rooms[roomid].GameOver)
                    rooms[roomid].Move(username, userX, userY);
            }
        }

        [WebMethod]
        public int[] WaitingForOpponent(int roomid, string username)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                return rooms[roomid].WaitingForOpponent(username);
            }

            return new int[] { -1, -1};
        }

        [WebMethod]
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
