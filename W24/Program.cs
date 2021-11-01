using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace W24
{
    internal class Program
    {
        public static List<RailObject> RailObjectList;

        static async Task Main(string[] args)
        {
            RailObject r104 = new("MKsA_104", "-");
            RailObject r105 = new("MKsA_105", "-");
            RailObject B = new("MKsA_B", "S13");

            RailObjectList = new List<RailObject>
            {
                r104,
                r105,
                B
            };

            //Console.WriteLine("port: ");
            //Connection.port = int.Parse(Console.ReadLine());
            Connection.Connect();

            while (true)
            {
                await Task.Delay(1000);

                if (!Connection.IsConnected)
                {
                    continue;
                }

                bool allTrue = true;
                string semafor = "";

                foreach (RailObject railObject in RailObjectList)
                {
                    if (!railObject.StateAsExpected || railObject.W24)
                    {
                        allTrue = false;
                        break;
                    }

                    if (railObject.ExpectedState is not "-" and not "+")
                    {
                        semafor = railObject.Name;
                    }
                }

                if (allTrue)
                {
                    Connection.Send($"{semafor}:W24");
                }
            }
        }
    }
}
