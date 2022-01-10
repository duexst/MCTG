using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    public static class HttpWriter
    {
        public static void WriteResponse(MyHttpResponse response, StreamWriter writer)
        {
            writer.WriteLine($"{response.Version} {response.Status} {response.StatusText}");


            //ab hier optional
            writer.WriteLine($"Server: MCTG-Server");
            if (response.GetHeaderValue("Content-Type") != "")
                writer.WriteLine($"Content-Type: {response.GetHeaderValue("Content-Type")}");

            if(response.GetHeaderValue("Content-Length") != "")
                writer.WriteLine($"Content-Length: {response.GetHeaderValue("Content-Length")}");

            writer.WriteLine("");
            writer.WriteLine($"{response.Body}");
        }
    }
}
