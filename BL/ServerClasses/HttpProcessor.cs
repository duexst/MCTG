using BL.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    class HttpProcessor
    {
        private TcpClient socket;
        private Server Server;


        public HttpProcessor(TcpClient s, Server server)
        {
            this.socket = s;
            this.Server = server;
        }

        public void Process()
        {
            var writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
            var reader = new StreamReader(socket.GetStream());
            Console.WriteLine();

            // read (and handle) the full HTTP-request
            Console.WriteLine("Waiting for request");

            MyHttpRequest request = HttpReader.ReadRequest(reader); // was hat der browser geschickt

            HttpController controller = null;

            Console.WriteLine("Handling request");
            Console.WriteLine($"Request path: {request.Path}");
            var path = request.Path.Split("/");
            Console.WriteLine($"parsed path: {path[1]}");

            if (Server.Controllers.TryGetValue(path[1], out controller) == true)
            {
                switch (request.Verb.ToLower())
                {
                    case "get":
                        HttpWriter.WriteResponse(controller.Get(request), writer);
                        break;

                    case "post":
                        HttpWriter.WriteResponse(controller.Post(request), writer);
                        break;

                    case "delete":
                        HttpWriter.WriteResponse(controller.Delete(request), writer);
                        break;

                    case "put":
                        HttpWriter.WriteResponse(controller.Put(request), writer);
                        break;

                    default:
                        break;

                }
            }
            else
            {
                Console.WriteLine($"kein Controller für Request {request.Path}");
            }

            Console.WriteLine("Request done");
            writer.Flush();
            writer.Close();
        }
    }
}
