using Microsoft.Xna.Framework;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    /* class had to be made because of the way collisions and equipment work. One has to take player with it, the other cannot. Can't add the same type as a key in the collider dictionary */
    internal class EquipmentItemWithPlayerEntity : LootableItemBase
    {
        private readonly EquipmentItem _equipmentItem;
        private readonly EquipmentItemHandler _pickupHandler;
        /// <summary>
        /// Get the type of the equipment item
        /// </summary>
        public EquipmentItem ItemType { get { return _equipmentItem; } }

        public EquipmentItemWithPlayerEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate, EquipmentItemHandler pickupHandler, EquipmentItem itemType) : base(entitySprite, position, removeDelegate)
        {
            _equipmentItem = itemType;
            _pickupHandler = pickupHandler;
            _entityCollider = new EquipmentWithPlayerCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height));
        }
        /// <summary>
        /// Controls what happens when the player picks up an equipment
        /// </summary>
        /// <param name="player">The player picking up the equipment</param>
        /// <param name="weapon">The weapon to be picked up</param>
        public override void Pickup(IEntity player, IWeaponEntity weapon)
        {
            _pickupHandler(player, _equipmentItem, weapon);
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
