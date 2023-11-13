using SprintZero1.Entities;
using SprintZero1.Sprites;

namespace SprintZero1.InventoryFiles
{

    internal interface IPlayerItem
    {
        /// <summary>
        /// Get the Item Entity
        /// </summary>
        IEntity ItemEntity { get; }
        /// <summary>
        /// Get the item sprite
        /// </summary>
        ISprite ItemSprite { get; }
    }
}
