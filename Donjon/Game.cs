using System;

namespace Donjon
{
    internal class Game
    {
        private Map map;
        private Hero hero;

        public Game(int width, int height)
        {
            this.map = new Map(width, height);
        }

        internal void Run()
        {
            // init game
            this.hero = new Hero(health: 100);
            bool printDebugInfo = false;
            PopulateMap();

            // game loop
            do
            {
                Console.Clear();
                if (printDebugInfo)
                    PrintDebugInfo();

                PrintMap();
                PrintStatus();

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
                        hero.X = 0;
                        hero.Y = 0;
                        break;
                    case ConsoleKey.F5:
                        printDebugInfo = !printDebugInfo;
                        break;
                }

            } while (true);

            // game over
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
                    if (this.hero.X == x && this.hero.Y == y)
                        Console.Write("H");
                    else if (cell.Monster != null)
                    {
                        Console.ForegroundColor = cell.Monster.Color;
                        Console.Write(cell.Monster.MapSymbol);
                        Console.ResetColor();
                    }
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void PrintDebugInfo()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debug info: ");

            Console.WriteLine(hero);
            Console.WriteLine();

            Console.ResetColor();
        }
    }
}