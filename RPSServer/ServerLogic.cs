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
        public List<Match> Matches;

        public ServerLogic(Form1 gui)
        {

            this.gui = gui;
            gui.AppendText("hej");
            Clients = new List<Client>();
            Matches = new List<Match>();

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

            var listener = new TcpListener(ipAddr, 4004);
            listener.Start();

            int clientAlias = 1;

            int matchId = 1;
            Match match = new Match(matchId);
            Matches.Add(match);

            while (true) {

                gui.Invoke((MethodInvoker)delegate { gui.AppendText("Listening..."); });
                TcpClient tcpClient = listener.AcceptTcpClient();
                gui.Invoke((MethodInvoker)delegate { gui.AppendText("Connection found!"); });
                
                
                Client client = new Client(tcpClient, clientAlias);
                clientAlias++;
                Clients.Add(client);
                gui.Invoke((MethodInvoker)delegate { gui.AppendText("There are "+Clients.Count()+" clients connected"); });

                //om matchen inte är full försöker vi fylla den med spelare
                if (!match.IsFull()) { match.AddPlayer(client); }

                //annars skapar vi en ny match
                else { matchId++; match = new Match(matchId); 
                    Matches.Add(match);
                    //och lägger till spelaren i den
                    match.AddPlayer(client); }

                MessageAllClients("A new match was created");

            }

        }

        public void MessageAllClients(string message) {

            foreach (var client in Clients)
            {
                client.SendMessageToClient(message);
            }
        
        }

    }
}
