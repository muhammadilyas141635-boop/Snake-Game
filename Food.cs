using System.Drawing;

namespace Snakegame
{
    internal class Food
    {
        public Point Position { get; set; }

        public Food(Point position)
        {
            Position = position;
        }

        public virtual int GetPoints()
        {
            return 10;
        }

        public virtual int GetGrowth()
        {
            return 1;
        }

        public virtual int Size
        {
            get { return 10; }
        }

        public virtual Color GetColor()
        {
            return Color.Yellow;
        }
    }
}