using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Player : Character , ICombatant
    {
        public Player(string name, int health, int maxHealth, int attackPower, int spellPower, int defense) :base(name, health, maxHealth, attackPower,spellPower,defense)
        {
         
        }

        public override void TakeDamage(int amount) {
            int damage = amount - Defense;
            if (damage <= 1)
            {
                damage = 1;
                Health -=damage;
            }
            else
            {
                Health -= damage;
            }

        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine("NO IMPLEMENTATION");
        }

        public override void Spell(ICombatant target)
        {
            Console.WriteLine("NO IMPLEMENTATION");
        }
    }
}
