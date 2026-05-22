using System.Drawing;

namespace ProjectShip
{
    public class EntityShip
    {
        public int Speed { get; private set; }
        public double Weight { get; private set; }
        public Color BodyColor { get; private set; }
        public int DecksCount { get; private set; }

        public double Step => Speed * 100 / Weight;

        public void Init(int speed, double weight, Color bodyColor, int decksCount)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
            DecksCount = decksCount;
        }
    }
}