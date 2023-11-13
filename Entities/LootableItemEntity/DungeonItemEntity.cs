using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    /// <summary>
    /// For items such as maps and compasses
    /// </summary>
    internal class DungeonItemEntity : LootableItemBase
    {
        private readonly DungeonItems _itemType;
        private readonly UtilityItemHandler _pickupHandler;
        public DungeonItems ItemType { get { return _itemType; } }
        /// <summary>
        /// Construct a new object that is a dungeon item entity
        /// </summary>
        /// <param name="entitySprite">The sprite of the entity</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="removeDelegate">The delegate that points to removing the entity from the room</param>
        /// <param name="pickupHandler">the delegate that points to the function for adding the item to the player's inventory</param>
        /// <param name="itemType">The specific type of item the entity is</param>
        public DungeonItemEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate, UtilityItemHandler pickupHandler, DungeonItems itemType) : base(entitySprite, position, removeDelegate)
        {
            _itemType = itemType;
            _pickupHandler = pickupHandler;
        }

        public override void Pickup(IEntity player, int amt = 0)
        {
            _pickupHandler(player, _itemType);
        }
    }
}
