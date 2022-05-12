using System;
namespace Assets.Scripts.Model
{
    public class Vector2Integer: IEquatable<Vector2Integer>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Integer(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Vector2Integer other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", this.X, this.Y);
        }
    }
}