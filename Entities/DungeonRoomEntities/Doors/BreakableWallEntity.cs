using Microsoft.Xna.Framework;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class BreakableWallEntity : BaseDoorEntity
    {
        public BreakableWallEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            this._doorCollider = new BreakableWallCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height), ScaleFactor);
        }

        public override void OpenDoor()
        {
            string doorType = $"hole_{this.DoorDirection}";
            this._doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(doorType.ToLower());
            Vector2 offset = _colliderOffsetDictionary[DoorDirection];
            this._doorCollider = new OpenDoorCollider(_doorPosition, new System.Drawing.Size(_doorSprite.Width, _doorSprite.Height), ScaleFactor, (int)offset.X, (int)offset.Y);
        }
    }
}
