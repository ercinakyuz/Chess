using System;
using System.Collections.Generic;

namespace ChessGame.Business.Piece
{
    public class Knight : BasePiece
    {
        public override bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false, bool isGameOverControl = false)
        {
            int xDist = move.To.X - move.From.X;
            int yDist = move.To.Y - move.From.Y;
            int xAbsDist = Math.Abs(xDist);
            int yAbsDist = Math.Abs(yDist);
            return base.IsValidMove(move, board, isDangerZonesControl) && ((xAbsDist == 2 && yAbsDist == 1) || (xAbsDist == 1 && yAbsDist == 2));
        }
    }
}
