using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ChessGame.Business.Piece;

namespace ChessGame.Business
{
    public class Game
    {
        public Player WhitePlayer { get; }
        public Player BlackPlayer { get; }
        public Player CurrentPlayer { get; private set; }
        public int TurnNumber { get; private set; }
        public Board Board { get; }

        public Game()
        {
            Board = Board.Init();
            WhitePlayer = new Player(PlayerType.White);
            BlackPlayer = new Player(PlayerType.Black);
            CurrentPlayer = WhitePlayer;
            TurnNumber = 1;
        }
        public bool Play(Move move, out GameOverType gameOverType, bool isGameOverControl = false)
        {
            bool isNextTurn = false;
            gameOverType = GameOverType.None;
            var piece = Board.FindPiece(move.From) ?? throw new ArgumentException($"Piece not found at {move.From}");
            var isValidMove = ValidateMove(piece, move, false, isGameOverControl);
            if (isValidMove)
            {
                piece.IsFirstMove = false;
                AnyPieceKilled(move.To, isGameOverControl);
                Board.Update(move);
                UpgradePawn(piece, move.To);
                var currentKing = Board.GetKing(CurrentPlayer.Type);

                var otherPlayerType = GetOtherPlayerType(CurrentPlayer.Type);
                var otherKing = Board.GetKing(otherPlayerType);
                if (!isGameOverControl)
                {
                    if (IsKingInDanger(currentKing))
                    {
                        Board.Undo();
                    }
                    else
                    {
                        SetKingsDangerZones(otherPlayerType);
                        if (IsOver(move, out gameOverType))
                        {
                            return true;
                        }

                        isNextTurn = true;
                        GetNextTurn();
                    }
                }
                else
                {
                    isNextTurn = !IsKingInDanger(otherKing, true);
                    Board.Undo();
                }
            }

            return isNextTurn;
        }

        private void GetNextTurn()
        {
            SwitchPlayer();
            Board.TakeSnapshot();
        }

        private bool IsKingInDanger(King king, bool isGameOverControl = false)
        {
            bool isKingInDanger = false;
            if (king.IsChecked)
            {
                SetKingsDangerZones(king.Ownership, isGameOverControl);
                if (king.DangerZones.Contains(Board.FindPieceLocation(king)))
                {
                    isKingInDanger = true;
                }
                else
                {
                    king.IsChecked = false;
                }
            }
            return isKingInDanger;
        }
        private void SetKingsDangerZones(PlayerType kingOwnership, bool isGameOverControl = false)
        {
            var king = Board.GetKing(kingOwnership);
            king.DangerZones = new HashSet<Block>();

            foreach (var piece in Board.Current.Values)
            {
                if (piece != null && piece.Ownership == GetOtherPlayerType(kingOwnership))
                {
                    var from = Board.FindPieceLocation(piece);
                    foreach (var to in Board.Current.Keys)
                    {
                        if (!king.DangerZones.Contains(to))
                        {
                            var pieceType = piece.GetType();
                            bool isValidMove = ValidateMove(piece, new Move { From = from, To = to }, true);

                            if (pieceType == typeof(Pawn) && ((Pawn)piece).IsKillableMove)
                            {
                                king.DangerZones.Add(to);
                            }
                            else if (pieceType != typeof(Pawn) && isValidMove)
                            {
                                king.DangerZones.Add(to);
                            }
                        }
                    }
                }
            }

            king.IsChecked = king.DangerZones.Contains(Board.FindPieceLocation(king));
            if (king.IsChecked)
            {
                if (!isGameOverControl)
                {
                    Console.WriteLine("Check!");
                }
            }
        }
        public void UpgradePawn(BasePiece piece, Block to)
        {
            if (piece is Pawn pawn)
                pawn.Upgrade(to, Board.Current);
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = GetOtherPlayer();
            TurnNumber++;
        }

        private Player GetOtherPlayer()
        {
            return CurrentPlayer.Type == PlayerType.White ? BlackPlayer : WhitePlayer;
        }

        private PlayerType GetOtherPlayerType(PlayerType playerType = PlayerType.None)
        {
            if (playerType == PlayerType.None)
            {
                playerType = CurrentPlayer.Type;
            }
            return playerType == PlayerType.White ? PlayerType.Black : PlayerType.White;
        }

        private bool ValidateMove(BasePiece piece, Move move, bool isDangerZonesControl = false, bool isGameOverControl = false)
        {
            bool isValidMove = false;
            if (isDangerZonesControl || isGameOverControl || piece?.Ownership == CurrentPlayer.Type)
            {
                var isPieceNotMoved = move.From.X == move.To.X && move.From.Y == move.To.Y;
                isValidMove = !isPieceNotMoved && piece.IsValidMove(move, Board.Current, isDangerZonesControl);
            }
            return isValidMove;
        }
        private void AnyPieceKilled(Block to, bool isGameOverControl = false)
        {
            var killedPiece = Board.FindPiece(to);
            if (killedPiece != null)
            {
                if (!isGameOverControl)
                {
                    Console.WriteLine($"{killedPiece.Ownership} player's {killedPiece.GetType().Name} killed!");
                }
            }
        }
        private bool IsOver(Move move, out GameOverType gameOverType)
        {
            Board.TakeSnapshot();
            var otherPlayerType = GetOtherPlayerType(CurrentPlayer.Type);
            bool isOtherPlayerChecked = IsKingInDanger(Board.GetKing(otherPlayerType), true);
            object _lock = new object();
            for (int i = 0; i < Board.Current.Values.Count; i++)
            {
                var piece = Board.Current.Values.ElementAt(i);
                if (piece != null && piece.Ownership == otherPlayerType)
                {
                    for (int j = 0; j < Board.Current.Keys.Count; j++)
                    {
                        var to = Board.Current.Keys.ElementAt(j);
                        lock (_lock)
                        {
                            if (Play(move, out var isGameOver, true))
                            {
                                gameOverType = GameOverType.None;
                                return false;
                            }
                        }

                    }
                }
            }
            if (isOtherPlayerChecked)
            {
                gameOverType = CurrentPlayer.Type == PlayerType.White ? GameOverType.WhiteWins : GameOverType.BlackWins;
            }
            else
            {
                gameOverType = GameOverType.Draw;
            }

            return true;
        }
    }

    public enum CheckType
    {
        None,
        Check,
        CheckMate
    }

    public enum GameOverType
    {
        [Description("Game not finished")]
        None = 0,
        [Description("Draw")]
        Draw = 1,
        [Description("White wins")]
        WhiteWins = 2,
        [Description("Black wins")]
        BlackWins = 2,
    }
}
