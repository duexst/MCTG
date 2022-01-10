using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    public static class HttpReader
    {
        public static MyHttpRequest ReadRequest(StreamReader reader)
        {
            var request = new MyHttpRequest();
            string line = null;

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                

                if (line.Length == 0)
                {
                    break;  // Head is over Body will follow now
                }
                // handle first line of HTTP
                if (request.Verb == null)
                {
                    var parts = line.Split(' ');
                    request.Verb = parts[0];
                    request.Path = parts[1];
                    request.Version = parts[2];
                }
                // handle HTTP headers
                else
                {
                    var parts = line.Split(": ");
                    request.AddHeader(parts[0], parts[1]);
                }
            }
            //Get the Body with this method, because reader will get stuck if trying to look for end of stream or newline
            string body = "";
            while (reader.Peek() >= 0)
            {
                body += (char)reader.Read();
            }

            request.Body = body;
            Console.WriteLine(request.Verb);
            Console.WriteLine(request.Path);
            Console.WriteLine(request.Body);
            return request;
        }
    }
}
