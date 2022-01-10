using DAL.DB;
using DAL.Repository;
using MODELS;
using System;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            //var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            //var userrepo = new UserRepository(db);
            //var cardrepo = new CardRepository(db);

            //var TestUser1 = new User(Guid.NewGuid().ToString(), "stefan", "test123", 70);
            //var TestUser2 = new User(Guid.NewGuid().ToString(), "jonas", "bla", 90);
            //var TestUser3 = new User(Guid.NewGuid().ToString(), "bekki", "haha84", 100);

            //var TestCard1 = new Card(Guid.NewGuid().ToString(), TestUser1.Guid, "Dragot", "Monster", 89, "fire", 75);
            //var TestCard2 = new Card(Guid.NewGuid().ToString(), TestUser2.Guid, "Selo", "Monster", 89, "water", 67);
            //var TestCard3 = new Card(Guid.NewGuid().ToString(), TestUser3.Guid, "Egora", "Monster", 89, "earth", 75);

            //userrepo.Create(TestUser1);
            //userrepo.Create(TestUser2);
            //userrepo.Create(TestUser3);

            //cardrepo.Create(TestCard1);
            //cardrepo.Create(TestCard2);
            //cardrepo.Create(TestCard3);

            //if (db.CloseCon())
            //{
            //    Console.WriteLine("Connection closed");
            //}
            //else
            //{
            //    Console.WriteLine("Connection not closed");
            //}
        }
    }
}
