using BL.Helpers;
using DAL;
using MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BattleLogic
{
    public enum BattleState
    {
        Finished = 0,
        Ongoing = 1,
        Created = -1
    }
    public class Battle
    {
        private Player player1;
        private Player player2;
        private Log log;
        private int m_state;
        private UserRepository m_userRepository;


        public Battle(Player p1, Player p2, UserRepository userRepo)
        {
            player1 = p1;
            player2 = p2;
            log = new(GetTimeStamp());
            m_userRepository = userRepo;
            m_state = (int)BattleState.Created;
        }

        public BattleResult Run()
        {
            int rounds = 1;
            var result = new BattleResult();
            result.Winner = null;

            while(rounds < 100 && result.Winner == null)
            {
                log.WriteLog("------------------------------------------------");
                log.WriteLog("New Round started:");
                log.WriteLog(" ");
                int outcome = 0;

                //Beide Spieler ziehen ihre Karte
                Card p1 = DrawCard(player1.Deck, player1.Deck.Count);
                Card p2 = DrawCard(player2.Deck, player2.Deck.Count);

                string player1winround = $"{player1.AccInfo.Username} won round {rounds}";
                string player2winround = $"{player2.AccInfo.Username} won round {rounds}";
                string draw = $"There was no winner in round {rounds}";

                //Die Karten Kämpfen gegeneinander
                if(p1.Type == "Spell" || p2.Type == "Spell")
                {
                    outcome = SpellFight(p1, p2);
                    if(outcome == 1)
                    {
                        log.WriteLog(player1winround);

                        player1.Deck.Add(p2);
                        player2.Deck.Remove(p2);
                    }
                    
                    if(outcome == 2)
                    {
                        log.WriteLog(player2winround);

                        player2.Deck.Add(p1);
                        player1.Deck.Remove(p1);
                    }
                    
                    if(outcome == 0)
                    {
                        log.WriteLog(draw);
                    }
                }
                else
                {
                    outcome = MonsterFight(p1, p2);
                    if(outcome == 1)
                    {
                        log.WriteLog(player1winround);

                        player1.Deck.Add(p2);
                        player2.Deck.Remove(p2);
                    }
                    
                    if(outcome == 2)
                    {
                        log.WriteLog(player2winround);

                        player2.Deck.Add(p1);
                        player1.Deck.Remove(p1);
                    }

                    if(outcome == 0)
                    {
                        log.WriteLog(draw);
                    }
                }
                log.WriteLog("------------------------------------------------");
                //Überprüfen ob es einen Gewinner gibt
                if (player1.Deck.Count == 0)
                {
                    result.Winner = player2;
                    result.Loser = player1;
                }
                
                if(player2.Deck.Count == 0)
                {
                    result.Winner = player1;
                    result.Loser = player2;
                }

                rounds++;
                log.WriteLog("The round is over");
            }

            log.WriteLog("------------------------------------------------");

            if (result.Winner == null)
            {
                result.draw = true;
                log.WriteLog("The Battle ended in a Draw");
            }
            else
            {
                log.WriteLog($"{result.Winner.AccInfo.Username} won the Battle");
            }

            log.WriteLog("------------------------------------------------");

            result.BattleLog = log;

            if(result.Winner == player1)
            {
                player1.AccInfo.Elo += 5;

                if ((player2.AccInfo.Elo -= 3) < 0)
                    player2.AccInfo.Elo = 0;
            }

            if(result.Winner == player2)
            {
                player2.AccInfo.Elo += 5;

                if ((player1.AccInfo.Elo -= 3) < 0)
                    player1.AccInfo.Elo = 0;
            }
            UpdateElo(player1.AccInfo, player2.AccInfo);

            return result;
        }

        private Card DrawCard(List<Card> deck, int size)
        {
            var rand = new Random();
            int i = rand.Next(0, size);

            return deck.ElementAt(i);
        }

        private int MonsterFight(Card p1, Card p2)
        {
            string logtext = $"{p1.Name}(Dmg: {p1.Damage}) fights against {p2.Name}(Dmg: {p2.Damage})";
            log.WriteLog(logtext);

            //Player1 instant win
            if(SpecialRule(p1, p2))
                return 1;

            //Player2 instant win
            if(SpecialRule(p2, p1))
                return 2;

            //Player1 win
            if (p1.Damage > p2.Damage)
            {
                log.WriteLog($"{p1.Name} destroyed {p2.Name}");
                return 1;
            }

            //Player2win
            if (p1.Damage < p2.Damage)
            {
                log.WriteLog($"{p2.Name} destroyed {p1.Name}");
                return 2;
            }

            //Draw
            return 0;
        }

        private int SpellFight(Card p1, Card p2)
        {
            string logtext = $"{p1.Name}(Dmg: {p1.Damage}) fights against {p2.Name}(Dmg: {p2.Damage})";
            log.WriteLog(logtext);
            //Player1 instant win
            if (SpecialRule(p1, p2))
                return 1;

            //player2 instant win
            if (SpecialRule(p2, p1))
                return 2;

            //Damage Modifiers
            if (ElementEffective(p1, p2))
            {
                p1.Damage *= 2;
                p2.Damage /= 2;
            }

            if (ElementEffective(p2, p1))
            {
                p2.Damage *= 2;
                p1.Damage /= 2;
            }

            //Player1 win
            if (p1.Damage > p2.Damage)
            {
                log.WriteLog($"{p1.Name} destroyed {p2.Name}");
                return 1;
            }

            //Player2 win
            if (p1.Damage < p2.Damage)
            {
                log.WriteLog($"{p2.Name} destroyed {p1.Name}");
                return 1;
            }

            //Draw
            return 0;
        }

        public bool ElementEffective(Card attacker, Card defender)
        {
            string action = $"{attacker.Element} is effective against {defender.Element}";

            if (attacker.Element == "Water" && defender.Element == "Fire")
            {
                log.WriteLog(action);
                return true;
            }

            if (attacker.Element == "Fire" && defender.Element == "Earth")
            {
                log.WriteLog(action);
                return true;
            }

            if (attacker.Element == "Earth" && defender.Element == "Water")
            {
                log.WriteLog(action);
                return true;
            }

            return false;
        }

        public bool SpecialRule(Card attacker, Card defender)
        {
            string action = $"{attacker.Name} is the nemesis of {defender.Name} and instantly destroyed its opponent";

            if (attacker.Name == "Dragon" && defender.Name == "Goblin")
            {
                log.WriteLog(action);
                return true;
            }

            if (attacker.Name == "FireElf" && defender.Name == "Dragon")
            {
                log.WriteLog(action);
                return true;
            }

            if (attacker.Name == "Wizard" && defender.Name == "Ork")
            {
                log.WriteLog(action);
                return true;
            }

            if ((attacker.Type == "Spell" && attacker.Element == "Water") && defender.Name == "Knight")
            {
                log.WriteLog(action);
                return true;
            }

            if (attacker.Name == "Kraken" && defender.Type == "Spell")
            {
                log.WriteLog(action);
                return true;
            }

            return false;
        }

        public string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyymmddhhmmssfff");
        }

        public void SetState(BattleState state)
        {
            m_state = (int)state;
        }

        public int GetState()
        {
            return m_state;
        }

        public void UpdateElo(User user1, User user2)
        {
            m_userRepository.Update(user1);
            m_userRepository.Update(user2);
        }
    }
}
