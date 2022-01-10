using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class UserScore
    {
        public string Username { get; set; }
        public int Elo { get; set; }

        public UserScore(string Username, int Score)
        {
            this.Username = Username;
            this.Elo = Score;
        }
    }
}
