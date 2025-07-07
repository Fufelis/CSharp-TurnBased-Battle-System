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
        public bool IsPlayer { get;set; }
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
        public int Defense { get; set; }
        public int Speed { get; set; }

        public bool IsAlive
        { get { return Health >= 1; } }

        public Character(string name, int health, int maxHealth, int attackPower, int spellPower, int defense, int speed,bool isPlayer)
        {
            Name = name;
           

            MaxHealth = maxHealth;
            Health = health;
            AttackPower = attackPower;
            SpellPower = spellPower;
            Defense = defense;
            Speed = speed;
            IsPlayer = isPlayer;
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


        public abstract void Spell(ICombatant target);

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if(Health < 0) Health = 0;

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
            return AttackPower-(target.Defense/2);
            //for future 
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