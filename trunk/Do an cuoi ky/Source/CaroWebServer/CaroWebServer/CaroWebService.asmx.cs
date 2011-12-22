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
        public bool CreateRoom(string username, int userid, bool playwithmachine)
        {
            Room room = new Room();
            if (room.AddPlayer(new Player() { Name = username, Id = userid }))
            {
                if (playwithmachine)
                {
                    if (!room.IsFull())
                    {
                        Player machine = new Machine();
                        room.AddPlayer(machine);
                        rooms.Add(room);
                        return true;
                    }
                }
                else
                {
                    rooms.Add(room);
                    return true;
                }
            }

            return false;
        }

        [WebMethod]
        public bool JoinRoom(int roomid, string username, int userid)
        {
            if (roomid >= 0 && roomid < rooms.Count)
            {
                rooms[roomid].AddPlayer(new Player() { Name = username, Id = userid });

                return true;
            }

            return false;
        }
    }
}
