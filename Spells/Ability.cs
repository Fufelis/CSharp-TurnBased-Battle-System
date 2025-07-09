using RPG_Turn_Based_Battle_System.Core;

namespace RPG_Turn_Based_Battle_System.Spells
{
    internal abstract class Ability
    {
        public bool CastOnAlly { get; set; }
        public string Name { get; set; }
        public int ManaCost { get; set; }

        public abstract void Use(Character caster, Character target);
    }
}