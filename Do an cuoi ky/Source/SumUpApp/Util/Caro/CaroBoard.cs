using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumUpApp
{
    public class CaroBoard
    {
        #region Attributes
        int boardSize;
        int nmbRows;
        int nmbColumns;

        public const int USERSQ = 1;
        public const int MACHSQ = -1;

        int winningMove = 9999999;
        int openFour = 8888888;
        int twoThrees = 7777777;

        int[,] f;
        int[,] s;
        int[,] q;

        bool drawPos = false;
        int[] w = new int[] { 0, 20, 17, 15, 14, 10 };
        int[] nPos;
        int[] dirA;
        #endregion

        #region Properties
        public int BoardSize
        {
            get { return boardSize; }
            set { boardSize = value; }
        }

        public int[,] F
        {
            get { return f; }
            set { f = value; }
        }

        public int[,] S
        {
            get { return s; }
            set { s = value; }
        }

        public int[,] Q
        {
            get { return q; }
            set { q = value; }
        }

        public bool DrawPos
        {
            get { return drawPos; }
            set { drawPos = value; }
        }
        #endregion

        #region Methods
        public CaroBoard()
        {
            boardSize = 20;
            nmbRows = boardSize;
            nmbColumns = boardSize;

            f = new int[boardSize, boardSize];
            s = new int[boardSize, boardSize];
            q = new int[boardSize, boardSize];

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
        }


        public int EvaluatePosS()
        {
            return EvaluatePos(ref s, USERSQ);
        }


        public int EvaluatePosQ()
        {
            return EvaluatePos(ref q, MACHSQ);
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

        public bool WinningMove(int i, int j, int mySq)
        {
            if (WinningPos(i, j, mySq) == winningMove)
                return true;
            return false;
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
    }
}