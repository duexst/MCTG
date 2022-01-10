using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Player
    {
        public User AccInfo { get; set; }
        public List<Card> Stack { get; set; }
        public List<Card> Deck { get; set; }
    }
}
