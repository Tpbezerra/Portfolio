using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("10.156.0.166");
            int port = 80;

            TcpListener TCPServer = new TcpListener(address, port);
            TCPServer.Start();

            TcpClient client = null;

            while (client == null)
            {
                if (TCPServer.Pending())
                {
                    client = TCPServer.AcceptTcpClient();
                }
            }

            while (client != null)
            {
                NetworkStream stream = client.GetStream();

                byte[] bytes = new byte[4096];
                int numBytes = stream.Read(bytes, 0, bytes.Length);

                string data = System.Text.Encoding.UTF8.GetString(bytes, 0, numBytes);

                if (data.Length > 0)
                    Console.WriteLine(data);
            }
        }
    }
}
