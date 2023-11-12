﻿using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.Sprites;

namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal class BreakableWallEntity : BaseDoorEntity
    {
        public BreakableWallEntity(ISprite entitySprite, Vector2 position, string destination, Direction direction) : base(entitySprite, position, destination, direction)
        {
        }
    }
}