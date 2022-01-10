using BL.Helpers;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BattleLogic
{
    public class BattleResult
    {
        public Player Winner { get; set; }
        public Player Loser { get; set; }
        public bool draw { get; set; }
        public Log BattleLog { get; set; }
    }
}
