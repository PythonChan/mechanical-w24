using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace W24
{
    internal class Program
    {
        public static List<List<RailObject>> RailObjectLists;

        [STAThread]
        static async Task Main(string[] args)
        {
            //RailObject r104 = new("MKsA_104", "-");
            //RailObject r105 = new("MKsA_105", "-");
            //RailObject B = new("MKsA_B", "S13");

            RailObjectLists = new();

            Console.WriteLine("Nazwa pliku: ");
            FileReader.Read(Console.ReadLine());

            Console.WriteLine("port: ");
            Connection.port = int.Parse(Console.ReadLine());
            Connection.Connect();

            while (true)
            {
                await Task.Delay(1000);

                if (!Connection.IsConnected)
                {
                    continue;
                }

                foreach (List<RailObject> list in RailObjectLists)
                {
                    bool allTrue = true;
                    string semafor = "";

                    foreach (RailObject railObject in list)
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
                        Console.WriteLine($"Ułożono przebieg na {semafor}");
                    }
                }
            }
        }
    }
}
