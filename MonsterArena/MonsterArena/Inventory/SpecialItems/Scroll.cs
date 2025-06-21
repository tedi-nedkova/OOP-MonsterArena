using MonsterArena.GameCharacters;

namespace MonsterArena.Inventory.SpecialItems
{
    public class Scroll : Item
    {
        public Scroll(string name) : base("BuffScroll")
        {
        }

        public override void UseItem(Player player)
        {
            player.IncreaseHealth(5);
            player.IncreaseAttackPoints(5);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You used a Potion and healed 15 HP!");
            Console.ResetColor();
        }
    }
}
