using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Player : Character , ICombatant
    {
        public Player(string name, int health, int maxHealth, int attackPower, int spellPower, int defense, int speed, bool isPlayer) :base(name, health, maxHealth, attackPower,spellPower,defense, speed, isPlayer)
        {
         
        }



        public override void Spell(ICombatant target)
        {
            Console.WriteLine("NO IMPLEMENTATION");
        }

    }
}
