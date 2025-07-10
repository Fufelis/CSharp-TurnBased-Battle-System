using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Items.Equipment
{
    public enum WeaponType
    {
        Sword,
        Bow,
        Wand,
    }
    internal class Weapon : Equipment
    {
        public int AttackBonus { get; set; }

        public WeaponType WeaponType { get; set; }

        public Weapon(string name,int attackBonus,WeaponType type) {
        
            Name = name;
            AttackBonus = attackBonus;
            WeaponType = type;
            Slot = EquipmentSlot.Weapon;
        }
    }
}
