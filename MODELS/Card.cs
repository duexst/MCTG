using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class Card
    {
        public string Uid { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Damage { get; set; }
        public string Element { get; set; }
        public bool Active { get; set; }

        public Card(string uid, string owner, string name, string type, int damage, string element, bool active)
        {
            Uid = uid;
            Owner = owner;
            Name = name;
            Type = type;
            Damage = damage;
            Element = element;
            Active = active;
        }
    }
}
