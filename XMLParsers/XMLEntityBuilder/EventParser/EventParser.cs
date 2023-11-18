using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Entities.DungeonRoomEntities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.LevelFiles.RoomEvents;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SprintZero1.XMLParsers.XMLEntityBuilder.EventParser
{
    internal class EventParser
    {
        private const string BlockElement = "Block";
        private const string StartXElement = "X";
        private const string StartYElement = "Y";
        private const string EndXElement = "X2";
        private const string EndYElement = "Y2";
        private const string TriggerXElement = "TriggerX";
        private const string TriggerYElement = "TriggerY";
        private const string NameElement = "Name";
        private const string DoorDirectionElement = "DoorDirection";
        private const string EventInfoElement = "EventInfo";
        private const string EventBlockElement = "EventBlock";
        private const string MovableDirection = "MovableDirection";
        private const string EventElement = "Event";

        private readonly XmlNodeType EndElementType = XmlNodeType.EndElement;
        private readonly XmlNodeType ElementType = XmlNodeType.Element;

        private readonly Dictionary<string, Action<XmlReader, EventInfo>> _eventMap = new Dictionary<string, Action<XmlReader, EventInfo>>()
        {
            { TriggerXElement, (reader, evt) => evt.TriggerX = reader.ReadElementContentAsInt() },
            { TriggerYElement, (reader, evt) => evt.TriggerY = reader.ReadElementContentAsInt() },
            { DoorDirectionElement, (reader, evt) => evt.DoorDirectionToOpen = reader.ReadElementContentAsString() },
        };

        private readonly Dictionary<string, Action<XmlReader, BlockInfo>> _blockMap = new Dictionary<string, Action<XmlReader, BlockInfo>>()
        {
            { NameElement, (reader, block) =>  block.Name = reader.ReadElementContentAsString() },
            { StartXElement, (reader, block) => block.StartX = reader.ReadElementContentAsInt() },
            { StartYElement, (reader, block) => block.StartY = reader.ReadElementContentAsInt() },
            { EndXElement, (reader, block) => block.EndX = reader.ReadElementContentAsInt() },
            { EndYElement, (reader, block) => block.EndY = reader.ReadElementContentAsInt() },
            { MovableDirection, (reader, block) => block.MovableDirection = reader.ReadElementContentAsString() }
        };

        /// <summary>
        /// Parses information in an event info element
        /// </summary>
        /// <param name="reader">The reader</param>
        private EventInfo ParseEventInfo(XmlReader reader)
        {
            EventInfo evt = new EventInfo();
            while (reader.Read())
            {
                if (reader.NodeType == ElementType && _eventMap.TryGetValue(reader.Name, out var eventMap))
                {
                    eventMap(reader, evt);
                }
                else if (reader.NodeType == EndElementType && reader.Name == EventInfoElement)
                {
                    break;
                }
            }
            return evt;
        }

        /// <summary>
        /// Parses an event block that is movable. 
        /// </summary>
        /// <param name="reader">The current xml reader</param>
        /// <param name="blockInfo">The struct where all the information should be saved</param>
        private BlockInfo ParseEventBlock(XmlReader reader)
        {
            BlockInfo blockInfo = new BlockInfo();
            while (reader.Read())
            {
                if (reader.NodeType == ElementType && _blockMap.TryGetValue(reader.Name, out var blockMap))
                {
                    blockMap(reader, blockInfo);
                }
                else if (reader.NodeType == EndElementType && reader.Name == EventBlockElement)
                {
                    break;
                }
            }
            return blockInfo;
        }

        /// <summary>
        /// Creates a vector with the given elements
        /// </summary>
        /// <param name="x">the x position</param>
        /// <param name="y">the y position</param>
        /// <returns>a new instance of a vector</returns>
        private Vector2 CreateVector(int x, int y)
        {
            return new Vector2(x, y);
        }

        private IMovableEntity CreateMovableBlock(BlockInfo block)
        {
            ISprite blockSprite = TileSpriteFactory.Instance.CreateNewTileSprite(block.Name);
            Vector2 startPos = CreateVector(block.StartX, block.StartY);
            Vector2 endPos = CreateVector(block.EndX, block.EndY);
            Direction moveDirection = (Direction)Enum.Parse(typeof(Direction), block.MovableDirection, true);
            return new MovableBlock(blockSprite, startPos, endPos, moveDirection);
        }

        /// <summary>
        /// Parse an event that 
        /// </summary>
        /// <param name="roomWithEvent"></param>
        /// <param name="reader"></param>
        public void ParseOpenDoorWithBlockEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventStruct = ParseEventInfo(reader);
            BlockInfo blockStruct = ParseEventBlock(reader);
            IMovableEntity blockEntity = CreateMovableBlock(blockStruct);
            Vector2 triggerPosition = CreateVector(eventStruct.TriggerX, eventStruct.TriggerY);
            Direction doorToOpenDirection = (Direction)Enum.Parse(typeof(Direction), eventStruct.DoorDirectionToOpen, true);
            IRoomEvent roomEvent = new OpenDoorWithBlockEvent(roomWithEvent, blockEntity, triggerPosition, doorToOpenDirection);
            roomWithEvent.AddRoomEvent(roomEvent);
            roomWithEvent.AddArchitecturalEntity(blockEntity);
        }
    }
}
