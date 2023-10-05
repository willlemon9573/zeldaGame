using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Commands
{
    public class NextItemCommand : ICommand
    {
        private readonly List<string> itemNames;
        private readonly Game1 myGame;
        private readonly ItemFactory myItemFactory;
        private int totalItems;
        public NextItemCommand(Game1 game)
        {
            myGame = game;
            myItemFactory = ItemFactory.Instance;
            itemNames = myItemFactory.ItemNamesList;
            totalItems = itemNames.Count;
        }

        public void Execute()
        {
            /*myGame.OnScreenItemIndex = (myGame.OnScreenItemIndex + 1) % totalItems;
            myGame.Item = myItemFactory.CreateItemSprite(itemNames[myGame.OnScreenItemIndex]);*/
        }
    }
}