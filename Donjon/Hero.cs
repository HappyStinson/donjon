using System;

namespace Donjon
{
    internal class Hero : Creature
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Health { get; set; }
        public int Damage { get; } = 10;

        public Hero(int health) : base("H")
        {
            this.Health = health;
            Color = System.ConsoleColor.White;
        }

        internal string Fight(Monster monster)
        {
            monster.Health -= Damage;
            if (monster.Health > 0)
                return $"You attacked the {monster.Name} for {Damage} hp damage.";
            else
                return $"You slayed the {monster.Name}!";
        }

        public override string ToString()
        {
            return $"Hero: Health({Health}), Position({X}, {Y})";
        }
    }
}