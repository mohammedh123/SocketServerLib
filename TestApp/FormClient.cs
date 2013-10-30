using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyncClientServerLib.Client;
using System.Net;
using AsyncClientServerLib.Message;

namespace TestApp
{
    public partial class FormClient : Form
    {
        private BasicSocketClient client = null;
        private Guid clientGuid = Guid.Empty;

        public FormClient()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.clientGuid = Guid.NewGuid();
                this.client = new BasicSocketClient();
                this.client.ReceiveMessageEvent += new SocketServerLib.SocketHandler.ReceiveMessageDelegate(client_ReceiveMessageEvent);
                this.client.ConnectionEvent += new SocketServerLib.SocketHandler.SocketConnectionDelegate(client_ConnectionEvent);
                this.client.CloseConnectionEvent += new SocketServerLib.SocketHandler.SocketConnectionDelegate(client_CloseConnectionEvent);
                this.client.Connect(new IPEndPoint(IPAddress.Loopback, 8100));
                this.buttonConnect.Enabled = false;
                this.buttonDisconnect.Enabled = true;
                this.buttonSend.Enabled = true;
                this.buttonSendLog.Enabled = true;
                this.buttonSendLong2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Client failed to connect remote server.\n{0}", ex.Message), "Socket Client", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void client_CloseConnectionEvent(SocketServerLib.SocketHandler.AbstractTcpSocketClientHandler handler)
        {
            MessageBox.Show(string.Format("Client disconnected from remote server."), "Socket Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void client_ConnectionEvent(SocketServerLib.SocketHandler.AbstractTcpSocketClientHandler handler)
        {
            MessageBox.Show(string.Format("Client connected to remote server."), "Socket Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void client_ReceiveMessageEvent(SocketServerLib.SocketHandler.AbstractTcpSocketClientHandler handler, SocketServerLib.Message.AbstractMessage message)
        {
            BasicMessage receivedMessage = (BasicMessage)message;
            byte[] buffer = receivedMessage.GetBuffer();
            string s = System.Text.ASCIIEncoding.Unicode.GetString(buffer);
            this.SetReceivedText(s);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.client.Close();
            this.client.Dispose();
            this.client = null;
            this.buttonConnect.Enabled = true;
            this.buttonDisconnect.Enabled = false;
            this.buttonSend.Enabled = false;
            this.buttonSendLog.Enabled = false;
            this.buttonSendLong2.Enabled = false;
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
            string s = this.textBoxSend.Text;
            byte[] buffer = System.Text.ASCIIEncoding.Unicode.GetBytes(s);
            BasicMessage message = new BasicMessage(buffer);
            this.client.SendAsync(message);
        }

        private void buttonSendLog_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[350000];
            BasicMessage message = new BasicMessage(buffer);
            this.client.SendAsync(message);
        }

        private void buttonSendLong2_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[350000];
            BasicMessage message = new BasicMessage(buffer);
            this.client.SendAsync(message);
            string s = this.textBoxSend.Text;
            buffer = System.Text.ASCIIEncoding.Unicode.GetBytes(s);
            message = new BasicMessage(buffer);
            this.client.SendAsync(message);
        }
    }
}
