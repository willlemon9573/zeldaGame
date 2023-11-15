using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class DoorlessEntity : BaseDoorEntity
    {
        private readonly SpriteEffects SpriteEffect = SpriteEffects.None;
        private readonly float rotation = 0f;
        private readonly float layerDepth = 0.5f;
        public DoorlessEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            this._doorCollider = new PushBackCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._doorSprite.Draw(spriteBatch, this._doorPosition, this.SpriteEffect, this.rotation, this.layerDepth);
        }
    }
}
