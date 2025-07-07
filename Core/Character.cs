using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Core
{
    public abstract class Character : ICombatant
    {
        private int health;
        public string Name { get; set; }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if (value >= MaxHealth)
                {
                    health = MaxHealth;
                }
                else if (Health - value < 0)
                {
                    health = 0;
                }
                else { health = value; }
            }
        }

        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }
        public int SpellPower { get; set; }
        public int Defense { get; set; }

        public bool IsDefeated
        { get { return Health <= 0; } }

        public Character(string name, int health, int maxHealth, int attackPower, int spellPower, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            SpellPower = spellPower;
            Defense = defense;
        }

        public Character(string name, int health, int maxHealth, int attackPower, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            Defense = defense;
        }

        public abstract void Attack(ICombatant target);


        public abstract void Spell(ICombatant target);

        public abstract void TakeDamage(int amount);
    }
}