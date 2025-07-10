namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Enemy : Character, ICombatant
    {
        public Enemy(string name, int health, int baseAttackPower, int baseSpellPower, int mana, int defense, int speed, bool isPlayer) : base(name, health, baseAttackPower, baseSpellPower, mana, defense, speed, isPlayer)
        {

        }
    }
}