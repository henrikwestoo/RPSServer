using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPSServer
{
    public class ServerLogic
    {

        public Form1 gui;
        public List<Client> Clients;

        public ServerLogic(Form1 gui)
        {

            this.gui = gui;
            gui.AppendText("hej");
            Clients = new List<Client>();

        }

        public void StartServer()
        {
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        public void Run()
        {

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];

            bool done = false;
            var listener = new TcpListener(ipAddr, 4004);
            listener.Start();

            int counter = 0;

            while (!done) {

                counter++;

                gui.Invoke((MethodInvoker)delegate { gui.AppendText("Listening..."); });
                TcpClient tcpClient = listener.AcceptTcpClient();
                gui.Invoke((MethodInvoker)delegate { gui.AppendText("Connection found!"); });
                
                
                Client client = new Client(tcpClient);
                Clients.Add(client);
                gui.Invoke((MethodInvoker)delegate { gui.AppendText("There are "+counter+" clients connected"); });

                MessageAllClients("this is a message from the server");

            }

            try
            {}
            catch (Exception e) {}

        }

        public void MessageAllClients(string message) {

            foreach (var client in Clients)
            {
                client.SendMessage(message);
            }
        
        }

    }
}
