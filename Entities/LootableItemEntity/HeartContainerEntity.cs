using Microsoft.Xna.Framework;
using SprintZero1.Colliders.ItemColliders;
using SprintZero1.LevelFiles;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal class HeartContainerEntity : LootableItemBase
    {
        /// <summary>
        /// Container class for Heart Entity
        /// </summary>
        /// <param name="entitySprite">The sprite of the entity</param>
        /// <param name="position">The position of the entity</param>
        /// <param name="removeDelegate">The delegate for removing the entity</param>
        public HeartContainerEntity(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate) : base(entitySprite, position, removeDelegate)
        {
            this._entitySprite = entitySprite;
            this.Position = position;
            this._removeFromRoom = removeDelegate;
            this._entityCollider = new HeartContainerCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height));
        }
        /* Note that this one doesn't need a pickup handler as the player health will be modified directly by the command */

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
