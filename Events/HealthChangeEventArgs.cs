using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Events
{
    internal class HealthChangeEventArgs :EventArgs
    {
        public int OldHealth { get; }
        public int NewHealth { get; }

        public HealthChangeEventArgs(int oldHealth, int newHealth)
        {
            OldHealth = oldHealth;
            NewHealth = newHealth;
        }
    }
}
