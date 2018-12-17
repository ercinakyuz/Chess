using System.Collections.Generic;

namespace ChessGame.Business.Piece
{
    public class Queen : BasePiece
    {
        public override bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false)
        {
            var rook = new Rook { Ownership = Ownership };
            var bishop = new Bishop { Ownership = Ownership };
            return rook.IsValidMove(move, board, isDangerZonesControl) || bishop.IsValidMove(move, board, isDangerZonesControl);
        }
    }
}
