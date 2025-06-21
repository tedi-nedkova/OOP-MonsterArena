using MonsterArena.GameCharacters;

namespace MonsterArena.Inventory.SpecialItems
{
    public abstract class Item
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Item(string name)
        {
            Name = name;
        }

        public abstract void UseItem(Player player);
    }
}
