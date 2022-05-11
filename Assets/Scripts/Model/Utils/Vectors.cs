using System;
namespace Assets.Scripts.Model
{
    public class Vector2Integer: IEquatable<Vector2Integer>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Vector2Integer other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}