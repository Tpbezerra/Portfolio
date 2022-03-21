using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ChatBox
{
    public partial class Form1 : Form
    {
        bool serverStarted;
        bool connectedToClient;

        string previousServerIPLine;
        string previousClientIPLine;
        string previousServerPortLine;
        string previousClientPortLine;
        string previousNameBoxLine;

        TcpListener server;
        TcpClient inClient;
        TcpClient outClient;

        public Form1()
        {
            InitializeComponent();

            serverStarted = false;
            connectedToClient = false;
            previousServerIPLine = ServerIPBox.Text;
            previousServerPortLine = ServerPortBox.Text;
            previousClientIPLine = ClientIPBox.Text;
            previousClientPortLine = ClientPortBox.Text;
        }

        private void StartServerButton_Click(object sender, EventArgs e)
        {
            if (serverStarted)
            {
                server.Stop();
                server = null;
                inClient = null;
                ServerTimer.Enabled = false;

                StartServerButton.Text = "Start Server";
                StatusBox.AppendText("Server stopped" + Environment.NewLine);
            }
            else
            {
                IPAddress address = IPAddress.Parse(ServerIPBox.Text);
                int port = Int32.Parse(ServerPortBox.Text);
                server = new TcpListener(address, port);
                server.Start();
                ServerTimer.Enabled = true;

                StartServerButton.Text = "Stop Server";
                StatusBox.AppendText("Server listening..." + Environment.NewLine);
            }

            serverStarted = !serverStarted;
        }

        private void ServerIPBox_TextChanged(object sender, EventArgs e)
        {
            if (serverStarted)
            {
                ServerIPBox.Text = previousServerIPLine;
                return;
            }

            previousServerIPLine = ServerIPBox.Text;
        }

        private void ServerPortBox_TextChanged(object sender, EventArgs e)
        {
            if (serverStarted)
            {
                ServerPortBox.Text = previousServerPortLine;
                return;
            }

            if (ServerPortBox.Text != previousServerPortLine)
            {
                int portTest = 0;
                if (!int.TryParse(ServerPortBox.Text, out portTest) && ServerPortBox.Text.Length > 0)
                    ServerPortBox.Text = previousServerPortLine;
            }

            previousServerPortLine = ServerPortBox.Text;
        }

        private void ClientIPBox_TextChanged(object sender, EventArgs e)
        {
            if (connectedToClient)
            {
                ClientIPBox.Text = previousClientIPLine;
                return;
            }

            previousClientIPLine = ClientIPBox.Text;
        }

        private void ClientPortBox_TextChanged(object sender, EventArgs e)
        {
            if (connectedToClient)
            {
                ClientPortBox.Text = previousClientPortLine;
                return;
            }

            if (ClientPortBox.Text != previousClientPortLine)
            {
                int portTest = 0;
                if (!int.TryParse(ClientPortBox.Text, out portTest) && ClientPortBox.Text.Length > 0)
                    ClientPortBox.Text = previousClientPortLine;
            }

            previousClientPortLine = ClientPortBox.Text;
        }

        private void ServerTimer_Tick(object sender, EventArgs e)
        {
            if (server.Pending())
            {
                inClient = server.AcceptTcpClient();
            }

            if (inClient != null)
            {
                NetworkStream stream = inClient.GetStream();
                if (stream.DataAvailable)
                {
                    byte[] data = new byte[4096];
                    int numBytes = stream.Read(data, 0, data.Length);
                    string message = System.Text.Encoding.UTF8.GetString(data, 0, numBytes);
                    ConversationBox.AppendText(message + Environment.NewLine);
                }
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (connectedToClient)
            {
                outClient = null;
                StatusBox.AppendText("Disconnected from client" + Environment.NewLine);

                ConnectButton.Text = "Connect";

                connectedToClient = false;
            }
            else
            {
                try
                {
                    int port = Int32.Parse(ClientPortBox.Text);
                    outClient = new TcpClient(ClientIPBox.Text, port);
                    StatusBox.AppendText("Connected to " + ClientIPBox.Text + Environment.NewLine);
                    previousNameBoxLine = NameBox.Text;

                    ConnectButton.Text = "Disconnect";

                    connectedToClient = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Occurred" + Environment.NewLine + ex.ToString(), "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (outClient != null)
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(NameBox.Text + ": " + WriteBox.Text);
                NetworkStream stream = outClient.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
                ConversationBox.AppendText(WriteBox.Text + Environment.NewLine);

                WriteBox.Text = "";
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            if (connectedToClient)
                NameBox.Text = previousNameBoxLine;
        }
    }
}
