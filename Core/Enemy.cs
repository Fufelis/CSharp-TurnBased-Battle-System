using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Enemy : Character , ICombatant
    {

        public Enemy(string name, int health, int maxHealth, int attackPower, int spellPower, int defense) : base(name, health, maxHealth, attackPower, spellPower, defense)
        {

        }
    }
}
