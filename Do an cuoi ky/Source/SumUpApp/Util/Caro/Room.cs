using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace SumUpApp
{

    public class Room
    {
        #region Event and Delegate (Not Use)
        /*
        public delegate int[] MoveWaitingEvent(int[] p);
        public event MoveWaitingEvent WaitingComplete;
        
        //Room
        internal void WaitingForOpponent(string username)
        {
            Thread thread = new Thread(o => WaitingThread(username));
            thread.Start();
        }

        public void WaitingThread(string username)
        {
            do
            {
                Thread.Sleep(200);
            }
            while (FindPlayer(username) != currentTurn);

            WaitingComplete(new int[] { iLastMove, jLastMove });
        }
        
        //Room Manager
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
         */
        #endregion

        #region Attributes
        private CaroBoard caroBoard;
        public const int MAX_PLAYER = 2;
        protected List<Player> players = new List<Player>();
        #endregion

        #region Properties
        public bool GameOver
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public bool PlayWithMachine
        {
            get;
            set;
        }

        public int CurrentTurn
        {
            get;
            private set;
        }

        public int LastTurn
        {
            get;
            private set;
        }

        public int LastX
        {
            get;
            private set;
        }

        public int LastY
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public Room()
        {
            caroBoard = new CaroBoard();
            GameOver = true;
            Name = "";
            Id = -1;

            CurrentTurn = -1;
            LastTurn = -1;
            LastX = -1;
            LastY = -1;
        }

        public bool IsFull()
        {
            if (players.Count >= MAX_PLAYER)
                return true;
            return false;
        }

        public bool HasPlayer(Guid id)
        {
            foreach (Player player in players)
            {
                if (player.Id == id)
                    return true;
            }
            return false;
        }

        public void AddUserPlayer(Guid id, string username)
        {
            if (!IsFull())
            {
                UserPlayer player = new UserPlayer(ref caroBoard);
                player.Name = username;
                player.Id = id;
                players.Add(player);

                UpdateRoom();

                if (IsFull())
                {
                    GameOver = false;
                    CurrentTurn = 0;
                }
            }
        }

        public void AddMachine()
        {
            if (!IsFull())
            {
                Machine machine = new Machine(ref caroBoard);
                players.Add(machine);

                UpdateRoom();

                if (IsFull())
                {
                    GameOver = false;
                    CurrentTurn = 0;
                }
            }
        }

        public void RemoveUserPlayer(Guid id)
        {
            foreach (Player player in players)
            {
                if (player.Id == id)
                {
                    players.Remove(player);
                    break;
                }
            }
            GameOver = true;
            CurrentTurn = -1;

            UpdateRoom();
        }

        public void Move(Guid id, int userX, int userY)
        {
            if (GameOver)
                return;
            foreach (Player p in players)
            {
                if (!(p is Machine) && p.Id == id)
                {
                    p.X = userX;
                    p.Y = userY;

                    p.Move();

                    LastX = p.X;
                    LastY = p.Y;

                    NextTurn();
                }
            }
        }

        private void NextTurn()
        {
            if (GameOver)
                return;

            LastTurn = CurrentTurn;
            CurrentTurn++;
            if (CurrentTurn >= players.Count)
                CurrentTurn = 0;

            CheckGameOver();

            if (players[CurrentTurn] is Machine)
            {
                players[CurrentTurn].Move();

                LastX = players[CurrentTurn].X;
                LastY = players[CurrentTurn].Y;

                NextTurn();
            }
        }

        private void CheckGameOver()
        {
            if (PlayWithMachine)
            {
                bool over;
                if (players[LastTurn] is Machine)
                {
                    over = caroBoard.WinningMove(LastX, LastY, CaroBoard.MACHSQ);
                }
                else
                {
                    over = caroBoard.WinningMove(LastX, LastY, CaroBoard.USERSQ);
                }
                GameOver = over;
            }
            else
            {
                if (caroBoard.WinningMove(LastX, LastY, LastTurn + 1))
                {
                    GameOver = true;
                }
                else if (caroBoard.DrawPos)
                {
                    GameOver = true;
                }
            }
        }

        private void UpdateRoom()
        {
            Name = "";
            for (int i = 0; i < players.Count; i++ )
            {
                Name += players[i].Name;

                if (PlayWithMachine)
                {
                    if (players[i] is UserPlayer)
                        players[i].Sq = CaroBoard.USERSQ;
                    else if (players[i] is Machine)
                        players[i].Sq = CaroBoard.MACHSQ;
                }
                else
                {
                    players[i].Sq = i + 1;
                }
            }
        }

        public bool IsEmpty()
        {
            if (players.Count == 0 | (players.Count == 1 && players[0] is Machine))
                return true;
            return false;
        }

        public bool IsMyTurn(Guid userid)
        {
            if (CurrentTurn == -1)
                return false;

            if (players[CurrentTurn].Id == userid)
                return true;

            return false;
        }

        public int[] GetLastMove()
        {
            return new int[] { LastX, LastY};
        }

        internal bool IsGameOver()
        {
            return GameOver;
        }

        internal bool IsWin(Guid userid)
        {
            if (GameOver && players[LastTurn].Id == userid)
                return true;
            return false;
        }
        #endregion
    }
}