using System;
using System.Collections.Generic;

namespace ChessGame.Business.Piece
{
    public abstract class BasePiece<TPiece> where TPiece : BasePiece, new()
    {
        public Guid Id { get; set; }
        public PlayerType Ownership { get; set; }
        public bool IsFirstMove { get; set; }

        public static TPiece Create()
        {
            return new TPiece();
        }
        public virtual bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false, bool isGameOverControl = false)
        {
            return isDangerZonesControl || board[move.To]?.Ownership != Ownership;
        }

        public virtual object Clone()
        {
            var piece = (BasePiece)MemberwiseClone();
            piece.IsFirstMove = IsFirstMove;
            piece.Ownership = Ownership;
            piece.Id = Id;
            return piece;
        }

        public override bool Equals(object obj)
        {
            return Equals((BasePiece)obj);
        }

        protected bool Equals(BasePiece other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            var hashCode = Id.GetHashCode();
            return hashCode;
        }
    }
    public abstract class BasePiece : ICloneable
    {
        public Guid Id { get; set; }
        public PlayerType Ownership { get; set; }
        public bool IsFirstMove { get; set; }

        public virtual bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false, bool isGameOverControl = false)
        {
            return isDangerZonesControl || board[move.To]?.Ownership != Ownership;
        }

        public virtual object Clone()
        {
            var piece = (BasePiece)MemberwiseClone();
            piece.IsFirstMove = IsFirstMove;
            piece.Ownership = Ownership;
            piece.Id = Id;
            return piece;
        }

        public override bool Equals(object obj)
        {
            return Equals((BasePiece)obj);
        }

        protected bool Equals(BasePiece other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            var hashCode = Id.GetHashCode();
            return hashCode;
        }
    }
}
