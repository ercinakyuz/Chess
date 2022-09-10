using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Business.Piece;
using ChessGame.Common;

namespace ChessGame.Business
{
    public class Board
    {
        public Dictionary<Block, BasePiece> Current { get; private set; }
        public Dictionary<Block, BasePiece> Snapshot { get; private set; }

        public static Board Init()
        {
            var board = new Board
            {
                Current = new Dictionary<Block, BasePiece>
                {
                    { new Block { X = 1, Y = 1 }, new Rook { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 2, Y = 1 }, new Knight { Id=Guid.NewGuid(), Ownership = PlayerType.White } },
                    { new Block { X = 3, Y = 1 }, new Bishop {Id=Guid.NewGuid(), Ownership = PlayerType.White } },
                    { new Block { X = 4, Y = 1 }, new Queen { Id=Guid.NewGuid(), Ownership = PlayerType.White } },
                    { new Block { X = 5, Y = 1 }, new King { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true, IsChecked = false} },
                    { new Block { X = 6, Y = 1 }, new Bishop { Id=Guid.NewGuid(), Ownership = PlayerType.White } },
                    { new Block { X = 7, Y = 1 }, new Knight { Id=Guid.NewGuid(), Ownership = PlayerType.White } },
                    { new Block { X = 8, Y = 1 }, new Rook { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },

                    { new Block { X = 1, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 2, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 3, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 4, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 5, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 6, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 7, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },
                    { new Block { X = 8, Y = 2 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.White, IsFirstMove = true } },


                    //{ new Block { X = 1, Y = 2 }, null },
                    //{ new Block { X = 2, Y = 2 }, null },
                    //{ new Block { X = 3, Y = 2 }, null },
                    //{ new Block { X = 4, Y = 2 }, null },
                    //{ new Block { X = 5, Y = 2 }, null },
                    //{ new Block { X = 6, Y = 2 }, null },
                    //{ new Block { X = 7, Y = 2 }, null },
                    //{ new Block { X = 8, Y = 2 }, null },

                    { new Block { X = 1, Y = 3 }, null },
                    { new Block { X = 2, Y = 3 }, null },
                    { new Block { X = 3, Y = 3 }, null },
                    { new Block { X = 4, Y = 3 }, null },
                    { new Block { X = 5, Y = 3 }, null },
                    { new Block { X = 6, Y = 3 }, null },
                    { new Block { X = 7, Y = 3 }, null },
                    { new Block { X = 8, Y = 3 }, null },

                    { new Block { X = 1, Y = 4 }, null },
                    { new Block { X = 2, Y = 4 }, null },
                    { new Block { X = 3, Y = 4 }, null },
                    { new Block { X = 4, Y = 4 }, null },
                    { new Block { X = 5, Y = 4 }, null },
                    { new Block { X = 6, Y = 4 }, null },
                    { new Block { X = 7, Y = 4 }, null },
                    { new Block { X = 8, Y = 4 }, null },

                    { new Block { X = 1, Y = 5 }, null },
                    { new Block { X = 2, Y = 5 }, null },
                    { new Block { X = 3, Y = 5 }, null },
                    { new Block { X = 4, Y = 5 }, null },
                    { new Block { X = 5, Y = 5 }, null },
                    { new Block { X = 6, Y = 5 }, null },
                    { new Block { X = 7, Y = 5 }, null },
                    { new Block { X = 8, Y = 5 }, null },

                    { new Block { X = 1, Y = 6 }, null },
                    { new Block { X = 2, Y = 6 }, null },
                    { new Block { X = 3, Y = 6 }, null },
                    { new Block { X = 4, Y = 6 }, null },
                    { new Block { X = 5, Y = 6 }, null },
                    { new Block { X = 6, Y = 6 }, null },
                    { new Block { X = 7, Y = 6 }, null },
                    { new Block { X = 8, Y = 6 }, null },

                    //{ new Block { X = 1, Y = 7 }, null },
                    //{ new Block { X = 2, Y = 7 }, null },
                    //{ new Block { X = 3, Y = 7 }, null },
                    //{ new Block { X = 4, Y = 7 }, null },
                    //{ new Block { X = 5, Y = 7 }, null },
                    //{ new Block { X = 6, Y = 7 }, null },
                    //{ new Block { X = 7, Y = 7 }, null },
                    //{ new Block { X = 8, Y = 7 }, null },

                    { new Block { X = 1, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 2, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 3, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 4, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 5, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 6, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 7, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 8, Y = 7 }, new Pawn { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },

                    { new Block { X = 1, Y = 8}, new Rook { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },
                    { new Block { X = 2, Y = 8}, new Knight { Id=Guid.NewGuid(), Ownership = PlayerType.Black } },
                    { new Block { X = 3, Y = 8}, new Bishop { Id=Guid.NewGuid(), Ownership = PlayerType.Black } },
                    { new Block { X = 4, Y = 8}, new Queen { Id=Guid.NewGuid(), Ownership = PlayerType.Black } },
                    { new Block { X = 5, Y = 8}, new King { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true, IsChecked = false } },
                    { new Block { X = 6, Y = 8}, new Bishop { Id=Guid.NewGuid(), Ownership = PlayerType.Black } },
                    { new Block { X = 7, Y = 8}, new Knight { Id=Guid.NewGuid(), Ownership = PlayerType.Black } },
                    { new Block { X = 8, Y = 8}, new Rook { Id=Guid.NewGuid(), Ownership = PlayerType.Black, IsFirstMove = true } },

                }
            };
            return board.TakeSnapshot();
        }

        public Board TakeSnapshot()
        {
            Snapshot = Current.Clone();
            return this;
        }

        public Board Undo()
        {
            Current = Snapshot.Clone();
            return this;
        }
        public Board Update(Move move)
        {
            Current[move.To] = Current[move.From];
            Current[move.From] = null;
            return this;
        }

        public BasePiece FindPiece(Block block)
        {
            return Current[block];
        }

        public King GetKing(PlayerType ownership)
        {
            return Current.Single(x => x.Value?.Ownership == ownership && x.Value.GetType() == typeof(King)).Value as King;
        }

        public Block FindPieceLocation(BasePiece piece)
        {
            return Current.Single(x => x.Value != null && x.Value.Id.Equals(piece.Id)).Key;
        }
    }
}
