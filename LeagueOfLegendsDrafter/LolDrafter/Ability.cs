using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolDrafter.enums;

namespace LolDrafter
{
    public class Ability
    {
        // Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int Cost { get; set; }
        public CostType TypeOfCost { get; set; }
        public bool IsUltimate { get; set; }

        // Constructor
        public Ability() { }

        public Ability(string name, string description, int cooldown, int cost, CostType abilityType, bool isUltimate)
        {
            Name = name;
            Description = description;
            Cooldown = cooldown;
            Cost = cost;
            TypeOfCost = abilityType;
            IsUltimate = isUltimate;
        }
    }
}
