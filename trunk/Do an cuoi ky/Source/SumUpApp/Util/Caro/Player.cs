using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SumUpApp
{
    public class Player
    {
        #region Attributes
        public string Name
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public int Sq
        {
            get;
            set;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        protected CaroBoard caroBoard;
        #endregion

        #region Methods
        public Player(ref CaroBoard board)
        {
            caroBoard = board;
        }

        public virtual void Move()
        {
            caroBoard.F[X, Y] = Sq;
        }
        #endregion
    }
}