using System.Collections.Generic;
using SprintZero1.Factories;

namespace SprintZero1.Commands
{
    public class PreviousItemCommand : ICommand
    {
        private readonly List<string> itemNames;
        private readonly Game1 myGame;
        private readonly ItemFactory myItemFactory;
        private readonly int totalItems;
        public PreviousItemCommand(Game1 game)
        {
            myGame = game;
            myItemFactory = ItemFactory.Instance;
            itemNames = myItemFactory.ItemNamesList;
            totalItems = itemNames.Count;
        }

        public void Execute()
        {
            myGame.OnScreenItemIndex = (myGame.OnScreenItemIndex - 1 + totalItems) % totalItems;
            myGame.Item = myItemFactory.CreateItemSprite(itemNames[myGame.OnScreenItemIndex]);
        }
    }
}
