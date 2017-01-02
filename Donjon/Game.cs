using System;

namespace Donjon
{
    internal class Game
    {
        private Map map;
        private Hero hero;
        private string log = "";

        public Game(int width, int height)
        {
            this.map = new Map(width, height);
        }

        internal void Run()
        {
            // init game
            this.hero = new Hero(health: 100);
            PopulateMap();

            // game loop
            do
            {
                Console.Clear();
                PrintStatus();
                PrintMap();
                PrintLog();
                PrintVisible();

                Console.WriteLine("What do you do?");
                ConsoleKey key = GetInput();

                // process actions
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (hero.X > 0) hero.X--;
                        break;
                    case ConsoleKey.UpArrow:
                        if (hero.Y > 0) hero.Y--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (hero.X < map.Width - 1) hero.X++;
                        break;
                    case ConsoleKey.DownArrow:
                        if (hero.Y < map.Height - 1) hero.Y++;
                        break;
                    case ConsoleKey.Spacebar:
                        var monster = map.Cells[hero.X, hero.Y].Monster;
                        if (monster != null) Fight(monster);
                        break;
                    case ConsoleKey.F5:
                        Console.WriteLine("Debug info: " + hero);
                        break;
                }

            } while (true);

            // game over
        }

        private void PrintLog()
        {
            Console.WriteLine(log);
            log = "";
        }

        private void Fight(Monster monster)
        {
            Log(hero.Fight(monster));

            if (monster.Health > 0)
                Log(monster.Fight(hero));
        }

        private void Log(string message)
        {
            log += message + "\n";
        }

        private void PrintVisible()
        {
            var cell = map.Cells[hero.X, hero.Y];
            var monster = cell.Monster;
            if (monster != null)
            {
                Console.WriteLine();
                Console.WriteLine($"You see a {monster.Name} ({monster.Health} hp)");
            }
        }

        private void PopulateMap()
        {
            map.Cells[8, 4].Monster = new Goblin();
            map.Cells[4, 8].Monster = new Goblin();
            map.Cells[6, 3].Monster = new Orc();
            map.Cells[10, 7].Monster = new Orc();
        }

        private void PrintStatus()
        {
            Console.WriteLine($"\nHealth: {hero.Health} hp");
        }

        private ConsoleKey GetInput()
        {
            Console.WriteLine("Press a key");
            return Console.ReadKey(intercept: true).Key;
        }

        private void PrintMap()
        {
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    var cell = map.Cells[x, y];
                    Console.Write(" ");

                    Creature creature = null;
                    if (this.hero.X == x && this.hero.Y == y)
                    {
                        creature = hero;
                    }
                    else if (cell.Monster != null)
                    {
                        creature = cell.Monster;
                    }
                    else
                    {
                        Console.Write(".");
                    }

                    if (creature != null)
                    {
                        Console.ForegroundColor = creature.Color;
                        Console.Write(creature.MapSymbol);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }
    }
}