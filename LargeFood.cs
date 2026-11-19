using System.Drawing;

namespace Snakegame
{
    internal class LargeFood : Food
    {
        public LargeFood(Point position)
            : base(position)
        {
        }

        public override int GetPoints()
        {
            return 20;
        }

        public override int GetGrowth()
        {
            return 2;
        }

        public override int Size
        {
            get { return 20; }
        }

        public override Color GetColor()
        {
            return Color.Red;
        }
    }
}