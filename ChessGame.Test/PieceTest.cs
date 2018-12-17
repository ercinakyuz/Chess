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
        public PieceTest()
        {
            _game = new Game();
        }
        [TestMethod]
        public void TestRookMoveIsValid()
        {
            foreach (var piece in _game.Board.Values)
            {
                if (piece.GetType() == typeof(Rook))
                {
                    var rook = (Rook)piece;
                    var from = _game.GetPieceLocation(rook);
                    foreach (var to in _game.Board.Keys)
                    {
                        var move = new Move { From = from, To = to };
                        Assert.IsTrue(rook.IsValidMove(move, _game.Board));
                    }
                }
            }
        }

        [TestMethod]
        public void TestUpgradePawn()
        {
            var from = new Block { Y = 1, X = 8 };
            var to = new Block { Y = 8, X = 1 };
            var pawn = _game.Board[from];
            _game.UpgradePawn(pawn, to);
            Assert.IsFalse(_game.Board[to] == null);
            Assert.IsFalse(_game.Board[to].GetType() == typeof(King));
            Assert.IsFalse(_game.Board[to].GetType() == typeof(Pawn));
        }

        [TestMethod]
        public void AreaOfEffectsTest()
        {

            var areaOfEffectsCases = new HashSet<Block>
            {
                new Block{X = 3, Y = 5 },
                new Block{X = 1, Y = 5 },
            };
            //foreach (var areaOfEffectCase in areaOfEffectsCases)
            //{
            //    Assert.IsFalse(_game.WhitePlayer.AreaOfEffects.Contains(areaOfEffectCase));
            //}
            var from = new Block { X = 2, Y = 2 };
            var to = new Block { X = 2, Y = 4 };

            var piece = _game.Board[from];
            _game.Play(piece, to);

            foreach (var areaOfEffectCase in areaOfEffectsCases)
            {
                Assert.IsTrue(_game.WhitePlayer.AreaOfEffects.Contains(areaOfEffectCase));
            }

            from = new Block { X = 3, Y = 7 };
            to = new Block { X = 3, Y = 5 };
            piece = _game.Board[from];
            _game.Play(piece, to);

            areaOfEffectsCases = new HashSet<Block>
            {
                new Block{X = 2, Y = 4 },
                new Block{X = 4, Y = 4 },
            };

            foreach (var areaOfEffectCase in areaOfEffectsCases)
            {
                Assert.IsTrue(_game.BlackPlayer.AreaOfEffects.Contains(areaOfEffectCase));
            }

        }
    }
}
