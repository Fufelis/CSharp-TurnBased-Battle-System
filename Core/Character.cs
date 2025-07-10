// Ignore Spelling: Unequip Mana

using RPG_Turn_Based_Battle_System.Items.Consumables;
using RPG_Turn_Based_Battle_System.Items.Equipment;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal abstract class Character : ICombatant
    {
        private int health;
        private int mana;
        private List<Ability> abilities = new List<Ability>();
        private List<Consumable> consumableInventory = new List<Consumable>();
        private const int CONSUMABLE_INVENTORY_SIZE = 5;
        private List<Equipment> equipmentInventory = new List<Equipment>();
        private const int EQUIPMENT_INVENTORY_SIZE = 32;
        public Dictionary<EquipmentSlot, Equipment> EquippedGear { get; private set; } = new Dictionary<EquipmentSlot, Equipment>();

        public List<Ability> Abilities
        { get { return abilities; } }

        public List<Equipment> EquipmentInventory
        { get { return equipmentInventory; } }

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

        public int BaseAttackPower { get; set; }
        public int BaseSpellPower { get; set; }
        public int AttackPower { get; private set; } = 0;
        public int SpellPower { get; private set; } = 0;
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

        public int Defense { get; private set; }
        public int BaseDefense { get; set; }
        public int Speed { get; set; }

        public bool IsAlive
        { get { return Health >= 1; } }

        public Character(string name, int health, int baseAttackPower, int baseSpellPower, int mana, int baseDefense, int speed, bool isPlayer)
        {
            Name = name;

            MaxHealth = health;
            Health = health;
            MaxMana = mana;
            Mana = mana;
            BaseAttackPower = baseAttackPower;
            BaseSpellPower = baseSpellPower;
            BaseDefense = baseDefense;
            Speed = speed;
            IsPlayer = isPlayer;
            RecalculateStats();
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

        public void EquipItem(Equipment item)
        {
            if (EquipmentInventory.Contains(item))
            {
                if (EquippedGear.TryGetValue(item.Slot, out Equipment oldEquippedItem))
                {
                    oldEquippedItem = EquippedGear[item.Slot];
                    EquippedGear.Remove(item.Slot);
                    EquipmentInventory.Add(oldEquippedItem);
                    EquippedGear.Add(item.Slot, item);
                    EquipmentInventory.Remove(item);
                    RecalculateStats();
                    Console.WriteLine($"{Name} has equipped {item.Name}");
                }
                else
                {
                    EquippedGear.Add(item.Slot, item);
                    EquipmentInventory.Remove(item);
                    RecalculateStats();
                    Console.WriteLine($"{Name} has equipped {item.Name}");
                }
            }
            else
            {
                Console.WriteLine("No Item to equip");
            }
        }

        public void UnequipItem(Equipment Item)
        {
            if (EquippedGear.ContainsKey(Item.Slot))
            {
                EquippedGear.TryGetValue(Item.Slot, out Equipment oldEquippedItem);
                if (oldEquippedItem != null)
                {
                    EquippedGear.Remove(oldEquippedItem.Slot);
                    EquipmentInventory.Add(oldEquippedItem);
                    Console.WriteLine($"{Name} has unequiped {oldEquippedItem.Name} from slot {oldEquippedItem.Slot}");
                    RecalculateStats() ;
                }
            }
            else
            {
                Console.WriteLine("No item to unequip");
            }
        }

        public void RecalculateStats()
        {
            AttackPower = BaseAttackPower;
            SpellPower = BaseSpellPower;
            Defense = BaseDefense;
            if (EquippedGear.Count > 0)
            {
                foreach (var item in EquippedGear)
                {
                    if (item.Value is Weapon weapon)
                    {
                        if (weapon.WeaponType == WeaponType.Wand)
                        {
                            SpellPower += weapon.AttackBonus;
                        }
                        else
                        {
                            AttackPower += weapon.AttackBonus;
                        }
                    }
                    if (item.Value is Armor armor)
                    {
                        Defense += armor.DefenseBonus;
                    }
                }
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"---{Name}`s Info---");
            Console.WriteLine($"Health : {Health} / {MaxHealth}");
            Console.WriteLine($"Attack : {AttackPower} Spell Power : {SpellPower} ");
            Console.WriteLine($"Defense : {Defense}");

            Console.WriteLine("=== INVENTORY ===");
            foreach (Equipment item in EquipmentInventory)
            {
                Console.WriteLine($"{item.Name} , {item.Slot}");
            }

            Console.WriteLine("=================");
        }

        public void DisplayEquippedGear()
        {
            foreach (var item in EquippedGear)
            {
                Console.WriteLine($"{item.Value.Name} in {item.Key} slot");
            }
        }
    }
}