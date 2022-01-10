using BL.ServerClasses;
using DAL.DB;
using System;

namespace BL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Monster Card Trading Game Server");
            Console.CancelKeyPress += (sender, e) => Environment.Exit(0);

            Server server = new Server(8080, new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG"));
            server.InitRepository();
            server.InitControllers();

            server.Run();
        }
    }
}
