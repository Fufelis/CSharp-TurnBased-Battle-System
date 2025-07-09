namespace RPG_Turn_Based_Battle_System.Items.Consumables
{
    public enum ConsumableType
    {
        Health,
        Mana,
        Buff,
    }

    internal abstract class Consumable
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public ConsumableType Type { get; set; }
    }
}