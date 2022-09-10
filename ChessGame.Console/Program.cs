using System;
using ChessGame.Business;
using ChessGame.Common;

namespace ChessGame.Console
{
    class Program
    {
        private static Game _game;

        static void Main(string[] args)
        {
            //TestValidMoves();
            StartNewGame();
        }

        static void StartNewGame()
        {
            _game = new Game();
            bool isGameActive = true;
            System.Console.WriteLine("Game started!");

            while (isGameActive)
            {
                try
                {
                    System.Console.WriteLine($"Turn {_game.TurnNumber}, {_game.CurrentPlayer.Type}'s Turn!");
                    System.Console.WriteLine("Select your piece location(x,y): ");
                    string fromStr = System.Console.ReadLine();
                    var fromArr = fromStr.Split(",");
                    var from = new Block { X = Convert.ToInt16(fromArr[0]), Y = Convert.ToInt16(fromArr[1]) };

                    System.Console.WriteLine("Select your move to location(x,y): ");
                    string ToStr = System.Console.ReadLine();
                    var ToArr = ToStr.Split(",");
                    var to = new Block { X = Convert.ToInt16(ToArr[0]), Y = Convert.ToInt16(ToArr[1]) };

                    _game.Play(new Move { From = from, To = to }, out var gameOverType);
                    if (gameOverType != GameOverType.None)
                    {
                        System.Console.WriteLine(gameOverType.GetDescription());
                        break;
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }

            }

            System.Console.ReadKey();
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
            foreach (var piece in _game.Board.Current.Values)
            {
                if (piece != null)
                {
                    var from = _game.Board.FindPieceLocation(piece);
                    foreach (var to in _game.Board.Current.Keys)
                    {
                        bool isValidMove = piece.IsValidMove(new Move { From = from, To = to }, _game.Board.Current);
                        System.Console.WriteLine($"{piece.Ownership} {piece.GetType().Name} at {from.X},{from.Y} to {to.X},{to.Y} is {isValidMove}.");
                    }
                    System.Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }
            }

            System.Console.ReadKey();
        }
    }
}
