using MonsterArena.GameCharacters;

namespace MonsterArena.Inventory.SpecialItems
{
    public class Potion : Item
    {
        public Potion(string name) : base(name)
        {
        }

        public override void UseItem(Player player)
        {
            player.IncreaseHealth(15);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You used a Potion and healed 15 HP!");
            Console.ResetColor();
        }
    }
}
