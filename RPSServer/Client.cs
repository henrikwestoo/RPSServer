using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RPSServer
{
    public class Client
    {
        public TcpClient TcpClient { get; set; }
        public NetworkStream NetworkStream { get; set; }

        public Client(TcpClient tcpClient) {

            this.TcpClient = tcpClient;
            NetworkStream = tcpClient.GetStream();


        }

        public void SendMessage(string message)
        {
            byte[] byteTime = Encoding.ASCII.GetBytes(message);
            NetworkStream.Write(byteTime, 0, byteTime.Length);

        }

    }
}
