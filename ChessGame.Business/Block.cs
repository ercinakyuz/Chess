using System;

namespace ChessGame.Business
{
    public struct Block : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public object Clone()
        {
            var block = (Block)MemberwiseClone();
            block.X = X;
            block.Y = Y;
            return block;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((Block)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        private bool Equals(Block other)
        {
            return X == other.X && Y == other.Y;
        }

        public override string ToString()
        {
            return $"{X}.{Y}";
        }
    }
}
