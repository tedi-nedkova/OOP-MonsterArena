using MonsterArena.GameCharacters;
using MonsterArena.Inventory.SpecialItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterArena.HelperClasses
{
    internal class GameManager
    {
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;
        private GameManager() { }

        private Player player;

        public void StartBattle()
        {
            bool gameRunning = true;

            while (gameRunning && player.IsAlive)
            {
                Monster monster = MonsterFactory.CreateMonster();

                Console.WriteLine($"A wild {monster.Name} appears!");

                int turn = 1;

                while (player.IsAlive && monster.IsAlive)
                {
                    Console.WriteLine($" ------ Turn {turn} ------ ");
                    Console.WriteLine(" --------------------------");
                    Console.WriteLine(" | 1. Attack              |");
                    Console.WriteLine(" | 2. Defend              |");
                    Console.WriteLine(" | 3. Use item            |");
                    Console.WriteLine(" | 4. View Stats          |");
                    Console.WriteLine(" | 5. Save and Exit       |");
                    Console.WriteLine(" | 6. Exit without Saving |");
                    Console.WriteLine(" --------------------------");

                    Console.WriteLine(" Choose an action:      ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            player.Attack(monster);
                            break;
                        case "2":
                            player.Defend(monster.AttackPower);
                            break;
                        case "3":
                            UseItem();
                            continue;
                        case "4":
                            ShowStats();
                            continue;
                        case "5":
                            SaveGameInfo.Save(player);

                            Console.WriteLine("Game saved. Exiting...");

                            gameRunning = false;
                            return;
                        case "6":
                            Console.WriteLine("Exiting without saving...");

                            gameRunning = false;
                            return;
                        default:
                            Console.WriteLine("Invalid input.");
                            continue;
                    }

                    if (monster.IsAlive)
                    {
                        monster.Attack(player);
                    }

                    turn++;
                }

                if (!player.IsAlive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have been defeated.");
                    Console.ResetColor();
                    gameRunning = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You defeated the {monster.Name}!");
                    Console.ResetColor();
                    player.GainXP(monster.Level * 50);

                    Random random1 = new Random();
                    int dropChance = random1.Next(1, 101);

                    if (dropChance > 60)
                    {
                        Item item = null;
                        if (dropChance <= 75)
                        {
                            item = new Potion("HealingPotion");
                        }
                        else if (dropChance > 75 && dropChance <= 85)
                        {
                            item = new Elixir("BuffElixir");
                        }
                        else if (dropChance > 85)
                        {
                            item = new Elixir("Scroll");
                        }
                        player.Inventory.Add(item);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"You found a {item.Name}!");
                        Console.ResetColor();
                    }

                    SaveGameInfo.Save(player);
                }
            }
        }

        public void Menu()
        {
            Console.WriteLine("----- Monster Arena -----");
            Console.WriteLine("|     1.Start New Game  |");
            Console.WriteLine("|     2.Load Game       |");
            Console.WriteLine("|     3.Exit Game       |");
            Console.WriteLine("-------------------------");

            Console.WriteLine();

            Console.WriteLine("Choose:");
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine();

            if (input == 1)
            {
                StartNewGame();
            }
            else if (input == 2)
            {
                LoadGame();
            }
            else if (input == 3)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private void StartNewGame()
        {
            Console.Clear();
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            player = new Player(name, 200, 60, 0, true, 0);

            player.OnLevelUp += () =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU LEVELED UP!");
                Console.ResetColor();

                player.UpPowerOnLevelUp();
                player.UpHealthOnLevelUp();
            };

            Console.WriteLine($"Welcome, {player.Name}!");

            StartBattle();
        }

        private void LoadGame()
        {
            player = SaveGameInfo.Load();

            if (player != null)
            {
                Console.WriteLine($"Welcome back, {player.Name}!");
                player.OnLevelUp += () =>
                {
                    Console.WriteLine($"{player.Name} has leveled up!");
                };

                StartBattle();
            }
        }

        private void UseItem()
        {
            var items = player.Inventory.Items
                .ToList();

            if (items.Count == 0)
            {
                Console.WriteLine("No usable items found.");
                return;
            }

            Console.WriteLine("Select an item to use:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name}");
            }

            Console.Write("Enter number: ");
            int selectedNumber = int.Parse(Console.ReadLine());

            if (selectedNumber != 1 && selectedNumber != 2 && selectedNumber != 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid selection.");
                Console.ResetColor();
            }
            else
            {
                var selected = items[selectedNumber - 1];

                selected.UseItem(player);

                player.Inventory.Remove(selected);
            }
        }

        private void ShowStats()
        {
            Console.WriteLine($"\nPlayer: {player.Name}");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Attack: {player.AttackPower}");
            Console.WriteLine($"Level: {player.Level}");

            Console.WriteLine("Inventory:");
            foreach (var item in player.Inventory.Items)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }
    }
}
