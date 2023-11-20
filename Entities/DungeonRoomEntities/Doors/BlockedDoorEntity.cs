using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class BlockedDoorEntity : BaseDoorEntity
    {
        private readonly SpriteEffects SpriteEffect = SpriteEffects.None;
        private readonly float rotation = 0f;
        private readonly float layerDepth = 0.5f;

        public BlockedDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            Vector2 offsets = _colliderOffsetDictionary[DoorDirection];
            this._doorCollider = new BlockedDoorCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._doorSprite.Draw(spriteBatch, this._doorPosition, this.SpriteEffect, this.rotation, this.layerDepth);
        }

        public override void OpenDoor()
        {
            SoundFactory.PlaySound(SoundFactory.GetSound("door_unlock"));
            string doorType = $"open_{this.DoorDirection}";
            this._doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(doorType.ToLower());
            Vector2 offset = _colliderOffsetDictionary[DoorDirection];
            this._doorCollider = new OpenDoorCollider(_doorPosition, new System.Drawing.Size(_doorSprite.Width, _doorSprite.Height), ScaleFactor, (int)offset.X, (int)offset.Y);
        }
    }
}
