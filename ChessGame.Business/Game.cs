using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Business.Piece;
using ChessGame.Common;

namespace ChessGame.Business
{
    public class Game
    {
        public readonly Player WhitePlayer;
        public readonly Player BlackPlayer;
        public Player CurrentPlayer;
        public int TurnNumber;
        private Dictionary<Block, BasePiece> _board;
        private Dictionary<Block, BasePiece> _boardSnapshot;
        public Dictionary<Block, BasePiece> BoardSnapshot
        {
            get => _boardSnapshot;
            set => _boardSnapshot = value;
        }

        public Dictionary<Block, BasePiece> Board
        {
            get => _board;
            set => _board = value;
        }

        public Game()
        {
            InitBoard();
            SetSnapshot();
            //_boardSnapshot = Board.ToDictionary(entry => entry.Key, entry => entry.Value);
            //BoardSnapshot = Board.Clone();
            WhitePlayer = new Player(PlayerType.White);
            BlackPlayer = new Player(PlayerType.Black);
            CurrentPlayer = WhitePlayer;
            TurnNumber = 1;
        }



        private void InitBoard()
        {
            _board = new Dictionary<Block, BasePiece>
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



            };

            //_boardSnapshot = new Dictionary<Block, BasePiece>
            //{
            //    { new Block { X = 1, Y = 1 }, new Rook { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 2, Y = 1 }, new Knight { Ownership = PlayerType.White } },
            //    { new Block { X = 3, Y = 1 }, new Bishop { Ownership = PlayerType.White } },
            //    { new Block { X = 4, Y = 1 }, new Queen { Ownership = PlayerType.White } },
            //    { new Block { X = 5, Y = 1 }, new King { Ownership = PlayerType.White, IsFirstMove = true, IsChecked = false} },
            //    { new Block { X = 6, Y = 1 }, new Bishop { Ownership = PlayerType.White } },
            //    { new Block { X = 7, Y = 1 }, new Knight { Ownership = PlayerType.White } },
            //    { new Block { X = 8, Y = 1 }, new Rook { Ownership = PlayerType.White, IsFirstMove = true } },

            //    { new Block { X = 1, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 2, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 3, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 4, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 5, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 6, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 7, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },
            //    { new Block { X = 8, Y = 2 }, new Pawn { Ownership = PlayerType.White, IsFirstMove = true } },


            //    //{ new Block { X = 1, Y = 2 }, null },
            //    //{ new Block { X = 2, Y = 2 }, null },
            //    //{ new Block { X = 3, Y = 2 }, null },
            //    //{ new Block { X = 4, Y = 2 }, null },
            //    //{ new Block { X = 5, Y = 2 }, null },
            //    //{ new Block { X = 6, Y = 2 }, null },
            //    //{ new Block { X = 7, Y = 2 }, null },
            //    //{ new Block { X = 8, Y = 2 }, null },

            //    { new Block { X = 1, Y = 3 }, null },
            //    { new Block { X = 2, Y = 3 }, null },
            //    { new Block { X = 3, Y = 3 }, null },
            //    { new Block { X = 4, Y = 3 }, null },
            //    { new Block { X = 5, Y = 3 }, null },
            //    { new Block { X = 6, Y = 3 }, null },
            //    { new Block { X = 7, Y = 3 }, null },
            //    { new Block { X = 8, Y = 3 }, null },

            //    { new Block { X = 1, Y = 4 }, null },
            //    { new Block { X = 2, Y = 4 }, null },
            //    { new Block { X = 3, Y = 4 }, null },
            //    { new Block { X = 4, Y = 4 }, null },
            //    { new Block { X = 5, Y = 4 }, null },
            //    { new Block { X = 6, Y = 4 }, null },
            //    { new Block { X = 7, Y = 4 }, null },
            //    { new Block { X = 8, Y = 4 }, null },

            //    { new Block { X = 1, Y = 5 }, null },
            //    { new Block { X = 2, Y = 5 }, null },
            //    { new Block { X = 3, Y = 5 }, null },
            //    { new Block { X = 4, Y = 5 }, null },
            //    { new Block { X = 5, Y = 5 }, null },
            //    { new Block { X = 6, Y = 5 }, null },
            //    { new Block { X = 7, Y = 5 }, null },
            //    { new Block { X = 8, Y = 5 }, null },

            //    { new Block { X = 1, Y = 6 }, null },
            //    { new Block { X = 2, Y = 6 }, null },
            //    { new Block { X = 3, Y = 6 }, null },
            //    { new Block { X = 4, Y = 6 }, null },
            //    { new Block { X = 5, Y = 6 }, null },
            //    { new Block { X = 6, Y = 6 }, null },
            //    { new Block { X = 7, Y = 6 }, null },
            //    { new Block { X = 8, Y = 6 }, null },

            //    //{ new Block { X = 1, Y = 7 }, null },
            //    //{ new Block { X = 2, Y = 7 }, null },
            //    //{ new Block { X = 3, Y = 7 }, null },
            //    //{ new Block { X = 4, Y = 7 }, null },
            //    //{ new Block { X = 5, Y = 7 }, null },
            //    //{ new Block { X = 6, Y = 7 }, null },
            //    //{ new Block { X = 7, Y = 7 }, null },
            //    //{ new Block { X = 8, Y = 7 }, null },

            //    { new Block { X = 1, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 2, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 3, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 4, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 5, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 6, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 7, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 8, Y = 7 }, new Pawn { Ownership = PlayerType.Black, IsFirstMove = true } },

            //    { new Block { X = 1, Y = 8}, new Rook { Ownership = PlayerType.Black, IsFirstMove = true } },
            //    { new Block { X = 2, Y = 8}, new Knight { Ownership = PlayerType.Black } },
            //    { new Block { X = 3, Y = 8}, new Bishop { Ownership = PlayerType.Black } },
            //    { new Block { X = 4, Y = 8}, new Queen { Ownership = PlayerType.Black } },
            //    { new Block { X = 5, Y = 8}, new King { Ownership = PlayerType.Black, IsFirstMove = true, IsChecked = false } },
            //    { new Block { X = 6, Y = 8}, new Bishop { Ownership = PlayerType.Black } },
            //    { new Block { X = 7, Y = 8}, new Knight { Ownership = PlayerType.Black } },
            //    { new Block { X = 8, Y = 8}, new Rook { Ownership = PlayerType.Black, IsFirstMove = true } },



            //};
        }
        public bool Play(BasePiece piece, Block to, bool isGameOverControl = false)
        {
            bool isNextTurn = false;
            var move = new Move
            {
                From = GetPieceLocation(piece),
                To = to,
            };
            var isValidMove = ValidateMove(piece, move, isGameOverControl);
            if (isValidMove)
            {
                piece.IsFirstMove = false;
                CheckKills(move.To);
                UpdateBoard(move);
                UpgradePawn(piece, to);
                var currentKing = GetKing(CurrentPlayer.Type);

                var otherPlayerType = GetOtherPlayerType(CurrentPlayer.Type);
                var otherKing = GetKing(otherPlayerType);
                if (!isGameOverControl)
                {
                    if (!IsKingInDanger(currentKing))
                    {
                        SetKingsDangerZones(otherPlayerType);
                        if (!otherKing.IsChecked)
                        {
                            SwitchPlayer();
                            isNextTurn = true;
                            SetSnapshot();
                        }
                    }
                    bool isGameOver = IsGameOver(out var gameOverType);
                    if (isGameOver)
                    {
                        Console.WriteLine("Game Over!");
                    }
                }
                else
                {
                    isNextTurn = !IsKingInDanger(otherKing);
                    GetSnapshot();
                }
            }

            return isNextTurn;
        }

        private void SetSnapshot()
        {
            var _lock = new object();
            lock (_lock)
            {
                BoardSnapshot = Board.Clone();
                //foreach (var pair in Board)
                //{
                //    BoardSnapshot[pair.Key] = pair.Value;
                //}
            }
        }

        private void GetSnapshot()
        {
            var _lock = new object();
            lock (_lock)
            {
                //Board = BoardSnapshot.Clone();
                foreach (var pair in BoardSnapshot)
                {
                    Board[pair.Key] = pair.Value;
                }
            }
        }
        private void UndoMove()
        {

            //Board = BoardSnapshot.Select();
            //Board = BoardSnapshot.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        private bool IsKingInDanger(King king)
        {
            bool isKingInDanger = false;
            if (king.IsChecked)
            {
                SetKingsDangerZones(king.Ownership);
                if (king.DangerZones.Contains(GetPieceLocation(king)))
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
        private void SetKingsDangerZones(PlayerType kingOwnership)
        {
            var king = GetKing(kingOwnership);
            king.DangerZones = new HashSet<Block>();

            foreach (var piece in Board.Values)
            {
                if (piece != null && piece.Ownership == GetOtherPlayerType(kingOwnership))
                {
                    var from = GetPieceLocation(piece);
                    foreach (var to in Board.Keys)
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

            bool isChecked = king.DangerZones.Contains(GetPieceLocation(king));
            if (isChecked)
            {
                king.IsChecked = true;
                Console.WriteLine("Check!");
            }
        }
        public void UpgradePawn(BasePiece piece, Block to)
        {
            var pieceType = piece.GetType();
            if (pieceType == typeof(Pawn) && ((Pawn)piece).IsUpgradeAvailable(to))
            {
                var upgradedPieceTypes = Enum.GetValues(typeof(UpgradedPieceType)).Cast<UpgradedPieceType>();
                Console.WriteLine("Which to upgrade?");
                foreach (var type in upgradedPieceTypes)
                {
                    Console.WriteLine($"{(int)type}. {type.ToString()}");
                }
                var enteredValue = Console.ReadLine();
                bool isParsed = int.TryParse(enteredValue, out int pieceIndex);
                if (isParsed)
                {
                    var upgradedPieceType = (UpgradedPieceType)pieceIndex;
                    var upgradedPiece = (BasePiece)upgradedPieceType.ToString().CreateClass();
                    upgradedPiece.Ownership = piece.Ownership;
                    Board[to] = upgradedPiece;
                }
            }


        }
        public Block GetPieceLocation(BasePiece piece)
        {
            return Board.FirstOrDefault(x => x.Value != null && x.Value.Id.Equals(piece.Id)).Key;
        }

        public King GetKing(PlayerType ownership)
        {
            return Board.FirstOrDefault(x => x.Value?.Ownership == ownership && x.Value.GetType() == typeof(King)).Value as King;
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

        private bool ValidateMove(BasePiece piece, Move move, bool isDangerZonesControl = false)
        {
            bool isValidMove = false;
            if (isDangerZonesControl || piece?.Ownership == CurrentPlayer.Type)
            {
                bool isPieceNotMoved = move.From.X == move.To.X && move.From.Y == move.To.Y;
                isValidMove = !isPieceNotMoved && piece.IsValidMove(move, Board, isDangerZonesControl);
            }
            return isValidMove;
        }
        private void CheckKills(Block to)
        {
            var killedPiece = Board[to];
            if (killedPiece != null)
            {
                Console.WriteLine($"{killedPiece.Ownership} player's {killedPiece.GetType().Name} killed!");
            }
        }
        private void UpdateBoard(Move move)
        {
            Board[move.To] = Board[move.From];
            Board[move.From] = null;
        }

        private bool IsGameOver(out GameOverType gameOverType)
        {
            SetSnapshot();
            var otherPlayerType = GetOtherPlayerType(CurrentPlayer.Type);
            bool isOtherPlayerChecked = IsKingInDanger(GetKing(otherPlayerType));
            object _lock = new object();
            //foreach (var piece in Board.Values)
            //{
            for (int i = 0; i < Board.Values.Count; i++)
            {

                var piece = Board.Values.ElementAt(i);
                if (piece != null && piece.Ownership == otherPlayerType)
                {
                    //foreach (var to in Board.Keys)
                    //{
                    for (int j = 0; j < Board.Keys.Count; j++)
                    {
                        //GetSnapshot();
                        var to = Board.Keys.ElementAt(j);
                        //var move = new Move
                        //{
                        //    To = Board.Keys.ElementAt(i),
                        //    From = GetPieceLocation(piece)
                        //};
                        lock (_lock)
                        {
                            if (Play(piece, to, true))
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

    public enum GameOverType
    {
        None = 0,
        Draw = 1,
        WhiteWins = 2,
        BlackWins = 2,
    }
}
