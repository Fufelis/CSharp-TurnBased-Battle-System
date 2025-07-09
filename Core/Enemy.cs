namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Enemy : Character, ICombatant
    {
        public Enemy(string name, int health, int attackPower, int spellPower, int mana, int defense, int speed, bool isPlayer) : base(name, health, attackPower, spellPower, mana, defense, speed, isPlayer)
        {
        }
    }
}