using DAL.DB;
using DAL.Repository;
using MODELS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Test.RepositoryTests
{
    class CardRepositoryTest
    {
        [Test]
        public void TestAddCard_Elf()
        {
            //Arrange
            string uid = Guid.NewGuid().ToString();
            var card = new Card(uid, "b298eb4f-7b90-449a-9628-03a422f9eba3", "Elf", "Monster", 200, "Water", true);
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var cardrepo = new CardRepository(db);

            //Act
            bool created = cardrepo.Create(card);

            //Assert
            Assert.IsTrue(created);
        }

        [Test]
        public void TestGetDeckById_IntegrationtestId()
        {
            //Arrange
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var cardrepo = new CardRepository(db);
            List <Card> res = new();
            //Act
            res = cardrepo.GetDeckById("b298eb4f-7b90-449a-9628-03a422f9eba3");

            //Assert
            Assert.NotNull(res);
        }
    }
}
