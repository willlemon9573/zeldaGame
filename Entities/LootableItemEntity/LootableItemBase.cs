using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.LootableItemEntity
{
    internal abstract class LootableItemBase : ILootableEntity
    {
        protected ISprite _entitySprite;
        protected ICollider _entityCollider;
        protected Vector2 _entityPosition;

        public ICollider Collider { get { return _entityCollider; } }

        public Vector2 Position { get { return _entityPosition; } set { _entityPosition = value; } }
        /// <summary>
        /// Abstract constructor for 
        /// </summary>
        /// <param name="entitySprite">The entities sprite</param>
        /// <param name="entityCollider"></param>
        /// <param name="position"></param>
        /// <param name="dimensions"></param>
        public LootableItemBase(ISprite entitySprite, Vector2 position)
        {
            _entitySprite = entitySprite;
            _entityPosition = position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this._entitySprite.Draw(spriteBatch, _entityPosition);
        }

        public abstract void Pickup(IEntity Player, int amt = 0);

        public abstract void Remove();

        public abstract void Update(GameTime gameTime);
    }
}
