using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyncClientServerLib.Server;
using System.Net;
using AsyncClientServerLib.Message;
using SocketServerLib.SocketHandler;
using SocketServerLib.Server;

namespace TestApp
{
    delegate void SetTextCallback(string text);
    
    public partial class FormServer : Form
    {
        private BasicSocketServer server = null;
        private Guid serverGuid = Guid.Empty;

        public FormServer()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.server != null)
            {
                this.server.Dispose();
            }
            base.OnClosed(e);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.serverGuid = Guid.NewGuid();
            this.server = new BasicSocketServer();
            this.server.ReceiveMessageEvent += new SocketServerLib.SocketHandler.ReceiveMessageDelegate(server_ReceiveMessageEvent);
            this.server.ConnectionEvent += new SocketConnectionDelegate(server_ConnectionEvent);
            this.server.CloseConnectionEvent += new SocketConnectionDelegate(server_CloseConnectionEvent);
            this.server.Init(new IPEndPoint(IPAddress.Loopback, 8100));
            this.server.StartUp();
            this.buttonStart.Enabled = false;
            this.buttonStop.Enabled = true;
            this.buttonSend.Enabled = true;
            MessageBox.Show("Server Started");
        }

        void server_CloseConnectionEvent(AbstractTcpSocketClientHandler handler)
        {
            MessageBox.Show(string.Format("A client is disconnected from the server"), "Socket Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void server_ConnectionEvent(AbstractTcpSocketClientHandler handler)
        {
            MessageBox.Show(string.Format("A client is connected to the server"), "Socket Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void server_ReceiveMessageEvent(SocketServerLib.SocketHandler.AbstractTcpSocketClientHandler handler, SocketServerLib.Message.AbstractMessage message)
        {
            BasicMessage receivedMessage = (BasicMessage)message;
            byte[] buffer = receivedMessage.GetBuffer();
            if (buffer.Length > 1000)
            {
                MessageBox.Show(string.Format("Received a long message of {0} bytes", receivedMessage.MessageLength), "Socket Server", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string s = System.Text.ASCIIEncoding.Unicode.GetString(buffer);
            this.SetReceivedText(s);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.server.Shutdown();
            this.server.Dispose();
            this.server = null;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
            this.buttonStop.Enabled = false;
            MessageBox.Show("Server Stopped");
        }

        private void SetReceivedText(string text)
        {
            if (this.textBoxReceived.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetReceivedText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBoxReceived.Text = text;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            ClientInfo[] clientList = this.server.GetClientList();
            if (clientList.Length == 0)
            {
                MessageBox.Show("The client is not connected", "Socket Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AbstractTcpSocketClientHandler clientHandler = clientList[0].TcpSocketClientHandler;
            string s = this.textBoxSend.Text;
            byte[] buffer = System.Text.ASCIIEncoding.Unicode.GetBytes(s);
            BasicMessage message = new BasicMessage(buffer);
            clientHandler.SendAsync(message);
        }
    }
}
