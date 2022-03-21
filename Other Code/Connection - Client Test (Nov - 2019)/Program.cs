using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int cursorStart = Console.CursorTop;
            TcpClient client = null;

            Start();
            
            void Start()
            {
                string ipAddressString = String.Empty;
                int port = 0;

                PrintIPAddress(out ipAddressString);
                PrintIPPort(out port);

                client = new TcpClient(ipAddressString, port);

                Display();
            }

            void PrintIPAddress(out string ipAddressString)
            {
                Console.Write("IP Address: ");
                ipAddressString = Console.ReadLine();

                IPAddress ipAddress = null;
                if (!IPAddress.TryParse(ipAddressString, out ipAddress))
                {
                    Console.CursorTop -= 1;
                    ClearCurrentConsoleLine();
                    PrintIPAddress(out ipAddressString);
                    return;
                }
            }

            void PrintIPPort(out int port)
            {
                Console.Write("IP Port: ");
                string line = Console.ReadLine();

                if (!int.TryParse(line, out port))
                {
                    Console.CursorTop -= 1;
                    ClearCurrentConsoleLine();
                    PrintIPPort(out port);
                    return;
                }
            }

            void Display()
            {
                Console.Write("Message: ");

                string line = Console.ReadLine();
                if (line.Length <= 0)
                {
                    Console.CursorTop -= 1;
                    ClearCurrentConsoleLine();
                    Display();
                    return;
                }

                NetworkStream stream = client.GetStream();
                byte[] byteMessage = System.Text.Encoding.UTF8.GetBytes(line);
                stream.Write(byteMessage, 0, byteMessage.Length);
                stream.Flush();

                Console.Write(Environment.NewLine);

                Display();
            }

            void ClearConsole()
            {
                do
                {
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                } while (Console.CursorTop > cursorStart);

                ClearCurrentConsoleLine();

                Display(); ;
            }

            void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
    }
}
