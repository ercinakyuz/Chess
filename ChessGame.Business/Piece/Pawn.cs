using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Business.Piece.Factory;

namespace ChessGame.Business.Piece
{
    struct DistanceRangeKey
    {
        public PlayerType OwnerShip { get; set; }
        public bool IsFirstMove { get; set; }
    }
    public class Pawn : BasePiece
    {
        public Pawn AvailablePawnForPassingToKill { get; set; }
        public bool IsKillableMove { get; set; }

        private static readonly Dictionary<DistanceRangeKey, int[]> DistanceRangeDictionary = new Dictionary<DistanceRangeKey, int[]>
        {
            {new DistanceRangeKey{OwnerShip = PlayerType.Black,IsFirstMove = true }, new[]{-2,-1} },
            {new DistanceRangeKey{OwnerShip = PlayerType.Black,IsFirstMove = false }, new[]{-1} },
            {new DistanceRangeKey{OwnerShip = PlayerType.White,IsFirstMove = true }, new[]{2,1} },
            {new DistanceRangeKey{OwnerShip = PlayerType.White,IsFirstMove = false }, new[]{1} },
        };
        private static readonly UpgradablePieceType[] UpgradablePieceTypes = Enum.GetValues(typeof(UpgradablePieceType)).Cast<UpgradablePieceType>().ToArray();

        public void Upgrade(Block to, Dictionary<Block, BasePiece> board)
        {
            if (!IsUpgradeAvailable(to))
                return;
            Console.WriteLine("Which to upgrade?");
            Array.ForEach(UpgradablePieceTypes, type => Console.WriteLine($"{(int)type}.{type}"));
            var upgradedPiece = UpgradablePieceFactory.Create(Console.ReadLine());
            upgradedPiece.Ownership = Ownership;
            board[to] = upgradedPiece;
        }
        private bool IsUpgradeAvailable(Block to)
        {
            bool isUpgraded = false;
            if (Ownership == PlayerType.White && to.Y == 8)
            {
                isUpgraded = true;
            }
            else if (Ownership == PlayerType.Black && to.Y == 1)
            {
                isUpgraded = true;
            }

            return isUpgraded;
        }

        public override bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false, bool isGameOverControl = false)
        {
            IsKillableMove = false;
            bool isValidMove = false;
            int xDist = move.To.X - move.From.X;
            int yDist = move.To.Y - move.From.Y;
            int xAbsDist = Math.Abs(xDist);
            int yAbsDist = Math.Abs(yDist);
            var yDirection = Ownership == PlayerType.White ? 1 : -1;

            if (xAbsDist == 1 && yDist == yDirection)
            {
                IsKillableMove = true;
                if (board[move.To] != null && board[move.To].Ownership != Ownership)
                {
                    isValidMove = true;
                }
                else if (AvailablePawnForPassingToKill != null && board[move.To] == null)
                {
                    var backLocation = new Block { X = move.To.X, Y = move.To.Y - yDirection };
                    var backLocationPiece = board[backLocation];
                    if (Equals(backLocationPiece, AvailablePawnForPassingToKill))
                    {
                        isValidMove = true;
                        if (!isDangerZonesControl)
                        {
                            board[backLocation] = null;
                            if (!isGameOverControl)
                            {
                                Console.WriteLine($"{Ownership} pawn killed {backLocationPiece.Ownership} pawn when passing!");
                            }
                        }
                    }
                }
            }
            else if (xDist == 0 && DistanceRangeDictionary[new DistanceRangeKey { IsFirstMove = IsFirstMove, OwnerShip = Ownership }].Contains(yDist))
            {
                for (int i = 1; i <= yAbsDist; i++)
                {
                    var pieceOnRotation = board[new Block { X = move.To.X, Y = move.From.Y + i * yDirection }];
                    if (pieceOnRotation != null)
                    {
                        return false;
                    }
                }
                isValidMove = true;
                if (IsFirstMove)
                {
                    var nearBlocks = new List<Block>
                    {
                        new Block { X = move.To.X + 1, Y = move.To.Y },
                        new Block { X = move.To.X - 1, Y = move.To.Y }
                    };

                    foreach (var nearBlock in nearBlocks)
                    {
                        if (board.ContainsKey(nearBlock) && board[nearBlock]?.GetType() == typeof(Pawn) && !isDangerZonesControl)
                        {
                            ((Pawn)board[nearBlock]).AvailablePawnForPassingToKill = this;
                        }
                    }
                }
            }

            if (isValidMove && !isDangerZonesControl)
            {
                AvailablePawnForPassingToKill = null;
            }
            return isValidMove;
        }

        public override object Clone()
        {
            var pawn = (Pawn)base.Clone();
            pawn.IsKillableMove = IsKillableMove;
            pawn.AvailablePawnForPassingToKill = (Pawn)AvailablePawnForPassingToKill?.Clone();
            return pawn;
        }
    }
}
