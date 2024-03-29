﻿using Microsoft.Xna.Framework;
using SprintZero1.Colliders.DoorColliders;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{

    internal class LockedDoorEntity : BaseDoorEntity
    {
        public LockedDoorEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
            _doorCollider = new LockedDoorCollider(position, new System.Drawing.Size(entitySprite.Width, entitySprite.Height));
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
