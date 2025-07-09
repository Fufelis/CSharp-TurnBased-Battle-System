using RPG_Turn_Based_Battle_System.Core;

namespace RPG_Turn_Based_Battle_System.Spells
{
    internal class Fireball : Ability
    {
        public Fireball()
        {
            Name = "Fireball";
            ManaCost = 10;
            CastOnAlly = false;
        }

        public override void Use(Character caster, Character target)
        {
            int damage = 5 + caster.SpellPower;
            target.TakeDamage(damage);
            caster.Mana -= ManaCost;
            Console.WriteLine($"{caster.Name} casts {Name} on {target.Name} and dealt {damage} damage");
        }
    }
}