namespace Donjon
{
    internal class Hero
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Health { get; set; }
        public int Damage { get; } = 10;

        public Hero(int health)
        {
            this.Health = health;
        }

        public override string ToString()
        {
            return $"Hero: Health({Health}), Position({X}, {Y})";
        }
    }
}