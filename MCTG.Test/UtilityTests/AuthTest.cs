using BL.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Test.UtilityTests
{
    class AuthTest
    {
        [Test]
        public void TestAddAuthUser_TokenAndUserObject()
        {
            //Arrange
            var authTest = new Auth();
            var authUser = new AuthUser("50061b02-f67e-4977-827d-8741d1ff5754", "NUnitTest");
            //Act
            authTest.AddAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6", authUser);
            //Assert
            Assert.AreEqual(authUser, authTest.GetAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6"));
        }

        [Test]
        public void TestDelAuthUser_Token()
        {
            //Arrange
            var authTest = new Auth();
            var authUser = new AuthUser("50061b02-f67e-4977-827d-8741d1ff5754", "NUnitTest");
            //Act
            authTest.AddAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6", authUser);
            authTest.DelAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6");
            //Assert
            Assert.IsNull(authTest.GetAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6"));
        }

        [Test]
        public void TestDelAuthUser_AuthUserObject()
        {
            //Arrange
            var authTest = new Auth();
            var authUser = new AuthUser("50061b02-f67e-4977-827d-8741d1ff5754", "NUnitTest");
            //Act
            authTest.AddAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6", authUser);
            string username = authTest.GetUsername("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6");
            //Assert
            Assert.AreEqual(authUser.username, username);
        }

        [Test]
        public void TestGetId_AuthUserObject()
        {
            //Arrange
            var authTest = new Auth();
            var authUser = new AuthUser("50061b02-f67e-4977-827d-8741d1ff5754", "NUnitTest");
            //Act
            authTest.AddAuthUser("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6", authUser);
            string id = authTest.GetId("767bf09f-d1a2-44a8-9fb6-8f45bd0550f6");
            //Assert
            Assert.AreEqual(id, authUser.uid);
        }
    }
}
