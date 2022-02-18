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
            RailObjectLists = new();
            try
            {
                FileReader.Read("przebiegi.txt");
            }
            catch
            {
                Console.WriteLine("Plik przebiegi.txt nie istnieje w katalogu programu");
                Console.WriteLine("Podaj nazwę pliku: ");

                try
                {
                    FileReader.Read(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Plik nie istnieje. Naciśnij dowolny klawisz aby zamknąć program");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("port: ");
            Connection.port = int.Parse(Console.ReadLine());
            Connection.Connect();

            while (true)
            {
                await Task.Delay(1000);

                if (!Connection.IsConnected)
                {
                    Console.WriteLine("próba nawiązania połączenia...");
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
