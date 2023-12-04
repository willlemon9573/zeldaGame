using SprintZero1.LevelFiles;
using SprintZero1.XMLParsers.XMLEntityBuilder;
using SprintZero1.XMLParsers.XMLEntityBuilder.EventParser;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SprintZero1.XMLParsers
{
    internal class LevelXMLParser
    {
        /* ------------------------------Private Members--------------------------------- */
        private readonly Dictionary<string, Action<XmlReader, DungeonRoom>> _outerElements;
        private readonly Dictionary<string, Action<XmlReader, IEntityParsingBuilder>> _innerElements;
        private readonly XmlNodeType EndElementType = XmlNodeType.EndElement;
        private readonly XmlNodeType ElementType = XmlNodeType.Element;
        /* ------------------------------Constants--------------------------------- */
        private const string OuterWallsElement = "Walls";
        private const string OuterBlocksElement = "Blocks";
        private const string OuterDoorsElement = "Doors";
        private const string OutFloorElement = "Floor";
        private const string OuterEnemiesElement = "Enemies";
        private const string OuterItemsElement = "Items";
        private const string OuterEventsElement = "Events";
        private const string InnerXPositionElement = "X";
        private const string InnerYPositionElement = "Y";
        private const string InnerNameElement = "Name";
        private const string InnerDoorDestinationElement = "Destination";
        private const string InnerDoorDirectionElement = "Direction";
        private const string InnerDoorTypeElement = "DoorType";
        private const string InnerElementHealthElement = "Health";
        private const string InnerItemFramesElement = "ItemFrames";
        private const string InnerItemEnumElement = "Enum";
        private const string InnerItemTypeElement = "Type";
        private const string InnerBossBoundaryElement = "Boundary";
        /* ------------------------------Public Functions--------------------------------- */
        /// <summary>
        /// Constructor for a new instance of LevelXmlParser
        /// </summary>
        public LevelXMLParser()
        {
            // set up dictionary for outer elements
            _outerElements = new Dictionary<string, Action<XmlReader, DungeonRoom>>() {
            { OuterWallsElement, (reader, room) => ParseWall(reader, room)},
            { OutFloorElement, (reader, room) => ParseFloor(reader, room)},
            { OuterBlocksElement, (reader, room) => ParseBlock(reader, room)},
            { OuterDoorsElement, (reader, room) => ParseDoor(reader, room)},
            { OuterEnemiesElement, (reader, room) => ParseEnemy(reader, room)},
            { OuterItemsElement, (reader, room) => ParseItem(reader, room)},
            { OuterEventsElement, (reader, room) => ParseEvent(reader, room)},
           };

            // set up dictionary for inner elements
            _innerElements = new Dictionary<string, Action<XmlReader, IEntityParsingBuilder>>() {
            { InnerXPositionElement, (x, data) => data.EntityPositionX = x.ReadElementContentAsInt() },
            { InnerYPositionElement, (y, data) => data.EntityPositionY = y.ReadElementContentAsInt() },
            { InnerNameElement, (name, data) => data.EntityName = name.ReadElementContentAsString() },
            { InnerItemFramesElement, (item, data) => (data as XMLItemEntity).ItemFrames = item.ReadElementContentAsInt() },
            { InnerItemTypeElement, (item, data) => (data as XMLItemEntity).ItemType = item.ReadElementContentAsString() },
            { InnerItemEnumElement, (item, data) => (data as XMLItemEntity).EnumName = item.ReadElementContentAsString() },
            { InnerElementHealthElement, (enemy, data) =>  (data as XMLEnemyEntity).EntityHealth = enemy.ReadElementContentAsFloat() },
            { InnerDoorDestinationElement, (door, data) => (data as XMLDoorEntity).Destination = door.ReadElementContentAsString() },
            { InnerDoorDirectionElement, (door, data) => (data as XMLDoorEntity).DoorDirection = door.ReadElementContentAsString() },
            { InnerDoorTypeElement,  (door, data) => (data as XMLDoorEntity).DoorType = door.ReadElementContentAsString() },
              { InnerBossBoundaryElement, (enemy, data) => (data as XMLEnemyEntity).ParseBossBoundary(enemy)}
            };
        }

        /// <summary>
        /// Parse The given file and return 
        /// </summary>
        /// <param name="path">The path to the file being parsed</param>
        public DungeonRoom Parse(string path)
        {
            DungeonRoom dungeonRoom = new DungeonRoom();
            XmlReader reader = XmlReader.Create(path);

            while (reader.Read())
            {
                if (reader.NodeType == ElementType && _outerElements.ContainsKey(reader.Name))
                {
                    _outerElements[reader.Name].Invoke(reader, dungeonRoom);
                }
            }

            reader.Close();
            return dungeonRoom;
        }
        /// <summary>
        /// Parses the floor textures required for the room
        /// </summary>
        /// <param name="reader">The xml reader</param>
        /// <param name="dungeonRoom">the dungeon room the floor is added to</param>
        private void ParseFloor(XmlReader reader, DungeonRoom dungeonRoom)
        {
            IEntityParsingBuilder floor = new XMLFloorEntity();
            string floorElement = "Floor";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var action))
                {
                    action(reader, floor);
                }
                else if (reader_type == EndElementType && element_name == floorElement)
                {
                    break;
                }
            }
            dungeonRoom.RoomName = (floor as XMLFloorEntity).GetName();
            dungeonRoom.AddArchitecturalEntity(floor.CreateEntity());
        }

        /// <summary>
        /// Parses the list of blocks from the xml file and adds them all to the list of entities in the dungeon room
        /// </summary>
        /// <param name="reader">the xml reader</param>
        /// <param name="dungeonRoom">the dungeon room to add the blocks to</param>
        private void ParseBlock(XmlReader reader, DungeonRoom dungeonRoom)
        {
            IEntityParsingBuilder block = new XMLBlockEntity();
            string innerBlockElement = "Block";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, block);
                }
                else if (reader_type == EndElementType && element_name == innerBlockElement)
                {
                    dungeonRoom.AddArchitecturalEntity(block.CreateEntity());
                }
                else if (reader_type == EndElementType && element_name == OuterBlocksElement)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Parses the list of blocks from the xml file and adds them all to the list of entities in the dungeon room
        /// </summary>
        /// <param name="reader">the xml reader</param>
        /// <param name="dungeonRoom">the dungeon room to add the blocks to</param>
        private void ParseWall(XmlReader reader, DungeonRoom dungeonRoom)
        {
            IEntityParsingBuilder wall = new XMLWallEntity();
            string innerWallElement = "Wall";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, wall);
                }
                else if (reader_type == EndElementType && element_name == innerWallElement)
                {
                    dungeonRoom.AddArchitecturalEntity(wall.CreateEntity());
                }
                else if (reader_type == EndElementType && element_name == OuterWallsElement)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Parses the individual doors that will appear in the level
        /// </summary>
        /// <param name="reader">the xml reader</param>
        /// <param name="dungeonRoom">The room to add the doors to</param>
        private void ParseDoor(XmlReader reader, DungeonRoom dungeonRoom)
        {
            IEntityParsingBuilder door = new XMLDoorEntity();
            string innerDoorElement = "Door";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, door);
                }
                else if (reader_type == EndElementType && element_name == innerDoorElement)
                {
                    dungeonRoom.AddArchitecturalEntity(door.CreateEntity());
                }
                else if (reader_type == EndElementType && element_name == OuterDoorsElement)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Parses the list of enemies  from the xml file and adds them all to the list of entities in the dungeon room
        /// </summary>
        /// <param name="reader">the xml reader</param>
        /// <param name="dungeonRoom">the dungeon room to add the enemies to</param>
        private void ParseEnemy(XmlReader reader, DungeonRoom dungeonRoom)
        {
            IEntityParsingBuilder enemy = new XMLEnemyEntity();
            string innerEnemyElement = "Enemy";
            string innerEnemyElementWithProjectile = "EnemyWithProjectile";
            string innerBossElement = "Boss";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, enemy);
                }
                else if (reader_type == EndElementType && (element_name == innerEnemyElement || element_name == innerBossElement || element_name == innerEnemyElementWithProjectile))
                {
                    if (element_name == innerBossElement)
                    {
                        dungeonRoom.AddEnemy((enemy as XMLEnemyEntity).CreateBossEntity(dungeonRoom.RemoveDeadEnemies));
                    }
                    else if (element_name == innerEnemyElementWithProjectile)
                    {
                        dungeonRoom.AddEnemy((enemy as XMLEnemyEntity).CreateEntityWithprojectile());
                    }
                    else
                    {
                        dungeonRoom.AddEnemy(enemy.CreateEntity());
                    }
                }
                else if (reader_type == EndElementType && element_name == "Enemies")
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Parses the list of blocks from the xml file and adds them all to the list of entities in the dungeon room
        /// </summary>
        /// <param name="reader">the xml reader</param>
        /// <param name="dungeonRoom">the dungeon room to add the items to</param>
        private void ParseItem(XmlReader reader, DungeonRoom dungeonRoom)
        {
            /* using block temporarily until we create an entity for items */
            IEntityParsingBuilder item = new XMLItemEntity();
            string innerItemElement = "Item";
            string innerAnimatedItemElement = "AnimatedItem";

            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ElementType && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, item);
                }
                else if (reader_type == EndElementType && (element_name == innerItemElement || element_name == innerAnimatedItemElement))
                {
                    if (element_name == innerAnimatedItemElement)
                    {
                        dungeonRoom.AddArchitecturalEntity((item as XMLItemEntity).CreateAnimatedEntity());
                    }
                    else
                    {
                        (item as XMLItemEntity).RemoveDelegateHandler = dungeonRoom.RemoveAndSaveItem;
                        dungeonRoom.AddRoomItem(item.CreateEntity());
                    }
                }
                else if (reader_type == EndElementType && element_name == OuterItemsElement)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Parses all Events that would be found in a single XML File
        /// </summary>
        /// <param name="reader">The XML Reader</param>
        /// <param name="dungeonRoom">The current dungeon room being created</param>
        private void ParseEvent(XmlReader reader, DungeonRoom dungeonRoom)
        {
            EventParser parser = new EventParser();
            string innerEventElement = "Event";
            string innerNameElement = "Name";
            string eventOne = "OpenDoorWithMovableBlock";
            string eventFive = "OpenPathWithBlockEvent";
            string eventTwo = "RoomBeatKeyEvent";
            string eventThree = "RoomBeatBoomerangEvent";
            string eventFour = "RoomBeatOpenDoorEvent";
            Dictionary<string, Action<XmlReader, DungeonRoom>> eventMap = new Dictionary<string, Action<XmlReader, DungeonRoom>>()
            {
                { eventOne, (reader, room) => parser.ParseOpenDoorWithBlockEvent(room, reader) },
                { eventTwo, (reader, room) => parser.ParseRoomBeatKeyEvent(room, reader) },
                { eventThree, (reader, room) => parser.ParseRoomBeatBoomerangEvent(room, reader) },
                { eventFour, (reader, room) => parser.ParseRoomBeatOpenDoorEvent(room, reader) },
                { eventFive, (reader, room) => parser.ParseOpenPathWithBlockEvent(room, reader) },
            };

            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;
                if (reader_type == ElementType && element_name == innerNameElement && eventMap.TryGetValue(reader.ReadElementContentAsString(), out var eventFunction))
                {
                    eventFunction(reader, dungeonRoom);
                }
                else if (reader_type == EndElementType && element_name == innerEventElement)
                {
                    break;
                }
            }
        }
    }
}