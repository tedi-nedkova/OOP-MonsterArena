using MonsterArena.GameCharacters;

namespace MonsterArena.Inventory.SpecialItems
{
    public class Elixir : Item
    {
        public Elixir(string name) : base(name)
        {
        }

        public override void UseItem(Player player)
        {
            player.IncreaseAttackPoints(10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You used a Elixir and added 10 points to your attack power!");
            Console.ResetColor();
        }
    }
}
