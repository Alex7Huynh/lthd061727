using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace CaroSocialNetwork
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

        #region Caro Board
        int boardSize;
        int nmbRows;
        int nmbColumns;

        int userSq;
        int machSq;

        int winningMove = 9999999;
        int openFour = 8888888;
        int twoThrees = 7777777;

        int[,] f;
        int[,] s;
        int[,] q;

        int currentTurn;
        int lastTurn;
        int iLastMove;
        int jLastMove;

        int[] iMax;
        int[] jMax;
        int nMax = 0;

        bool drawPos = false;
        int[] w = new int[] { 0, 20, 17, 15, 14, 10 };
        int[] nPos;
        int[] dirA;
        int iMach;
        int jMach;
        #endregion

        #region Room Attribute
        public const int MAX_PLAYER = 2;
        protected List<Player> players = new List<Player>();

        public bool GameOver
        {
            get;
            set;
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
        #endregion

        public Room()
        {
            boardSize = 20;
            nmbRows = boardSize;
            nmbColumns = boardSize;

            userSq = 1;
            machSq = -1;

            f = new int[boardSize, boardSize];
            s = new int[boardSize, boardSize];
            q = new int[boardSize, boardSize];

            iMax = new int[boardSize * boardSize];
            jMax = new int[boardSize * boardSize];


            nPos = new int[4];
            dirA = new int[4];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    f[i, j] = 0;
                    s[i, j] = 0;
                    q[i, j] = 0;
                }
            }

            currentTurn = -1;
            lastTurn = -1;
            iLastMove = -1;
            jLastMove = -1;

            GameOver = true;
            Name = "";
            Id = -1;
        }

        public int GetNumberPlayer()
        {
            return players.Count;
        }

        public bool AddPlayer(Player player)
        {
            if (!IsFull())
            {
                players.Add(player);
                UpdateRoomName();

                if (IsFull())
                {
                    GameOver = false;
                    currentTurn = 0;
                }
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

        protected int FindPlayer(string username)
        {
            foreach (Player p in players)
            {
                if (!(p is Machine) && p.Name == username)
                {
                    return players.IndexOf(p);
                }
            }

            return -1;
        }

        internal void Move(string username, int userX, int userY)
        {
            if (GameOver)
                return;
            int id = FindPlayer(username);
            if (id != -1 && currentTurn == id)
            {
                f[userX, userY] = id+1;

                iLastMove = userX;
                jLastMove = userY;

                NextTurn();
            }
        }

        private void NextTurn()
        {
            if (GameOver)
                return;

            lastTurn = currentTurn;
            currentTurn++;
            if (currentTurn >= players.Count)
                currentTurn = 0;

            CheckGameOver();

            if (players[currentTurn] is Machine)
            {
                MachineMove();
            }
        }

        private void CheckGameOver()
        {
            if (PlayWithMachine)
            {
                bool over;
                if (players[lastTurn] is Machine)
                {
                    over = (WinningPos(iLastMove, jLastMove, machSq) == winningMove);
                }
                else
                {
                    over = (WinningPos(iLastMove, jLastMove, userSq) == winningMove);
                }
                GameOver = over;
            }
            else
            {
                if (WinningPos(iLastMove, jLastMove, lastTurn+1) == winningMove)
                {
                    GameOver = true;
                }
                else if (drawPos)
                {
                    GameOver = true;
                }
            }
        }

        internal void RemovePlayer(string username)
        {
            int index = FindPlayer(username);
            players.RemoveAt(index);
            GameOver = true;
            currentTurn = -1;

            UpdateRoomName();
        }

        private void UpdateRoomName()
        {
            Name = "";
            foreach (Player player in players)
            {
                Name += player.Name;
            }
        }

        internal bool IsEmpty()
        {
            if (players.Count == 0 | (players.Count == 1 && players[0] is Machine))
                return true;
            return false;
        }
        #region Machine

        private void MachineMove()
        {
            if (GameOver)
                return;
            GetBestMachMove();
            f[iMach, jMach] = machSq;

            iLastMove = iMach;
            jLastMove = jMach;

            NextTurn();
        }

        private void GetBestMachMove()
        {
            int maxS = EvaluatePos(ref s, userSq);
            int maxQ = EvaluatePos(ref q, machSq);

            // alert ('maxS='+maxS+', maxQ='+maxQ);

            if (maxQ >= maxS)
            {
                maxS = -1;
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (q[i, j] == maxQ)
                        {
                            if (s[i, j] > maxS)
                            {
                                maxS = s[i, j]; nMax = 0;
                            }
                            if (s[i, j] == maxS)
                            {
                                iMax[nMax] = i;
                                jMax[nMax] = j;
                                nMax++;
                            }
                        }
                    }
                }
            }
            else
            {
                maxQ = -1;
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (s[i, j] == maxS)
                        {
                            if (q[i, j] > maxQ) { maxQ = q[i, j]; nMax = 0; }
                            if (q[i, j] == maxQ) { iMax[nMax] = i; jMax[nMax] = j; nMax++; }
                        }
                    }
                }
            }
            // alert('nMax='+nMax+'\niMax: '+iMax+'\njMax: '+jMax)
            Random RandomNumber = new Random();
            double x = RandomNumber.NextDouble();
            int randomK = Convert.ToInt32(Math.Floor(nMax * x));
            iMach = iMax[randomK];
            jMach = jMax[randomK];
        }

        private int EvaluatePos(ref int[,] a, int mySq)
        {
            int maxA = -1;
            drawPos = true;
            int m;
            int A1, A2, A3, A4;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {

                    // Compute "value" a[i][j] of the (i,j) move

                    if (f[i, j] != 0) { a[i, j] = -1; continue; }
                    if (HasNeighbors(i, j) == 0) { a[i, j] = -1; continue; }

                    int wp = WinningPos(i, j, mySq);
                    if (wp > 0) a[i, j] = wp;
                    else
                    {
                        int minM = i - 4; if (minM < 0) minM = 0;
                        int minN = j - 4; if (minN < 0) minN = 0;
                        int maxM = i + 5; if (maxM > boardSize) maxM = boardSize;
                        int maxN = j + 5; if (maxN > boardSize) maxN = boardSize;

                        nPos[0] = 1; A1 = 0;
                        m = 1; while (j + m < maxN && f[i, j + m] != -mySq) { nPos[0]++; A1 += w[m] * f[i, j + m]; m++; }
                        if (j + m >= boardSize || f[i, j + m] == -mySq) A1 -= (f[i, j + m - 1] == mySq) ? (w[5] * mySq) : 0;
                        m = 1; while (j - m >= minN && f[i, j - m] != -mySq) { nPos[0]++; A1 += w[m] * f[i, j - m]; m++; }
                        if (j - m < 0 || f[i, j - m] == -mySq) A1 -= (f[i, j - m + 1] == mySq) ? (w[5] * mySq) : 0;
                        if (nPos[0] > 4) drawPos = false;

                        nPos[1] = 1; A2 = 0;
                        m = 1; while (i + m < maxM && f[i + m, j] != -mySq) { nPos[1]++; A2 += w[m] * f[i + m, j]; m++; }
                        if (i + m >= boardSize || f[i + m, j] == -mySq) A2 -= (f[i + m - 1, j] == mySq) ? (w[5] * mySq) : 0;
                        m = 1; while (i - m >= minM && f[i - m, j] != -mySq) { nPos[1]++; A2 += w[m] * f[i - m, j]; m++; }
                        if (i - m < 0 || f[i - m, j] == -mySq) A2 -= (f[i - m + 1, j] == mySq) ? (w[5] * mySq) : 0;
                        if (nPos[1] > 4) drawPos = false;

                        nPos[2] = 1; A3 = 0;
                        m = 1; while (i + m < maxM && j + m < maxN && f[i + m, j + m] != -mySq) { nPos[2]++; A3 += w[m] * f[i + m, j + m]; m++; }
                        if (i + m >= boardSize || j + m >= boardSize || f[i + m, j + m] == -mySq) A3 -= (f[i + m - 1, j + m - 1] == mySq) ? (w[5] * mySq) : 0;
                        m = 1; while (i - m >= minM && j - m >= minN && f[i - m, j - m] != -mySq) { nPos[2]++; A3 += w[m] * f[i - m, j - m]; m++; }
                        if (i - m < 0 || j - m < 0 || f[i - m, j - m] == -mySq) A3 -= (f[i - m + 1, j - m + 1] == mySq) ? (w[5] * mySq) : 0;
                        if (nPos[2] > 4) drawPos = false;

                        nPos[3] = 1; A4 = 0;
                        m = 1; while (i + m < maxM && j - m >= minN && f[i + m, j - m] != -mySq) { nPos[3]++; A4 += w[m] * f[i + m, j - m]; m++; }
                        if (i + m >= boardSize || j - m < 0 || f[i + m, j - m] == -mySq) A4 -= (f[i + m - 1, j - m + 1] == mySq) ? (w[5] * mySq) : 0;
                        m = 1; while (i - m >= minM && j + m < maxN && f[i - m, j + m] != -mySq) { nPos[3]++; A4 += w[m] * f[i - m, j + m]; m++; }
                        if (i - m < 0 || j + m >= boardSize || f[i - m, j + m] == -mySq) A4 -= (f[i - m + 1, j + m - 1] == mySq) ? (w[5] * mySq) : 0;
                        if (nPos[3] > 4) drawPos = false;

                        dirA[0] = (nPos[0] > 4) ? A1 * A1 : 0;
                        dirA[1] = (nPos[1] > 4) ? A2 * A2 : 0;
                        dirA[2] = (nPos[2] > 4) ? A3 * A3 : 0;
                        dirA[3] = (nPos[3] > 4) ? A4 * A4 : 0;

                        A1 = 0; A2 = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            if (dirA[k] >= A1) { A2 = A1; A1 = dirA[k]; }
                        }
                        a[i, j] = A1 + A2;
                    }
                    if (a[i, j] > maxA)
                    {
                        maxA = a[i, j];
                    }
                }
            }
            return maxA;
        }

        private int HasNeighbors(int i, int j)
        {
            if (j > 0 && f[i, j - 1] != 0) return 1;
            if (j + 1 < boardSize && f[i, j + 1] != 0) return 1;
            if (i > 0)
            {
                if (f[i - 1, j] != 0) return 1;
                if (j > 0 && f[i - 1, j - 1] != 0) return 1;
                if (j + 1 < boardSize && f[i - 1, j + 1] != 0) return 1;
            }
            if (i + 1 < boardSize)
            {
                if (f[i + 1, j] != 0) return 1;
                if (j > 0 && f[i + 1, j - 1] != 0) return 1;
                if (j + 1 < boardSize && f[i + 1, j + 1] != 0) return 1;
            }
            return 0;
        }

        private int WinningPos(int i, int j, int mySq)
        {
            int test3 = 0;
            int test4 = 0;
            int L, m, m1, m2;
            bool side1, side2;

            L = 1;
            m = 1; while (j + m < boardSize && f[i, j + m] == mySq) { L++; m++; } m1 = m;
            m = 1; while (j - m >= 0 && f[i, j - m] == mySq) { L++; m++; } m2 = m;
            if (L > 4) { return winningMove; }
            side1 = (j + m1 < boardSize && f[i, j + m1] == 0);
            side2 = (j - m2 >= 0 && f[i, j - m2] == 0);

            if (L == 4 && (side1 || side2)) test3++;
            if (side1 && side2)
            {
                if (L == 4) test4 = 1;
                if (L == 3) test3++;
            }

            L = 1;
            m = 1; while (i + m < boardSize && f[i + m, j] == mySq) { L++; m++; } m1 = m;
            m = 1; while (i - m >= 0 && f[i - m, j] == mySq) { L++; m++; } m2 = m;
            if (L > 4) { return winningMove; }
            side1 = (i + m1 < boardSize && f[i + m1, j] == 0);
            side2 = (i - m2 >= 0 && f[i - m2, j] == 0);
            if (L == 4 && (side1 || side2)) test3++;
            if (side1 && side2)
            {
                if (L == 4) test4 = 1;
                if (L == 3) test3++;
            }

            L = 1;
            m = 1; while (i + m < boardSize && j + m < boardSize && f[i + m, j + m] == mySq) { L++; m++; } m1 = m;
            m = 1; while (i - m >= 0 && j - m >= 0 && f[i - m, j - m] == mySq) { L++; m++; } m2 = m;
            if (L > 4) { return winningMove; }
            side1 = (i + m1 < boardSize && j + m1 < boardSize && f[i + m1, j + m1] == 0);
            side2 = (i - m2 >= 0 && j - m2 >= 0 && f[i - m2, j - m2] == 0);
            if (L == 4 && (side1 || side2)) test3++;
            if (side1 && side2)
            {
                if (L == 4) test4 = 1;
                if (L == 3) test3++;
            }

            L = 1;
            m = 1; while (i + m < boardSize && j - m >= 0 && f[i + m, j - m] == mySq) { L++; m++; } m1 = m;
            m = 1; while (i - m >= 0 && j + m < boardSize && f[i - m, j + m] == mySq) { L++; m++; } m2 = m;
            if (L > 4) { return winningMove; }
            side1 = (i + m1 < boardSize && j - m1 >= 0 && f[i + m1, j - m1] == 0);
            side2 = (i - m2 >= 0 && j + m2 < boardSize && f[i - m2, j + m2] == 0);
            if (L == 4 && (side1 || side2)) test3++;
            if (side1 && side2)
            {
                if (L == 4) test4 = 1;
                if (L == 3) test3++;
            }

            if (test4 > 0) return openFour;
            if (test3 >= 2) return twoThrees;
            return -1;
        }
        #endregion

        internal string GetCurrentTurn()
        {
            if (currentTurn == -1)
                return "";
            else
            {
                return players[currentTurn].Name;
            }
        }

        internal int[] GetLastMove()
        {
            return new int[] { iLastMove, jLastMove};
        }

        internal bool IsGameOver()
        {
            return GameOver;
        }

        internal bool IsWin(string username)
        {
            if (FindPlayer(username) == lastTurn)
                return true;
            return false;
        }
    }
}