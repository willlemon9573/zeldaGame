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
        private const string EventBlockListElement = "Event";

        private readonly XmlNodeType EndElementType = XmlNodeType.EndElement;
        private readonly XmlNodeType ElementType = XmlNodeType.Element;

        private readonly Dictionary<string, Action<XmlReader, EventInfo>> _eventMap = new Dictionary<string, Action<XmlReader, EventInfo>>()
        {
            { TriggerXElement, (reader, evt) => evt.TriggerX = reader.ReadElementContentAsInt() },
            { TriggerYElement, (reader, evt) => evt.TriggerY = reader.ReadElementContentAsInt() },
            { DoorDirectionElement, (reader, evt) => evt.DoorDirectionToOpen = reader.ReadElementContentAsString() },
        };

        private readonly Dictionary<string, Action<XmlReader, EventInfo>> _eventMapLists = new Dictionary<string, Action<XmlReader, EventInfo>>()
        {
            { TriggerXElement, (reader, evt) => evt.AddToTriggerList(reader.ReadElementContentAsInt(), evt.TriggerY) },
            { TriggerYElement, (reader, evt) => evt.AddToTriggerList(evt.TriggerX, reader.ReadElementContentAsInt()) },
            { DoorDirectionElement, (reader, evt) => evt.AddDirection(reader.ReadElementContentAsString()) },
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

        private EventInfo ParseEventInfoLists(XmlReader reader)
        {
            EventInfo evt = new EventInfo();
            while (reader.Read())
            {
                if (reader.NodeType == ElementType && _eventMapLists.TryGetValue(reader.Name, out var eventMap))
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

        private List<BlockInfo> ParseBlockInfos(XmlReader reader)
        {
            List<BlockInfo> blockList = new List<BlockInfo>();

            while (reader.Read())
            {
                if (reader.NodeType == ElementType && _blockMap.TryGetValue(reader.Name, out var blockMap))
                {
                    BlockInfo block = new BlockInfo();
                    blockMap(reader, block);
                    blockList.Add(block);
                }
                else if (reader.NodeType == EndElementType && reader.Name == EventBlockListElement)
                {
                    break;
                }
            }

            return blockList;
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

        /// <summary>
        /// Creates the movable block for a movable block event room
        /// </summary>
        /// <param name="block">The object that contains information on the block</param>
        /// <returns>A new instance of type movableBlock</returns>
        private IMovableEntity CreateMovableBlock(BlockInfo block)
        {
            ISprite blockSprite = TileSpriteFactory.Instance.CreateNewTileSprite(block.Name);
            Vector2 startPos = CreateVector(block.StartX, block.StartY);
            Vector2 endPos = CreateVector(block.EndX, block.EndY);
            Direction moveDirection = (Direction)Enum.Parse(typeof(Direction), block.MovableDirection, true);
            return new MovableBlock(blockSprite, startPos, endPos, moveDirection);
        }

        /// <summary>
        /// Parses an XML Element that contains the the information to create this event
        /// </summary>
        /// <param name="roomWithEvent">the room that will contain the event</param>
        /// <param name="reader">the current xml reader</param>
        public void ParseOpenDoorWithBlockEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfo(reader);
            BlockInfo blockInfo = ParseEventBlock(reader);
            IMovableEntity blockEntity = CreateMovableBlock(blockInfo);
            Vector2 triggerPosition = CreateVector(eventInfo.TriggerX, eventInfo.TriggerY);
            Direction doorToOpenDirection = (Direction)Enum.Parse(typeof(Direction), eventInfo.DoorDirectionToOpen, true);
            IRoomEvent roomEvent = new OpenDoorWithBlockEvent(roomWithEvent, blockEntity, triggerPosition, doorToOpenDirection);
            roomWithEvent.AddRoomEvent(roomEvent);
            roomWithEvent.AddArchitecturalEntity(blockEntity);
        }

        /// <summary>
        /// Parses an XML Element that contains the the information to create this event
        /// </summary>
        /// <param name="roomWithEvent">the room that will contain the event</param>
        /// <param name="reader">the current xml reader</param>
        public void ParseOpenPathWithBlockEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfo(reader);
            BlockInfo blockInfo = ParseEventBlock(reader);
            IMovableEntity blockEntity = CreateMovableBlock(blockInfo);
            Vector2 triggerPosition = CreateVector(eventInfo.TriggerX, eventInfo.TriggerY);
            IRoomEvent roomEvent = new OpenPathWithBlockEvent(blockEntity, triggerPosition);
            roomWithEvent.AddRoomEvent(roomEvent);
            roomWithEvent.AddArchitecturalEntity(blockEntity);
        }

        /// <summary>
        /// Parses an XML Element that contains the the information to create this event
        /// </summary>
        /// <param name="roomWithEvent">the room that will contain the event</param>
        /// <param name="reader">the current xml reader</param>
        public void ParseRoomBeatKeyEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfo(reader);
            Vector2 dropPosition = CreateVector(eventInfo.TriggerX, eventInfo.TriggerY);
            IRoomEvent roomEvent = new RoomBeatKeyEvent(roomWithEvent, dropPosition);
            roomWithEvent.AddRoomEvent(roomEvent);
        }

        /// <summary>
        /// Parses an XML Element that contains the the information to create this event
        /// </summary>
        /// <param name="roomWithEvent">the room that will contain the event</param>
        /// <param name="reader">the current xml reader</param>
        public void ParseRoomBeatBoomerangEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfo(reader);
            Vector2 dropPosition = CreateVector(eventInfo.TriggerX, eventInfo.TriggerY);
            IRoomEvent roomEvent = new RoomBeatBoomerangEvent(roomWithEvent, dropPosition);
            roomWithEvent.AddRoomEvent(roomEvent);
        }

        /// <summary>
        /// Parses an XML Element that contains the the information to create this event
        /// </summary>
        /// <param name="roomWithEvent">the room that will contain the event</param>
        /// <param name="reader">the current xml reader</param>
        public void ParseRoomBeatOpenDoorEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfo(reader);
            Direction doorDirection = (Direction)Enum.Parse(typeof(Direction), eventInfo.DoorDirectionToOpen, true);
            IRoomEvent roomEvent = new RoomBeatOpenDoorEvent(roomWithEvent, doorDirection);
            roomWithEvent.AddRoomEvent(roomEvent);
        }

        public void ParsePuzzleRoomEvent(DungeonRoom roomWithEvent, XmlReader reader)
        {
            EventInfo eventInfo = ParseEventInfoLists(reader);
            List<BlockInfo> blockList = ParseBlockInfos(reader);
            List<IMovableEntity> movableBlocks = new List<IMovableEntity>();
            foreach(var block in blockList)
            {
                IMovableEntity movableBlock = CreateMovableBlock(block);
                movableBlocks.Add(movableBlock);
            }
            List<String> doorDirections = eventInfo.DoorDirections;
            List<Tuple<int, int>> triggerList = eventInfo.TriggerLocations;
            //convert to List<Vector2>
            List<Vector2> triggerLocations = new List<Vector2>();
            foreach (var tuple in triggerList)
            {
                Vector2 vector2 = new Vector2(tuple.Item1, tuple.Item2);
                triggerLocations.Add(vector2);
            }
            IRoomEvent roomEvent = new DropWithMultipleBlocksEvent(roomWithEvent, movableBlocks,triggerLocations, doorDirections);
            roomWithEvent.AddRoomEvent(roomEvent);
        }
    }
}
