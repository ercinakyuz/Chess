using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessGame.Business.Piece
{
    struct RookLocationKey
    {
        public PlayerType OwnerShip { get; set; }
        public int XDirection { get; set; }
    }

    public class King : BasePiece
    {
        public bool IsChecked { get; set; }
        public HashSet<Block> DangerZones { get; set; } = new HashSet<Block>();

        private readonly Dictionary<RookLocationKey, Block> _rookLocationDictionary = new Dictionary<RookLocationKey, Block>
        {
            {new RookLocationKey{OwnerShip = PlayerType.White,XDirection = 1 }, new Block{ X = 8, Y = 1 } },
            {new RookLocationKey{OwnerShip = PlayerType.Black,XDirection = 1 }, new Block{ X = 8, Y = 8 } },
            {new RookLocationKey{OwnerShip = PlayerType.Black,XDirection = -1 }, new Block{ X = 1, Y = 8 } },
            {new RookLocationKey{OwnerShip = PlayerType.White,XDirection = -1 }, new Block{ X = 1, Y = 1 } },
        };

        public override bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false)
        {
            int xDist = move.To.X - move.From.X;
            int yDist = move.To.Y - move.From.Y;
            int xAbsDist = Math.Abs(xDist);
            int yAbsDist = Math.Abs(yDist);
            bool isValidMove = base.IsValidMove(move, board, isDangerZonesControl) && !DangerZones.Contains(move.To)
                && ((xAbsDist == 1 && yAbsDist == 1) || (xAbsDist == 0 && yAbsDist == 1) || (xAbsDist == 1 && yAbsDist == 0));
            if (xAbsDist == 2 && yAbsDist == 0 && !IsChecked && IsFirstMove && !isDangerZonesControl)
            {
                int xDirection = Math.Sign(xDist);
                for (int i = 1; i <= 2; i++)
                {
                    var moveBlock = new Block { X = move.From.X + xDirection * i, Y = move.To.Y };
                    isValidMove = board[moveBlock] == null && !DangerZones.Contains(moveBlock);
                    if (!isValidMove)
                    {
                        break;
                    }
                }

                #region RookControl
                if (isValidMove)
                {
                    var rookLocation = _rookLocationDictionary[new RookLocationKey { OwnerShip = Ownership, XDirection = xDirection }];

                    var piece = board[rookLocation];
                    if (piece.GetType() == typeof(Rook))
                    {
                        var rook = (Rook)piece;
                        if (rook.IsFirstMove)
                        {
                            rook.IsFirstMove = false;
                            var newRookLocation = rookLocation;
                            newRookLocation.X = move.To.X - xDirection;
                            board[rookLocation] = null;
                            board[newRookLocation] = rook;
                        }
                        else
                        {
                            isValidMove = false;
                        }
                    }
                }
                #endregion
            }

            return isValidMove;
        }

        public override object Clone()
        {
            var king = (King)base.Clone();
            king.IsChecked = IsChecked;
            //foreach (var zone in king.DangerZones)
            //{
            //    zone.X
            //}
            king.DangerZones = DangerZones;
            return king;
        }
    }
}
