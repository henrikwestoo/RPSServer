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
        public static List<Client> Clients;
        public static List<Match> Matches;

        public ServerLogic(Form1 gui)
        {

            this.gui = gui;
            Clients = new List<Client>();
            Matches = new List<Match>();

        }

        public void StartServer()
        {
            Thread newThread = new Thread(new ThreadStart(Run));
            newThread.Start();
        }

        public static void RemoveMatchFromList(Match match) {

            Matches.Remove(match);
            Clients.Remove(match.Player1);
            Clients.Remove(match.Player2);
        
        }

        public void Run()
        {

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];

            //som en serversocket
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
                if (!match.IsFull()) 
                { 
                    match.AddPlayer(client);

                    //om matchen nu är full så kan matchen börja
                    if (match.IsFull()) {

                        match.Player1.SendMessageToClient("matchfound");
                        match.Player2.SendMessageToClient("matchfound");
                    
                    }
                }

                //annars skapar vi en ny match
                else { matchId++; match = new Match(matchId); 
                    Matches.Add(match);
                    //och lägger till spelaren i den
                    match.AddPlayer(client); }

            }

        }

        public void MessageAllClients(string message) {

            foreach (var client in Clients)
            {
                client.SendMessageToClient(message);
            }
            //gui.Invoke((MethodInvoker)delegate { gui.AppendText("sent:" + message + " to clients"); });
            
        
        }

    }
}
