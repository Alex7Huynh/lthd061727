using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroWebServer
{
    public class Room
    {
        public static const int MAX_PLAYER = 2;
        protected List<Player> players = new List<Player>();

        public int GetNumberPlayer()
        {
            return players.Count;
        }

        public bool AddPlayer(Player player)
        {
            if (!IsFull())
            {
                players.Add(player);

                if (player is Machine)
                {
                    HasMachine = true;
                }

                if (IsFull())
                    GameStarted = true;
                return true;
            }
            return false;
        }

        public bool IsFull()
        {
            if (players.Count >= MAX_PLAYER)
                return true;
            return false;
        }

        public bool GameStarted
        {
            get;
            set;
        }

        public bool GameOver
        {
            get;
            set;
        }

        public bool HasMachine
        {
            get;
            set;
        }
    }
}