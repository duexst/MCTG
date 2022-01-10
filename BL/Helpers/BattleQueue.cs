using BL.BattleLogic;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    class BattleQueue
    {
        private Queue<Player> m_Players;
        private readonly Object m_LockQ = new Object();
        private Dictionary<string, BattleObject> m_Battles;
        
        public BattleQueue()
        {
            m_Players = new();
            m_Battles = new();
        }

        public void QueuePlayer(Player player)
        {
            lock (m_LockQ)
            {
                m_Players.Enqueue(player);
            }
        }

        public Player DequeuePlayer()
        {
            lock (m_LockQ)
            {
                if (m_Players.Count == 0)
                    return null;

                return m_Players.Dequeue();
            }
        }

        public int Count()
        {
            lock (m_LockQ)
            {
                return m_Players.Count;
            }
        }

        public void UpdateBattle(string id)
        {

        }
    }
}
