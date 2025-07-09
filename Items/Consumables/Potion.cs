namespace RPG_Turn_Based_Battle_System.Items.Consumables
{
    internal class Potion : Consumable
    {
        public Potion(string name, int value, ConsumableType type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}