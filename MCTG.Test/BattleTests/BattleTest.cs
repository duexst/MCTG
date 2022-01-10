using BL.BattleLogic;
using DAL;
using DAL.DB;
using MODELS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Test.BattleTests
{
    public class BattleTest
    {
        [Test]
        public void TestIsEffective_WaterVsFire()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var fireCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Dragon", "Spell", 100, "Fire", true);
            var waterCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Ork", "Monster", 200, "Water", true);
            //Act
            bool effective = battle.ElementEffective(waterCard, fireCard);

            //Assert
            Assert.IsTrue(effective);
        }

        [Test]
        public void TestIsEffective_FireVsEarth()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var fireCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Dragon", "Spell", 100, "Fire", true);
            var earthCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Ork", "Monster", 200, "Water", true);
            //Act
            bool effective = battle.ElementEffective(fireCard, earthCard);

            //Assert
            Assert.IsTrue(effective);
        }

        [Test]
        public void TestIsEffective_WaterVsEarth()
        {
            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var earthCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Dragon", "Spell", 100, "Earth", true);
            var waterCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Ork", "Monster", 200, "Water", true);
            //Act
            bool effective = battle.ElementEffective(waterCard, earthCard);

            //Assert
            Assert.IsTrue(!effective);
        }

        [Test]
        public void TestSpecialRule_KrakenVsSpell()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var krakenCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Kraken", "Monster", 100, "Fire", true);
            var spellCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "FireSpell", "Spell", 200, "Fire", true);
            //Act
            bool special = battle.SpecialRule(krakenCard, spellCard);

            //Assert
            Assert.IsTrue(special);
        }

        [Test]
        public void TestSpecialRule_DragonVsElf()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var dragonCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Dragon", "Monster", 100, "Fire", true);
            var elfCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Elf", "Monster", 200, "Fire", true);
            //Act
            bool special = battle.SpecialRule(dragonCard, elfCard);

            //Assert
            Assert.IsTrue(!special);
        }

        [Test]
        public void TestSpecialRule_DragonVsGoblin()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var dragonCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Dragon", "Monster", 100, "Fire", true);
            var goblinCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Goblin", "Monster", 200, "Fire", true);
            //Act
            bool special = battle.SpecialRule(dragonCard, goblinCard);

            //Assert
            Assert.IsTrue(special);
        }

        [Test]
        public void TestSpecialRule_WaterSpellVsKnight()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "TestUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "TestUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            var spellCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true);
            var knightCard = new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Knight", "Monster", 200, "Fire", true);
            //Act
            bool special = battle.SpecialRule(spellCard, knightCard);

            //Assert
            Assert.IsTrue(special);
        }

        [Test]
        public void TestRun_BalancedDeckVsBalancedDeck()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "BalancedUser1", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "BalancedUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            player1.Deck = new();
            player2.Deck = new();

            player1.AccInfo = user1;
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));

            player2.AccInfo = user2;
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            //Act
            BattleResult result = battle.Run();

            //Assert
            Assert.IsTrue(result.draw);
        }

        [Test]
        public void TestRun_OpDeckVsBalancedDeck()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "OpUser", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "BalancedUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            player1.Deck = new();
            player2.Deck = new();

            player1.AccInfo = user1;
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Kraken", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Kraken", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Kraken", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Kraken", "Monster", 100, "Water", true));

            player2.AccInfo = user2;
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            //Act
            BattleResult result = battle.Run();

            //Assert
            Assert.AreEqual(result.Winner, player1);
        }

        [Test]
        public void TestRun_WeakDeckVsBalancedDeck()
        {
            //Arrange
            var user1 = new User(Guid.NewGuid().ToString(), "OpUser", "Test123", 20, 1000);
            var user2 = new User(Guid.NewGuid().ToString(), "BalancedUser2", "Test1413", 30, 2000);

            var player1 = new Player();
            var player2 = new Player();

            player1.Deck = new();
            player2.Deck = new();

            player1.AccInfo = user1;
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Knight", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Knight", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Knight", "Monster", 100, "Water", true));
            player1.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "Knight", "Monster", 100, "Water", true));

            player2.AccInfo = user2;
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));
            player2.Deck.Add(new Card(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "WaterSpell", "Spell", 100, "Water", true));

            var db = new Database("Host=localhost;Username=postgres;Password=123;Database=MCTG");
            var battle = new Battle(player1, player2, new UserRepository(db));

            //Act
            BattleResult result = battle.Run();

            //Assert
            Assert.AreEqual(result.Winner, player2);
        }

        
    }
}
