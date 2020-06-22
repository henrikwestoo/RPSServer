using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPSServer
{
    public enum Move
    {
        None,
        Rock,
        Paper,
        Scissors
    }


    public class Client
    {
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetworkStream { get; set; }
        public int Alias { get; set; }
        public Move CurrentMove { get; set; }

        public int Points { get; set; }

        public Match CurrentMatch { get; set; }



        public Client(TcpClient tcpClient, int alias)
        {

            Alias = alias;
            TcpClient = tcpClient;
            NetworkStream = tcpClient.GetStream();
            ListenToMessages();

        }

        public void SendMessageToClient(string message)
        {
            byte[] byteTime = Encoding.ASCII.GetBytes(message);
            NetworkStream.Write(byteTime, 0, byteTime.Length);

        }

        public void ListenToMessages()
        {
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        public void Run()
        {

            //lyssna efter meddelanden
            while (true)
            {

                byte[] bytes = new byte[1024];
                int bytesRead = NetworkStream.Read(bytes, 0, bytes.Length);

                string message = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                switch (message)
                {

                    //rock
                    case "1":
                        CurrentMove = (Move)1;
                        break;

                    //paper
                    case "2":
                        CurrentMove = (Move)2;
                        break;

                    //scissors
                    case "3":
                        CurrentMove = (Move)3;
                        break;

                }

                CurrentMatch.NotifyPlayMade();

            }

        }

    }
}
