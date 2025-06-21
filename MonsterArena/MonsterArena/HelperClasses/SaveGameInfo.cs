using MonsterArena.GameCharacters;
using MonsterArena.Inventory.SpecialItems;
using System.Runtime.CompilerServices;

namespace MonsterArena.HelperClasses
{
    public class SaveGameInfo
    {
        private static readonly string SaveDir = "../../../SavedInfo";
        private static readonly string SaveFile = "../../../SavedInfo/savedPlayers.txt";

        public static void Save(Player player)
        {
            try
            {
                if (!Directory.Exists(SaveDir))
                {
                    Directory.CreateDirectory(SaveDir);
                }

                using (StreamWriter writer = new StreamWriter("../../../SavedInfo/savedPlayers.txt"))
                {
                    writer.WriteLine($"Name={player.Name}");
                    writer.WriteLine($"Health={player.Health}");
                    writer.WriteLine($"Level={player.Level}");
                    writer.WriteLine($"AttackPower={player.AttackPower}");
                    writer.WriteLine($"XP={player.Xp}");
                    writer.WriteLine($"Inventory={string.Join(",", player.Inventory.Items.Select(i => i.Name))}");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game saved successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error saving game: {ex.Message}");
                Console.ResetColor();
            }
        }


        public static Player Load()
        {
            try
            {
                if (!File.Exists("../../../SavedInfo/savedPlayers.txt"))
                {
                    Console.WriteLine("No save file found.");

                    return null;
                }

                string[] lines = File.ReadAllLines(SaveFile);

                string name = lines.FirstOrDefault(l => l.StartsWith("Name="))?.Split('=')[1] ?? string.Empty;

                double health = int.Parse(lines.FirstOrDefault(l => l.StartsWith("Health="))?.Split('=')[1] ?? "100");

                int level = int.Parse(lines.FirstOrDefault(l => l.StartsWith("Level="))?.Split('=')[1] ?? "1");

                double attackPower = int.Parse(lines.FirstOrDefault(l => l.StartsWith("AttackPower="))?.Split('=')[1]);

                int xp = int.Parse(lines.FirstOrDefault(l => l.StartsWith("XP="))?.Split('=')[1] ?? "0");

                bool isAlive = false;

                string inventoryLine = lines.FirstOrDefault(l => l.StartsWith("Inventory="))?.Split('=')[1] ?? string.Empty;
                List<string> inventoryItems = inventoryLine.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                if (health > 0)
                {
                    isAlive = true;
                }

                Player player = new Player(name, health, attackPower, level, isAlive, xp);

                foreach (var inventoryItem in inventoryItems)
                {
                    Item item = null;

                    if (inventoryItem == "Potion")
                    {
                        item = new Potion("HealingPotion");
                    }
                    else if (inventoryItem == "Elixir")
                    {
                        item = new Elixir("AttackElixir");
                    }
                    else
                    {
                        item = null;
                    }

                    if (item != null)
                    {
                        player.Inventory.Add(item);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Game loaded successfully!");
                Console.ResetColor();

                return player;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            return null;
        }
    }
}
