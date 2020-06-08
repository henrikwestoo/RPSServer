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

        public Form1()
        {
            InitializeComponent();
        }

        public void AppendText(string Text) {

            textBox1.AppendText(Environment.NewLine +Text);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverLogic.StartServer();
        }
    }
}
