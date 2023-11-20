using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.LevelFiles;
using SprintZero1.Sprites;
using Size = System.Drawing.Size;

namespace SprintZero1.Entities.LootableItemEntity
{
    /// <summary>
    /// The base implementation for a lootable item
    /// @author Aaron Heishman
    /// </summary>
    internal abstract class LootableItemBase : ILootableEntity
    {
        private const float Rotation = 0f;
        private const float LayerDepth = 0.5f;
        private const SpriteEffects EntitySpriteEffects = SpriteEffects.None;
        private readonly Size _entityDimensions = new Size(8, 8);
        protected ISprite _entitySprite;
        protected ICollider _entityCollider;
        protected Vector2 _entityPosition;
        protected RemoveDelegate _removeFromRoom;

        /// <summary>
        /// Get this item's collider
        /// </summary>
        public ICollider Collider { get { return _entityCollider; } }

        /// <summary>
        /// Get and set this item's position
        /// </summary>
        public Vector2 Position { get { return _entityPosition; } set { _entityPosition = value; } }

        /// <summary>
        /// Constructor for base lootable items
        /// </summary>
        /// <param name="entitySprite">The item sprite</param>
        /// <param name="position">The position of the item</param>
        /// <param name="removeDelegate">The delegate that points to the specific room's remove function</param>
        protected LootableItemBase(ISprite entitySprite, Vector2 position, RemoveDelegate removeDelegate)
        {
            _entitySprite = entitySprite;
            _entityPosition = position;
            _removeFromRoom = removeDelegate;
            _entityCollider = new StaticCollider(_entityPosition, new Size(entitySprite.Width, entitySprite.Height));
        }

        /// <summary>
        /// When overriden updates player inventory to contain the amount given of the item
        /// </summary>
        /// <param name="player">The player that picked up the item</param>
        /// <param name="amt">The amount of the item (optional)</param>
        public virtual void Pickup(IEntity player, int amt = 0)
        {
            /* To be overriden by derived class */
        }
        /// <summary>
        /// When overriden updates player inventory to contain the weapon given
        /// </summary>
        /// <param name="player">The player that picked up the item</param>
        /// <param name="weapon">The weapon the player will be given(optional)</param>
        public virtual void Pickup(IEntity player, IWeaponEntity weapon)
        {
            /* To be overriden by derived class */
        }

        /// <summary>
        /// remove this item from the current room
        /// </summary>
        public virtual void Remove()
        {
            _removeFromRoom(this);
        }

        /// <summary>
        /// Draw's this item into the room
        /// </summary>
        /// <param name="spriteBatch">The current sprite batch drawing entities</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this._entitySprite.Draw(spriteBatch, _entityPosition, EntitySpriteEffects, Rotation, LayerDepth);
        }

        /// <summary>
        /// Update this item if it needs to be updated
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);


    }
}
