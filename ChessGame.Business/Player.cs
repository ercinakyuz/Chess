using System.Collections.Generic;

namespace ChessGame.Business
{
    public class Player
    {
        public PlayerType Type { get; set; }
        public HashSet<Block> AreaOfEffects { get; set; }

        public Player(PlayerType playerType)
        {
            Type = playerType;
        }
    }
}
