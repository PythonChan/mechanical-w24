using System;
using System.Collections.Generic;
using System.IO;

namespace W24
{
    internal class FileReader
    {
        public static void Read(string filename = "MKs.txt")
        {
            try
            {
                foreach (string line in File.ReadLines(filename))
                {
                    List<RailObject> list = new();

                    string[] lineArray = line.Split(" ");
                    foreach (string obj in lineArray)
                    {
                        string[] strArray = obj.Split(new char[1] { ':' }, 2);
                        string name = strArray[0];
                        string response = strArray[1];

                        RailObject railObject = new(name, response);
                        list.Add(railObject);
                    }

                    Program.RailObjectLists.Add(list);
                }
            }
            catch
            {
                Console.WriteLine("Błąd wczytywania pliku");
            }
        }
    }
}
