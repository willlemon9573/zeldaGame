using SprintZero1.Sprites;

namespace SprintZero1.InventoryFiles
{
    internal interface IStackableItems
    {

        /// <summary>
        /// Get the item sprite
        /// </summary>
        ISprite ItemSprite { get; }
        /// <summary>
        /// Get the current amount of the item
        /// </summary>
        int CurrentStock { get; }
        /// <summary>
        /// Get the max amount the item can have
        /// </summary>
        int MaxStock { get; }
        /// <summary>
        /// Handles what happens when using the item
        /// </summary>
        /// <param name="amount">The amount of the item to use</param>
        /// <returns></returns>
        void UsedItem(int amount);
        /// <summary>
        /// Add the amount of the item to the player inventory
        /// </summary>
        /// <param name="amount">The amount to add to the inventory</param>
        void PickedUpItem(int amount);
    }
}
