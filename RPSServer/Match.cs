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
        public bool Running { get; set; }

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
                player.CurrentMatch = this;
            }

            else if (Player2 == null)
            { Player2 = player;
                player.CurrentMatch = this;
                Running = true;
                Player1.Points = 0;
                Player2.Points = 0;
            }

        }

        public void NotifyPlayMade() {

            if (Player1.CurrentMove != 0 && Player2.CurrentMove != 0) {

                switch (Player1.CurrentMove)
                {

                    case (Move)1:
                        if (Player2.CurrentMove == (Move)1) { }
                        else if (Player2.CurrentMove == (Move)2) { Player2.Points++; }
                        else if (Player2.CurrentMove == (Move)3) { Player1.Points++; }
                        break;


                    case (Move)2:
                        if (Player2.CurrentMove == (Move)1) { Player1.Points++; }
                        else if (Player2.CurrentMove == (Move)2) { }
                        else if (Player2.CurrentMove == (Move)3) { Player2.Points++; }
                        break;


                    case (Move)3:
                        if (Player2.CurrentMove == (Move)1) { Player2.Points++; }
                        else if (Player2.CurrentMove == (Move)2) { Player1.Points++; }
                        else if(Player2.CurrentMove == (Move)3) { }
                        break;

                }

                if(Player1.Points == 3)
                {
                    Player1.SendMessageToClient("gamewon");
                    Player2.SendMessageToClient("gamelost");
                }

                else if (Player2.Points == 3)
                {
                    Player2.SendMessageToClient("gamewon");
                    Player1.SendMessageToClient("gamelost");
                }

                Player1.CurrentMove = 0;
                Player2.CurrentMove = 0;

            }
            
        }

    }
}
