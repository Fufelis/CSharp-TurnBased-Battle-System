using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Items.Equipment
{
    internal abstract class Equipment :Item
    {
       public EquipmentSlot Slot { get; set; }
    }
}
