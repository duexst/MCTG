using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class Log
    {
        public string path { get; set; }
        public Log(string stamp)
        {
            path = $"C:/Users/Stefan/MCTGBattlelogs/{stamp}.txt";
        }

        public void WriteLog(string action)
        { 
            try
            {
                TextWriter writer = new StreamWriter(path, true);
                writer.WriteLine(action);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public List<string> ReadLog()
        {
            {
                TextReader reader = new StreamReader(path);
                string action;
                List<string> events = new();
                while ((action = reader.ReadLine()) != null)
                {
                    events.Add(action);
                }

                reader.Close();
                return events;
            }
        }
    }
}
