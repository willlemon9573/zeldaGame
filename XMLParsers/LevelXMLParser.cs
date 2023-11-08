using SprintZero1.LevelFiles;
using SprintZero1.XMLParsers.XMLEntityBuilder;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SprintZero1.XMLParsers
{
    internal class LevelXMLParser
    {
        /* ------------------------------Private Members--------------------------------- */
        private Dictionary<string, Action<XmlReader, DungeonRoom>> _outerElements;
        private Dictionary<string, Action<XmlReader, IEntityParsingBuilder>> _innerElements;
        private readonly XmlNodeType END_ELEMENT_TYPE = XmlNodeType.EndElement;
        private readonly XmlNodeType ELEMENT_TYPE = XmlNodeType.Element;
        /* ------------------------------Constants--------------------------------- */
        private const string OUTER_WALL_ELEMENT = "Walls";
        private const string OUTER_BLOCK_ELEMENT = "Blocks";
        private const string OUTER_DOOR_ELEMENT = "Doors";
        private const string OUTER_FLOOR_ELEMENT = "Floor";
        private const string OUTER_ENEMIES_ELEMENT = "Enemies";
        private const string OUTER_ITEM_ELEMENT = "Items";
        private const string INNER_X_ELEMENT = "X";
        private const string INNER_Y_ELEMENT = "Y";
        private const string INNER_NAME_ELEMENT = "Name";
        private const string INNER_DOOR_POINT_X_ELEMENT = "PointX";
        private const string INNER_DOOR_POINT_Y_ELEMENT = "PointY";
        private const string INNER_HEALTH_ELEMENT = "Health";
        private const string INNER_FRAMES_ELEMENT = "Frames";
        /* ------------------------------Public Functions--------------------------------- */
        /// <summary>
        /// Constructor for a new instance of LevelXmlParser
        /// </summary>
        public LevelXMLParser()
        {
            // set up dictionary for outer elements
            _outerElements = new Dictionary<string, Action<XmlReader, DungeonRoom>>() {
            { OUTER_WALL_ELEMENT, (reader, room) => ParseWall(reader, room)},
            { OUTER_FLOOR_ELEMENT, (reader, room) => ParseFloor(reader, room)},
            { OUTER_BLOCK_ELEMENT, (reader, room) => ParseBlock(reader, room)},
            { OUTER_DOOR_ELEMENT, (reader, room) => ParseDoor(reader, room)},
            { OUTER_ENEMIES_ELEMENT, (reader, room) => ParseEnemy(reader, room)},
            { OUTER_ITEM_ELEMENT, (reader, room) => ParseItem(reader, room)}
           };
            // set up dictionary for inner elements
            _innerElements = new Dictionary<string, Action<XmlReader, IEntityParsingBuilder>>() {
            { INNER_X_ELEMENT, (x, data) => data.EntityPositionX = x.ReadElementContentAsInt() },
            { INNER_Y_ELEMENT, (y, data) => data.EntityPositionY = y.ReadElementContentAsInt() },
            { INNER_NAME_ELEMENT, (name, data) => data.EntityName = name.ReadElementContentAsString() },
            { INNER_DOOR_POINT_X_ELEMENT, (x, data) => (data as XMLDoorEntity).DestPointX = x.ReadElementContentAsInt() },
            { INNER_DOOR_POINT_Y_ELEMENT, (y, data) => (data as XMLDoorEntity).DestPointY = y.ReadElementContentAsInt() },
            { INNER_HEALTH_ELEMENT, (health, data) =>  (data as XMLEnemyEntity).EntityHealth = health.ReadElementContentAsInt()  },
            { INNER_FRAMES_ELEMENT, (frames, data) => (data as XMLEnemyEntity).EntityFrames = frames.ReadElementContentAsInt() }
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
                if (reader.NodeType == ELEMENT_TYPE && _outerElements.ContainsKey(reader.Name))
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

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var action))
                {
                    action(reader, floor);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == floorElement)
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

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, block);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == innerBlockElement)
                {
                    dungeonRoom.AddArchitecturalEntity(block.CreateEntity());
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == OUTER_BLOCK_ELEMENT)
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

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, wall);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == innerWallElement)
                {
                    dungeonRoom.AddArchitecturalEntity(wall.CreateEntity());
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == OUTER_WALL_ELEMENT)
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

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, door);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == innerDoorElement)
                {
                    dungeonRoom.AddArchitecturalEntity(door.CreateEntity());
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == OUTER_DOOR_ELEMENT)
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
            string innerBossElement = "Boss";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, enemy);
                }
                else if (reader_type == END_ELEMENT_TYPE && (element_name == innerEnemyElement || element_name == innerBossElement))
                {
                    if (element_name == innerBossElement)
                    {
                        dungeonRoom.AddEnemy((enemy as XMLEnemyEntity).CreateBossEntity());
                    }
                    else
                    {
                        dungeonRoom.AddEnemy(enemy.CreateEntity());
                    }
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Enemies")
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
            string innerAnimatedItemElement = "AnimatedElement";
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, item);
                }
                else if (reader_type == END_ELEMENT_TYPE && (element_name == innerItemElement || element_name == innerAnimatedItemElement))
                {
                    if (innerItemElement == innerAnimatedItemElement)
                    {
                        dungeonRoom.AddArchitecturalEntity((item as XMLItemEntity).CreateAnimatedEntity());
                    }
                    else
                    {
                        dungeonRoom.AddArchitecturalEntity(item.CreateEntity());
                    }
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == OUTER_ITEM_ELEMENT)
                {
                    break;
                }
            }
        }
    }
}
