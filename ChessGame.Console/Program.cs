using System;
using ChessGame.Business;

namespace ChessGame.Console
{
    class Program
    {
        private static Game _game;
        private static Game _snapShot;
        static void Main(string[] args)
        {
            //TestValidMoves();
            StartNewGame();
        }

        static void StartNewGame()
        {
            _game = new Game();
            _snapShot = _game;
            bool isGameActive = true;
            bool isNextTurn = true;
            System.Console.WriteLine("Game started!");

            while (isGameActive)
            {
                //if (isNextTurn)
                //{
                //    _snapShot = _game;
                //}
                //else
                //{
                //    _game = _snapShot;
                //}

                System.Console.WriteLine($"Turn {_game.TurnNumber}, {_game.CurrentPlayer.Type}'s Turn!");
                System.Console.WriteLine("Select your piece location(x,y): ");
                string fromStr = System.Console.ReadLine();
                var fromArr = fromStr.Split(",");
                var from = new Block { X = Convert.ToInt16(fromArr[0]), Y = Convert.ToInt16(fromArr[1]) };

                System.Console.WriteLine("Select your move to location(x,y): ");
                string ToStr = System.Console.ReadLine();
                var ToArr = ToStr.Split(",");
                var to = new Block { X = Convert.ToInt16(ToArr[0]), Y = Convert.ToInt16(ToArr[1]) };

                var piece = _game.Board[from];
                _game.Play(piece, to);
            }
        }

        static void StopTheGame(Game game)
        {
            // ReSharper disable once RedundantCheckBeforeAssignment
            if (game != null)
            {
                game = null;
            }
        }

        static void TestValidMoves()
        {
            _game = new Game();
            foreach (var piece in _game.Board.Values)
            {
                if (piece != null)
                {
                    var from = _game.GetPieceLocation(piece);
                    foreach (var to in _game.Board.Keys)
                    {
                        bool isValidMove = piece.IsValidMove(new Move { From = from, To = to }, _game.Board);
                        System.Console.WriteLine($"{piece.Ownership} {piece.GetType().Name} at {from.X},{from.Y} to {to.X},{to.Y} is {isValidMove}.");
                    }
                    System.Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }
            }

            System.Console.ReadKey();
        }
    }
}
