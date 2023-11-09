using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Sprites;

namespace SprintZero1.Entities
{
    internal class FireTrapEntity : ICollidableEntity
    {
        private StaticCollider _trapCollider;
        private ISprite _entitySprite;
        private Vector2 _entityPosition;
        private readonly SpriteEffects _spriteEffects = SpriteEffects.None;
        private readonly float _spriteRotation = 0;
        private readonly float _layerDepth = 0.4f;

        public ICollider Collider { get { return _trapCollider; } }

        public Vector2 Position
        {
            get { return _entityPosition; }
            set { _entityPosition = value; }
        }
        public FireTrapEntity(ISprite sprite, Vector2 position, Rectangle dimensions) : base(sprite, position)
        {
            this._sprite = sprite;
            this.Position = position;
            _trapCollider = new LevelBlockCollider(dimensions);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._sprite.Draw(spriteBatch, this.Position, _spriteEffects, _spriteRotation, _layerDepth);
        }

        public override void Update(GameTime gameTime)
        {
            this._sprite.Update(gameTime);
        }
    }
}
