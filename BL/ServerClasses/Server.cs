using BL.BattleLogic;
using BL.Controllers;
using BL.Helpers;
using DAL;
using DAL.DB;
using DAL.Repository;
using MODELS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL.ServerClasses
{
    class Server
    {
        protected int port;
        TcpListener listener;

        public Dictionary<string, HttpController> Controllers; //Request pfad -> controller

        private Database m_db;

        public Auth AuthUsers;
        public BattleQueue BattleQ;
        public List<Battle> Battles;
        public UserRepository UserRepository;
        public CardRepository CardRepository;
        public PackageRepository PackageRepository;

        public Server(int port, Database database)
        {
            this.port = port;
            this.m_db = database;
            this.Controllers = new Dictionary<string, HttpController>();
            this.AuthUsers = new Auth();
            this.BattleQ = new();
            Battles = new();
        }

        public void Run()
        {
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start(5);
            while (true)
            {
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                new Thread(processor.Process).Start();
                Thread.Sleep(1);

                if(BattleQ.Count() > 1)
                {
                    Player player1 = BattleQ.DequeuePlayer();
                    Player player2 = BattleQ.DequeuePlayer();

                    Battles.Add(new Battle(player1, player2, this.UserRepository));
                }

                foreach(Battle battle in Battles)
                {
                    if(battle.GetState() == -1)
                    {
                        battle.Run();
                        Console.WriteLine("Commencing Battle");
                    }
                }
            }
        }

        public void InitRepository()
        {
            UserRepository = new(m_db);
            CardRepository = new(m_db);
            PackageRepository = new(m_db);
        }

        public void RegisterController(string path, HttpController controller)
        {
            Controllers[path] = controller;
        }
        public void InitControllers()
        {
            RegisterController("users", new UserController(this.UserRepository, this.AuthUsers));
            RegisterController("sessions", new SessionController(this.UserRepository, this.AuthUsers));
            RegisterController("cards", new CardController(this.CardRepository, this.AuthUsers));
            RegisterController("packages", new PackageController(this.PackageRepository, this.AuthUsers));
            RegisterController("transactions", new TransactionController(this.UserRepository, this.CardRepository, this.PackageRepository, this.AuthUsers));
            RegisterController("deck", new DeckController(this.CardRepository, this.AuthUsers));
            RegisterController("stats", new ScoreController(this.UserRepository, this.AuthUsers));
            RegisterController("battle", new BattleController(this.UserRepository, this.CardRepository, this.AuthUsers, this.BattleQ));
        }
    }
}
