using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Xml;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        private Dictionary<string, Action<XmlReader>> _outerElements;
        private Dictionary<string, Action<XmlReader, Node>> _innerElements;
        private const string WALL_ELEMENT = "Walls";
        private const string BLOCK_ELEMENT = "Blocks";
        private const string DOOR_ELEMENT = "Doors";
        private const string FLOOR_ELEMENT = "Floor";
        private const string ENEMY_ELEMENT = "Enemies";
        private const string ITEM_ELEMENT = "Item";

        private XmlReader reader;
        XmlNodeType END_ELEMENT_TYPE = XmlNodeType.EndElement;
        XmlNodeType ELEMENT_TYPE = XmlNodeType.Element;
        public XmlReader XMLTextReader { get; private set; }



        public XMLParser()
        {
            _outerElements = new Dictionary<string, Action<XmlReader>>() {
            { "Walls", val => ParseWall(reader)},
            { "Floor", val => ParseFloor(reader)},
            { "Blocks", val => ParseBlock(reader)},
            { "Doors", val => ParseDoor(reader)},
            { "Enemies", val => ParseEnemy(reader)},
            { "Items", val => ParseItem(reader)}
           };

            _innerElements = new Dictionary<string, Action<XmlReader, Node>>() {
            { "X", (x, node) => node.X = x.ReadElementContentAsInt() },
            { "Y", (y, node) => node.Y = y.ReadElementContentAsInt() },
            { "Name", (name, node) => node.Name = name.ReadElementContentAsString() },
            { "Destination", (x, node) => node.DestinationOrHealth = x.ReadElementContentAsInt()},
            { "Frame", (x, node) => node.frame = x.ReadElementContentAsInt() },
            { "IsBoss", (x, node) => node.isBoss = x.ReadElementContentAsInt() }

                };
        }

        public void Parse(String fileName)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/" + fileName;

            reader = XmlReader.Create(workingDirectory);

            while (reader.Read())
            {
                if (reader.NodeType == ELEMENT_TYPE && _outerElements.ContainsKey(reader.Name))
                {
                    var readerName = reader.Name;
                    _outerElements[readerName].Invoke(reader);
                }
            }

            reader.Close();
        }

        private void ParseFloor(XmlReader reader)
        {
            Node data = new Node(0, 0, "", 0, 0, 0);
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
            //Debug.WriteLine($"X: {data.X}, Y: {data.Y}, name: {data.Name}");
            IEntity floor = data.AddFloorToGame();
            ProgramManager.AddOnScreenEntity(floor);
        }
        private void ParseBlock(XmlReader reader)
        {
            Node data = new Node(0, 0, "", 0, 0, 0);
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
            Node data = new Node(0, 0, "", 0, 0, 0);
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
            Node data = new Node(0, 0, "", 0, 0, 0);
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

            Node data = new Node(0, 0, "", 0, 0, 0);
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

            Node data = new Node(0, 0, "", 0, 0, 0);
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
