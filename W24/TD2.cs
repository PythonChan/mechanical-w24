using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace W24
{
    class TD2
    {
        private const int BUFFER_SIZE = 64;
        private static string msg = string.Empty;
        private static int byteCount;
        public static int receiveCounter = 0;

        public static void Listen(Socket s)
        {
            while (true)
            {
                if (!Connection.IsConnected)
                {
                    break;
                }

                try
                {
                    if (receiveCounter < 1000)
                    {
                        ReceiveBuffer(s);
                    }
                    else
                    {
                        Connection.Disconnect();
                    }
                }
                catch (SocketException)
                {
                    break;
                }
                catch (Exception)
                {

                }
            }
        }

        public static void ReceiveBuffer(Socket s)
        {
            byte[] numArray = new byte[64];
            byteCount = s.Receive(numArray);
            msg += Encoding.UTF8.GetString(numArray, 0, byteCount);
            List<string> list = ((IEnumerable<string>)msg.Split(new string[1] { "\n\r\0" }, StringSplitOptions.None)).ToList<string>();
            msg = list.Last();
            list.Remove(list.Last());
            list.ForEach(m => TD2.MessageReceived(m));

            ++receiveCounter;
        }

        public static void MessageReceived(string msg)
        {
            if (msg.StartsWith("Ready: "))
            {
                msg = msg.Replace("Ready: ", "");
                msg = msg[0..^12];
                Console.Title = $"W24  [{msg}]";
            }

            string[] strArray = msg.Split(new char[1] { ':' }, 2);
            string name = strArray[0];
            string response = strArray[1];

            if (response.StartsWith("/bl"))
            {
                return;
            }
            else
            {
                var railObject = Program.RailObjectList.Find(x => x.Name == name);

                if (railObject != null)
                {
                    railObject.ChangeState(response);
                }
            }
        }
    }
}
