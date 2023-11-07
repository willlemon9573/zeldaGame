using SprintZero1.Entities;
using SprintZero1.Managers;
using SprintZero1.XMLParsers.XMLEntityBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SprintZero1.XMLParsers
{
    public class LevelXMLParser
    {
        private Dictionary<string, Action<XmlReader>> _outerElements;
        private Dictionary<string, Action<XmlReader, EntityBase>> _innerElements;
        private const string WALL_ELEMENT = "Walls";
        private const string BLOCK_ELEMENT = "Blocks";
        private const string DOOR_ELEMENT = "Doors";
        private const string FLOOR_ELEMENT = "Floor";
        private const string ENEMY_ELEMENT = "Enemies";
        private const string ITEM_ELEMENT = "Item";

        private XmlReader reader;
        private readonly XmlNodeType END_ELEMENT_TYPE = XmlNodeType.EndElement;
        private readonly XmlNodeType ELEMENT_TYPE = XmlNodeType.Element;
        public XmlReader XMLTextReader { get; private set; }

        public LevelXMLParser()
        {
            _outerElements = new Dictionary<string, Action<XmlReader>>() {
            { WALL_ELEMENT, val => ParseWall(reader)},
            { FLOOR_ELEMENT, val => ParseFloor(reader)},
            { BLOCK_ELEMENT, val => ParseBlock(reader)},
            { DOOR_ELEMENT, val => ParseDoor(reader)},
            { ENEMY_ELEMENT, val => ParseEnemy(reader)},
            { ITEM_ELEMENT, val => ParseItem(reader)}
           };


            Dictionary<string, Action<XmlReader, Type>>

            _innerElements = new Dictionary<string, Action<XmlReader, EntityBase>>() {
            { "X", (x, data) => data.X = x.ReadElementContentAsInt() },
            { "Y", (y, data) => data.Y = y.ReadElementContentAsInt() },
            { "Name", (name, data) => data.Name = name.ReadElementContentAsString() },
            { "Destination", (x, data) => data.DestinationOrHealth = x.ReadElementContentAsInt()},
            { "Frame", (x, data) => data = x.ReadElementContentAsInt() },
            };
        }

        public void Parse(string fileName)
        {
            string workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/" + fileName;

            reader = XmlReader.Create(workingDirectory);

            while (reader.Read())
            {
                if (reader.NodeType == ELEMENT_TYPE && _outerElements.ContainsKey(reader.Name))
                {
                    _outerElements[reader.Name].Invoke(reader);
                }
            }

            reader.Close();
        }

        private void ParseFloor(XmlReader reader)
        {
            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Floor")
                {
                    break;
                }
            }
            IEntity floor = data.AddFloorToGame();
            ProgramManager.AddOnScreenEntity(floor);
        }
        private void ParseBlock(XmlReader reader)
        {
            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Block")
                {
                    //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
                    IEntity block = data.AddBlockToGame();
                    ProgramManager.AddOnScreenEntity(block);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Blocks")
                {
                    break;
                }
            }

        }

        private void ParseWall(XmlReader reader)
        {
            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Wall")
                {
                    //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
                    IEntity wall = data.AddWallToGame();
                    ProgramManager.AddOnScreenEntity(wall);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Walls")
                {
                    break;
                }
            }
        }

        private void ParseDoor(XmlReader reader)
        {
            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Door")
                {
                    //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
                    IEntity door = data.AddDoorToGame();
                    ProgramManager.AddOnScreenEntity(door);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Doors")
                {
                    break;
                }
            }
        }

        private void ParseEnemy(XmlReader reader)
        {

            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Enemy")
                {
                    //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
                    IEntity enemy = data.AddEnemyToGame();
                    ProgramManager.AddOnScreenEntity(enemy);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Enemies")
                {
                    break;
                }
            }
        }

        private void ParseItem(XmlReader reader)
        {

            EntityBase data = new BlockEntityBuilder(0, 0, "", 0, 0, 0);
            while (reader.Read())
            {
                var element_name = reader.Name;
                var reader_type = reader.NodeType;

                if (reader_type == ELEMENT_TYPE && _innerElements.TryGetValue(element_name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Item")
                {
                    //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
                    IEntity item = data.AddItemToGame();
                    ProgramManager.AddOnScreenEntity(item);
                }
                else if (reader_type == END_ELEMENT_TYPE && element_name == "Items")
                {
                    break;
                }
            }
        }
    }
}
