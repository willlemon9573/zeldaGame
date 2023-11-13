using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class LockedDoorEntity : BaseDoorEntity
    {
        private readonly SpriteEffects SpriteEffect = SpriteEffects.None;
        private readonly float rotation = 0f;
        private readonly float layerDepth = 0.5f;

        public LockedDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            Rectangle colliderDimensions = new Rectangle((int)position.X, (int)position.Y, DoorDimensions.Width, DoorDimensions.Height);
            this._doorCollider = new LockedDoorCollider(colliderDimensions);
        }
    }
}
