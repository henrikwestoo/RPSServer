using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPSServer
{
    public partial class Form1 : Form
    {

        public ServerLogic serverLogic;
        public int counter { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        public void AppendText(string Text)
        {

            textBox1.AppendText(Environment.NewLine + Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverLogic.StartServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            serverLogic.MessageAllClients("test-message from server " + counter);
            counter++;
            serverLogic.MessageAllClients("\ntest-message from server " + counter);
            counter++;
            serverLogic.MessageAllClients("\ntest-message from server " + counter);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serverLogic.Matches != null)
            {
                foreach (var match in serverLogic.Matches)
                {
                    AppendText("MatchID: " + match.MatchId + ":");

                    if (match.Player1 != null)
                    { AppendText("Player1: " + match.Player1.Alias + ". Move: " + match.Player1.CurrentMove.ToString()+". Points: " + match.Player1.Points); }

                    else { AppendText("Player1: " + "Not connected"); }

                    if (match.Player2 != null)
                    { AppendText("Player2: " + match.Player2.Alias + ". Move: " + match.Player2.CurrentMove.ToString() + ". Points: " + match.Player2.Points); }

                    else { AppendText("Player2: " + "Not connected"); }
                }
            }
        }
    }
}
