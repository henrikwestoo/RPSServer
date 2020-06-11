using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSServer
{
    public class Match
    {

        public Client Player1 { get; set; }
        public Client Player2 { get; set; }
        public int MatchId { get; set; }

        public Match(int matchId)
        {

            this.MatchId = matchId;
        }

        public bool IsFull()
        {

            bool isFull;

            if (Player1 != null && Player2 != null)
            { isFull = true; }

            else { isFull = false; }

            return isFull;

        }

        public void AddPlayer(Client player)
        {

            if (Player1 == null)
            {
                Player1 = player;
            }

            else if (Player2 == null)
            { Player2 = player; }

        }


    }
}
