using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Core;

namespace RPG_Turn_Based_Battle_System.Spells
{
    internal class Heal : Ability
    {
        public Heal() {
            Name = "Heal";
            ManaCost = 25;
            CastOnAlly = true;
        }

        public override void Use(Character caster, Character target)
        {

            if (caster.Mana < ManaCost)
            {
                Console.WriteLine($"Not enough mana to cast {Name}");
                return;
            }
            else { 
            int healAmount = 10+ caster.SpellPower ;
            target.Health += healAmount;
            caster.Mana -= ManaCost;
            Console.WriteLine($"{caster.Name} Heals {target.Name} for {healAmount} HP!");            
            }


        }
    }
}
