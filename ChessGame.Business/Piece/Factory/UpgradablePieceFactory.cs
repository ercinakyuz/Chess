using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ChessGame.Business.Piece.Factory
{
    public class UpgradablePieceFactory
    {
        private static readonly IReadOnlyDictionary<UpgradablePieceType, Func<BasePiece>> UpgradablePieces = new ConcurrentDictionary<UpgradablePieceType, Func<BasePiece>>
        {
            [UpgradablePieceType.Bishop] = () => new Bishop(),
            [UpgradablePieceType.Knight] = () => new Knight(),
            [UpgradablePieceType.Rook] = () => new Rook(),
            [UpgradablePieceType.Queen] = () => new Queen(),
        };
        public static BasePiece Create(string chosenUpgradeText)
        {
            if (int.TryParse(chosenUpgradeText, out int upgradeablePieceTypeIndex)
                && UpgradablePieces.TryGetValue((UpgradablePieceType)upgradeablePieceTypeIndex, out var pieceGenerator))
                return pieceGenerator.Invoke();
            throw new ArgumentException($"{upgradeablePieceTypeIndex} is not an upgradable piece");
        }
    }
}
