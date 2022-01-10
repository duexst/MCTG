using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    public enum HttpStatus
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        Forbidden = 401,
        Notfound = 404,
        Conflict = 409
    };
    public class MyHttpResponse
    {
        public string Version { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        private Dictionary<string, string> m_headers;
        public string Body { get; set; }

        public MyHttpResponse()
        {
            m_headers = new Dictionary<string, string>();
            Version = "HTTP/1.1";
            StatusText = "";
            Body = "";
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

        public void SetStatus(HttpStatus status, string statusText)
        {
            this.Status = (int)status;
            this.StatusText = statusText;
        }
    }
}
