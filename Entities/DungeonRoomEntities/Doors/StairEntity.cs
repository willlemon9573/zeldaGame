using Microsoft.Xna.Framework;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class StairEntity : BaseDoorEntity
    {
        /// <summary>
        /// Create a type of collider meant for stairs.
        /// </summary>
        /// <param name="entitySprite"></param>
        /// <param name="position"></param>
        /// <param name="destination"></param>
        /// <param name="direction"></param>
        public StairEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            /* Stairs are smaller than doors, so the collider needs to be smaller */
            Vector2 offset = _colliderOffsetDictionary[direction];
            this._doorCollider = new OpenDoorCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height), ScaleFactor, (int)offset.X, (int)offset.Y);
        }
    }
}
