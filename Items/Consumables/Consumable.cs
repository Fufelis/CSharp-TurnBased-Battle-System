namespace RPG_Turn_Based_Battle_System.Items.Consumables
{
    public enum ConsumableType
    {
        Health,
        Mana,
        Buff,
    }

    internal abstract class Consumable : Item
    {
        public int Value { get; set; }
        public ConsumableType Type { get; set; }
    }
}