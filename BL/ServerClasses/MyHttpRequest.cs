using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    public class MyHttpRequest
    {
        private Dictionary<string, string> m_headers; 
        public string Verb { get; set; } //Methode Get,Post,Delete
        public string Path { get; set; } // nicht http://localhost:8080/user -> sonder /user
        public string Body { get; set; }
        public string Version { get; set; }
        public MyHttpRequest()
        {
            m_headers = new Dictionary<string, string>();
            Verb = null;
            Path = null;
            Body = null;
        }

        public string GetHeaderValue(string header)
        {
            string value = null;

            if (m_headers.TryGetValue(header, out value))
            {
                return value;
            }
            else
            {
                return "";
            }
        }

        public void AddHeader(string key, string value)
        {
            m_headers.Add(key, value);
        }
    }
}
