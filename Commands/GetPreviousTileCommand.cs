using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class GetPreviousTileCommand : ICommand
    {
        private readonly List<string> blockNames;
        private readonly Game1 myGame;
        private readonly TileSpriteFactory tileSpriteFactory;
        private readonly int totalBlocks;
        public GetPreviousTileCommand(Game1 game)
        {
            myGame = game;
            tileSpriteFactory = TileSpriteFactory.Instance;
            blockNames = tileSpriteFactory.TileSourceRectangles;
            totalBlocks = blockNames.Count;
        }

        public void Execute()
        {
            /* myGame.OnScreenTileIndex = (myGame.OnScreenTileIndex - 1 + totalBlocks) % totalBlocks; // clock arithmetic [0, totalBlocks]
             myGame.OnScreenTile = tileSpriteFactory.CreateNewTileSprite(blockNames[myGame.OnScreenTileIndex]);*/
        }
    }
}
