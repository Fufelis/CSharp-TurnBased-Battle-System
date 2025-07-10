using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Items.Equipment
{
    public enum ArmorType
    {
        Light,
        Medium,
        Heavy,
    }
    internal class Armor:Equipment
    {
        public int DefenseBonus { get; set; }
        public ArmorType Type { get; set; }

        public Armor(string name, int defenseBonus,ArmorType type,EquipmentSlot slot) { 

            Name = name;
            DefenseBonus = defenseBonus;
            Type = type;
            Slot = slot;
        }
    }
}
