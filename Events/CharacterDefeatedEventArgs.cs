using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Core;

namespace RPG_Turn_Based_Battle_System.Events
{
    internal class CharacterDefeatedEventArgs : EventArgs
    {
        public ICombatant DefeatedCharacter { get;}

        public CharacterDefeatedEventArgs(ICombatant defeatedCharacter)
        {
            DefeatedCharacter = defeatedCharacter;
        }
    }
}
