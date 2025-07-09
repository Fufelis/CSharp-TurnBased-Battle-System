using RPG_Turn_Based_Battle_System.Items.Consumables;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal abstract class Character : ICombatant
    {
        private int health;
        private int mana;
        private List<Ability> abilities;
        private List<Consumable> consumableInventory;
        private const int CONSUMABLE_INVENTORY_SIZE = 5;

        public List<Ability> Abilities
        { get { return abilities; } }

        public List<Consumable> ConsumableInventory
        { get { return consumableInventory; } }
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
            consumableInventory = new List<Consumable>();
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

            if (damage <= 0) { damage = 1; }
            return damage;

            //for future
        }

        public void LearnAbility(Ability ability)
        {
            abilities.Add(ability);
        }

        public void AddConsumable(Consumable consumable)
        {
            if (consumableInventory.Count >= CONSUMABLE_INVENTORY_SIZE)
            {
                Console.WriteLine("inventory full cannot pick up item");
                //future discard option
                return;
            }
            else
            {
                consumableInventory.Add(consumable);
                Console.WriteLine($"{Name} picked up {consumable.Name}");
            }
        }

        public void UseConsumable(Consumable consumable)
        {
            switch (consumable.Type)
            {
                case ConsumableType.Health:
                    {
                        Health += consumable.Value;
                        Console.WriteLine($"{Name} used {consumable.Name} and healed for {consumable.Value} HP");
                        Console.WriteLine($"{Name} now has {Health} HP");
                        ConsumableInventory.Remove(consumable);
                        break;
                    }
                case ConsumableType.Mana:
                    {
                        Mana += consumable.Value;
                        Console.WriteLine($"{Name} used {consumable.Name} and restored  {consumable.Value} MP");
                        Console.WriteLine($"{Name} now has {Mana} MP");
                        ConsumableInventory.Remove(consumable);
                        break;
                    }
                case ConsumableType.Buff:
                    {
                        Console.WriteLine("BUFF NOT IMPLEMENTED YET");
                        ConsumableInventory.Remove(consumable);
                        break;
                    }
            }
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