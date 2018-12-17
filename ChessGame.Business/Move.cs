namespace ChessGame.Business
{
    public class Move
    {
        public Block From { get; set; }
        public Block To { get; set; }

        public override bool Equals(object obj)
        {
            return Equals((Move)obj);
        }

        protected bool Equals(Move other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode() * 397) ^ To.GetHashCode();
            }
        }
    }
}
