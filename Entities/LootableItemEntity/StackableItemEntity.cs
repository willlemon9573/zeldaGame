using Microsoft.Xna.Framework;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System.Drawing;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class StackableItemEntity : LootableItemBase
    {
        private readonly StackableItemHandler _pickupHandler;
        private readonly StackableItems _itemType;

        public StackableItems ItemType { get { return _itemType; } }
        /// <summary>
        /// Construct a new object that is a dungeon item entity
        /// </summary>
        /// <param name="entitySprite">The sprite of the entity</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="removeDelegate">The delegate that points to removing the entity from the room</param>
        /// <param name="pickupHandler">the delegate that points to the function for adding the item to the player's inventory</param>
        /// <param name="itemType">The specific type of item the entity is</param>
        public StackableItemEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate, StackableItemHandler pickupHandler, StackableItems itemType) : base(entitySprite, position, removeDelegate)
        {
            _pickupHandler = pickupHandler;
            _itemType = itemType;
            _entityCollider = new StackableItemCollider(_entityPosition, new Size(entitySprite.Width, entitySprite.Height));
        }

        public override void Pickup(IEntity player, int amt = 0)
        {
            _pickupHandler(player, _itemType, amt);
        }

        /// <summary>
        /// Update this item if it needs to be updated
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _entityCollider.Update(this);
        }
    }
}
