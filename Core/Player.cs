using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Player : Character , ICombatant
    {
        public Player(string name, int health, int attackPower, int spellPower, int mana, int defense, int speed, bool isPlayer) : base(name, health, attackPower, spellPower, mana, defense, speed, isPlayer)
        {
        }



        public override void Spell(ICombatant target)
        {
            Console.WriteLine("NO IMPLEMENTATION");
        }

    }
}
