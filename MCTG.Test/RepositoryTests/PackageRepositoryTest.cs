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
    class PackageRepositoryTest
    {
        [Test]
        public void TestRead_FalseId()
        {
            //Arrange
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var packagerepo = new PackageRepository(db);

            //Act
            Package cards = packagerepo.Read("fauxid");

            //Assert
            Assert.IsNull(cards);
        }

        [Test]
        public void TestAddPackage_Owner()
        {
            //Arrange
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var packagerepo = new PackageRepository(db);

            string owner = Guid.NewGuid().ToString();
            Package  package = new();
            package.Cards = new List<Card>();

            package.Cards.Add(new Card(Guid.NewGuid().ToString(), owner, "Ork", "Monster", 20, "Fire", false));
            package.Cards.Add(new Card(Guid.NewGuid().ToString(), owner, "Ork", "Monster", 20, "Fire", false));
            package.Cards.Add(new Card(Guid.NewGuid().ToString(), owner, "Ork", "Monster", 20, "Fire", false));
            package.Cards.Add(new Card(Guid.NewGuid().ToString(), owner, "Ork", "Monster", 20, "Fire", false));

            //Act
            bool created = packagerepo.Create(package);

            //Assert
            Assert.IsTrue(created);
        }
    }
}
