using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal abstract class Character : ICombatant
    {
        private int health;
        private int mana;
        private List<Ability> abilities;

        public List<Ability> Abilities { get { return abilities; } }
        public bool IsPlayer { get; set; }
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
                else if (value <= 0)
                {
                    health = 0;
                }
                else { health = value; }
            }
        }

        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }
        public int SpellPower { get; set; }
        public int MaxMana { get; set; }
        public int Mana
        {
            get
            {
                return mana;
            }

            set
            {
                if (value >= MaxMana)
                {
                    mana = MaxMana;
                }
                else if (value <= 0)
                {
                    mana = 0;
                }
                else
                {
                    mana = value;
                }
            }
        }


        public int Defense { get; set; }
        public int Speed { get; set; }

        public bool IsAlive
        { get { return Health >= 1; } }

        public Character(string name, int health, int attackPower, int spellPower, int mana, int defense, int speed, bool isPlayer)
        {
            Name = name;

            MaxHealth = health;
            Health = health;
            MaxMana = mana;
            Mana = mana;
            AttackPower = attackPower;
            SpellPower = spellPower;
            Defense = defense;
            Speed = speed;
            IsPlayer = isPlayer;
            abilities = new List<Ability>();
        }

        public Character(string name, int health, int maxHealth, int attackPower, int defense)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            Defense = defense;
        }

        public void Attack(ICombatant target)
        {
            target.TakeDamage(AttackPower);
        }


        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public void ApplyStartOfTurnEffects()
        {
            Console.WriteLine($"{Name} applies start of turn effects.");
        }

        public void ApplyEndOfTurnEffects()
        {
            Console.WriteLine($"{Name} applies end of turn effects.");
        }

        public int CalculateAttackDamage(Character target)
        {
            int damage = AttackPower - (target.Defense / 2);
            
            if(damage <= 0) { damage = 1; }
            return damage;
            
            // return AttackPower - (target.Defense / 2);
            //for future
        }
        public void LearnAbility(Ability ability)
        {
            abilities.Add(ability);
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"---{Name}`s Info---");
            Console.WriteLine($"Health : {Health} / {MaxHealth}");
            Console.WriteLine($"Attack : {AttackPower} Spell Power : {SpellPower} ");
            Console.WriteLine($"Defense : {Defense}");
        }
    }
}