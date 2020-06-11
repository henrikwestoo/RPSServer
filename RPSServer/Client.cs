using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPSServer
{
    public class Client
    {
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetworkStream { get; set; }
        public int Alias { get; set; }

        public Client(TcpClient tcpClient, int alias) {

            Alias = alias;
            TcpClient = tcpClient;
            NetworkStream = tcpClient.GetStream();

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
        
        }

        }
}
