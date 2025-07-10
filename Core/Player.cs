// Ignore Spelling: mana

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class Player : Character, ICombatant
    {
        public Player(string name, int health, int baseAttackPower, int baseSpellPower, int mana, int defense, int speed, bool isPlayer):base(name,health, baseAttackPower, baseSpellPower, mana, defense, speed,isPlayer)
        {

        }
    }
}