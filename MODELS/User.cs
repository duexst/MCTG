using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class User
    {
        public string Guid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public int ?Elo { get; set; }

        public User(string id, string username, string password, int coins, int ?elo)
        {
            Guid = id;
            Username = username;
            Password = password;
            Coins = coins;
            Elo = elo;
            
        }
    }
}
