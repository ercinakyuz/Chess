using System.Collections.Generic;
using ChessGame.Business;
using ChessGame.Business.Piece;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessGame.Test
{
    [TestClass]
    public class PieceTest
    {
        private readonly Game _game;

        private readonly Board _board;

        public PieceTest()
        {
            _game = new Game();
            _board = _game.Board;
        }
        [TestMethod]
        public void TestRookMoveIsValid()
        {
            foreach (var piece in _board.Current.Values)
            {
                if (piece.GetType() == typeof(Rook))
                {
                    var rook = (Rook)piece;
                    var from = _board.FindPieceLocation(rook);
                    foreach (var to in _board.Current.Keys)
                    {
                        var move = new Move { From = from, To = to };
                        Assert.IsTrue(rook.IsValidMove(move, _board.Current));
                    }
                }
            }
        }

        [TestMethod]
        public void TestUpgradePawn()
        {
            var currentBoard = _board.Current;
            var from = new Block { Y = 1, X = 8 };
            var to = new Block { Y = 8, X = 1 };
            var pawn = currentBoard[from];
            _game.UpgradePawn(pawn, to);
            Assert.IsFalse(currentBoard[to] == null);
            Assert.IsFalse(currentBoard[to].GetType() == typeof(King));
            Assert.IsFalse(currentBoard[to].GetType() == typeof(Pawn));
        }

        [TestMethod]
        public void AreaOfEffectsTest()
        {
            var areaOfEffectsCases = new HashSet<Block>
            {
                new() { X = 3, Y = 5 },
                new() { X = 1, Y = 5 },
            };
            var move = new Move
            {
                From = new Block { X = 2, Y = 2 },
                To = new Block { X = 2, Y = 4 }
            };

            var piece = _board.FindPiece(move.To);
            _game.Play(move, out var gameOverControl);

            foreach (var areaOfEffectCase in areaOfEffectsCases)
            {
                Assert.IsTrue(_game.WhitePlayer.AreaOfEffects.Contains(areaOfEffectCase));
            }

        }
    }
}
