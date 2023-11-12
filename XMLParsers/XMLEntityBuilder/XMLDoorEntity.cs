﻿using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities.Doors;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.Sprites;
using System;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal class XMLDoorEntity : EntityBase
    {
        private const string NameSpace = "SprintZero1.Entities.DungeonRoomEntities.Doors";
        string _destination;
        string _facingDirection;
        string _doorType;

        public string Destination { set { _destination = value; } }
        public string DoorDirection { set { _facingDirection = value; } }

        public string DoorType { set { _doorType = value; } }

        public override IEntity CreateEntity()
        {
            bool ignoreCase = true;
            Direction doorDirection = (Direction)Enum.Parse(typeof(Direction), _facingDirection, ignoreCase);
            ISprite doorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(_entityName);
            Vector2 position = new Vector2(_entityPositionX, _entityPositionY);

            return (BaseDoorEntity)Activator.CreateInstance(Type.GetType($"{NameSpace}.{_doorType}"), doorSprite, position, _destination, doorDirection);
        }
    }
}