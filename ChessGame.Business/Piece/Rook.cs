using System;
using System.Collections.Generic;

namespace ChessGame.Business.Piece
{
    public class Rook : BasePiece
    {
        public override bool IsValidMove(Move move, Dictionary<Block, BasePiece> board, bool isDangerZonesControl = false)
        {
            bool isValidMove = base.IsValidMove(move, board, isDangerZonesControl) && (move.From.X == move.To.X || move.From.Y == move.To.Y);
            if (isValidMove)
            {
                int xDist = move.To.X - move.From.X;
                int yDist = move.To.Y - move.From.Y;
                int xAbsDist = Math.Abs(xDist);
                int yAbsDist = Math.Abs(yDist);
                if (xAbsDist > 0)
                {
                    int signofXDist = Math.Sign(xDist);
                    for (int i = 1; i < xAbsDist; i++)
                    {
                        if (board[new Block { X = move.From.X + i * signofXDist, Y = move.From.Y }] != null)
                        {
                            isValidMove = false;
                            break;
                        }
                    }
                }
                else if (yAbsDist > 0)
                {
                    int signofYDist = Math.Sign(yDist);
                    for (int i = 1; i < yAbsDist; i++)
                    {
                        if (board[new Block { X = move.From.X, Y = move.From.Y + i * signofYDist }] != null)
                        {
                            isValidMove = false;
                            break;
                        }
                    }
                }
            }
            return isValidMove;
        }

    }
}
