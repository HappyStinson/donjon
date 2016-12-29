using System;

namespace Donjon
{
    public abstract class Monster
    {
        public int Health { get; set; }
        public string MapSymbol { get; }
        public string Name { get; }
        public ConsoleColor Color { get; protected set; } = ConsoleColor.Red;

        protected int Damage = 20;

        public Monster(string mapSymbol, string name, int health)
        {
            MapSymbol = mapSymbol;
            Name = name;
            Health = health;
        }
    }

    public class Goblin : Monster
    {
        public Goblin() : base("G", "goblin", 20)
        {
            Color = ConsoleColor.Green;
        }
    }

    public class Orc : Monster
    {
        public Orc() : base("O", "orc", 40)
        {
            Damage = 40;
        }
    }
}