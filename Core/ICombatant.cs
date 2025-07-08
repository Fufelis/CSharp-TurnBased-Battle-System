using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal interface ICombatant
    {

        string Name { get; }
        int Health { get; }
        int AttackPower { get; }
        int SpellPower {  get; }
        int Defense { get; }
        bool IsAlive { get; }
        int MaxHealth { get; }
        int Speed { get; }

        void Attack(ICombatant target);
        void Spell(ICombatant target);
        void TakeDamage(int amount);
    }
}
