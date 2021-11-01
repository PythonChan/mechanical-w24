using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace W24
{
    class Connection
    {
        private static Socket mySocket;
        private static Task listener;
        public static string ip = "127.0.0.1";
        public static int port = 7424;

        public static bool IsConnected { get; private set; }
        public static bool IsReallyConnected { get; private set; }

        public static void Connect()
        {
            try
            {
                TD2.receiveCounter = 0;
                mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mySocket.Connect(ip, port);
                listener = null;
                listener = new Task(() => TD2.Listen(mySocket));
                listener.Start();
                IsConnected = true;

                foreach (List<RailObject> list in Program.RailObjectLists)
                {
                    foreach (RailObject railObject in list)
                    {
                        if (railObject.State == null)
                        {
                            Send("GetState");
                        }
                    }
                }

                Console.WriteLine($"Nawiązano połączenie na porcie {port}");
            }
            catch (SocketException)
            {
                mySocket.Close();
                IsConnected = false;
                IsReallyConnected = false;

                Console.WriteLine($"Utracono połączenie na porcie {port}");
            }
        }

        public static void Disconnect()
        {
            mySocket.Close();
            IsConnected = false;
            IsReallyConnected = false;

            Console.WriteLine($"Utracono połączenie na porcie {port}");
        }

        public static void Send(string msg)
        {
            if (IsConnected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(msg + "\r\n");
                try
                {
                    mySocket.Send(bytes, 0, bytes.Length, SocketFlags.None);
                }
                catch (SocketException)
                {
                    Console.WriteLine($"Brak połączenia na porcie {port}");
                }
            }
            else
            {
                TD2.MessageReceived(msg);
            }
        }
    }
}
