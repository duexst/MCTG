using DAL;
using DAL.DB;
using MODELS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Test.RepositoryTests
{
    class UserRepositoryTest
    {
        [Test]
        public void TestAddUser()
        {
            //Arrange
            string uid = Guid.NewGuid().ToString();
            var user = new User(uid, Guid.NewGuid().ToString(), "NunitPassword", 100, 200);
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var userrepo = new UserRepository(db);

            //Act
            bool created = userrepo.Create(user);

            //Assert
            Assert.IsTrue(created);
        }

        [Test]
        public void TestFindUserByName()
        {
            //Arrange
            string uid = Guid.NewGuid().ToString();
            var user = new User(uid, "NUnitUser4", "NunitPassword", 100, 200);
            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var userrepo = new UserRepository(db);

            //Act
            userrepo.Create(user);

            //Assert
            Assert.NotNull(userrepo.FindUserByName("NUnitUser4"));
        }
    }
}
