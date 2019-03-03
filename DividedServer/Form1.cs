using ServerLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DividedServer
{
    public partial class Form1 : Form
    {
        Server _server;
        delegate void StringArgReturningVoidDelegate(string text, Control control);
        public Form1()
        {
            InitializeComponent();
            _server = new Server(new ServerConfig() { Port = 5001 });
            _server.OnServerConnectionEvent += _server_OnServerConnectionEvent;
            _server.OnServerDataRecieved += _server_OnServerConnectionEvent;
            _server.OnServerMessageEvent += _server_OnServerConnectionEvent;
        }

        private void _server_OnServerConnectionEvent(ServerEventArgs e)
        {
            SetText(e.Message + Environment.NewLine, richTextBox1);
        }
        /// <summary>
        /// there is a better way to do this I just have to remember how
        /// </summary>
        /// <param name="text"></param>
        /// <param name="control"></param>
        private void SetText(string text, Control control)
        {
            if (control.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                Invoke(d, new object[] { text, control });
            }
            else
            {
                control.Text += text;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            _server.Start();
        }
    }
}
