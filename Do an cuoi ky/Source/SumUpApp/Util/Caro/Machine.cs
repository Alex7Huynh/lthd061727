using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumUpApp
{
    public class Machine: Player
    {
        #region Attributes
        int[] iMax;
        int[] jMax;
        int nMax;
        #endregion

        #region Methods
        public Machine(ref CaroBoard board)
            : base(ref board)
        {
            Name = "Machine";
            Id = new Guid();
            Sq = -1;

            iMax = new int[caroBoard.BoardSize * caroBoard.BoardSize];
            jMax = new int[caroBoard.BoardSize * caroBoard.BoardSize];
            nMax = 0;
        }

        public override void Move()
        {
            GetBestMachMove();

            base.Move();
        }

        private void GetBestMachMove()
        {
            int maxS = caroBoard.EvaluatePosS();
            int maxQ = caroBoard.EvaluatePosQ();

            // alert ('maxS='+maxS+', maxQ='+maxQ);

            if (maxQ >= maxS)
            {
                maxS = -1;
                for (int i = 0; i < caroBoard.BoardSize; i++)
                {
                    for (int j = 0; j < caroBoard.BoardSize; j++)
                    {
                        if (caroBoard.Q[i, j] == maxQ)
                        {
                            if (caroBoard.S[i, j] > maxS)
                            {
                                maxS = caroBoard.S[i, j]; nMax = 0;
                            }
                            if (caroBoard.S[i, j] == maxS)
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
                for (int i = 0; i < caroBoard.BoardSize; i++)
                {
                    for (int j = 0; j < caroBoard.BoardSize; j++)
                    {
                        if (caroBoard.S[i, j] == maxS)
                        {
                            if (caroBoard.Q[i, j] > maxQ) { maxQ = caroBoard.Q[i, j]; nMax = 0; }
                            if (caroBoard.Q[i, j] == maxQ) { iMax[nMax] = i; jMax[nMax] = j; nMax++; }
                        }
                    }
                }
            }
            // alert('nMax='+nMax+'\niMax: '+iMax+'\njMax: '+jMax)
            Random RandomNumber = new Random();
            double x = RandomNumber.NextDouble();
            int randomK = Convert.ToInt32(Math.Floor(nMax * x));
            X = iMax[randomK];
            Y = jMax[randomK];
        }
        #endregion
    }
}