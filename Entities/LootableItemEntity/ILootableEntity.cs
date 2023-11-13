namespace SprintZero1.Entities.LootableItemEntity
{
    internal interface ILootableEntity : ICollidableEntity
    {
        /// <summary>
        /// Handles what happens when picking up an item
        /// </summary>
        /// <param name="player">The player picking up the item</param>
        /// <param name="amount">[Optional]The amount given. Default param is 0[Optional]</param>
        void Pickup(IEntity player, int amount = 0);

        /// <summary>
        /// Handles the removal of the item from being displayed
        /// </summary>
        void Remove();

    }
}
