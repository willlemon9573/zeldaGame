using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        private Dictionary<string, Action<XmlReader>> _outerElements;
        private Dictionary<string, Action<XmlReader, Node>> _innerElements;

        private XmlReader x;

        public XmlReader XMLTextReader { get; private set; }



        public XMLParser()
        {
            _outerElements = new Dictionary<string, Action<XmlReader>>() {
                { "Wall", val => ParseWall(x)},
                { "Floor", val => ParseFloor(x)},
                { "Block", val => ParseBlock(x)},
                { "Door", val => ParseBlock(x)},
            };

            _innerElements = new Dictionary<string, Action<XmlReader, Node>>()
            {
                { "X", (x, node) => node.X = x.ReadElementContentAsInt() },
                { "Y", (y, node) => node.Y = y.ReadElementContentAsInt() },
                { "name", (name, node) => node.Name = name.ReadElementContentAsString() }
            };
        }

        public void Parse(String fileName)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/" + fileName;

            x = XmlReader.Create(workingDirectory);
            {
                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && _outerElements.ContainsKey(x.Name))
                    {
                        _outerElements[x.Name].Invoke(x);
                    }
                }
            }
            x.Close();
        }

        private void ParseFloor(XmlReader reader)
        {


        }
        private void ParseBlock(XmlReader reader)
        {
            Node data = new Node(0, 0, string.Empty);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && _innerElements.TryGetValue(reader.Name, out var parsedValue))
                {
                    parsedValue(reader, data);
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Block")
                {
                    break;
                }
            }

        }

        private void ParseWall(XmlReader reader)
        {
            Debug.WriteLine(reader.Name);

        }

        private void ParseDoor(XmlReader reader)
        {

            Debug.WriteLine(reader.Name);
        }

        private void ParseEnemy(XmlReader reader)
        {

            Debug.WriteLine(reader.Name);
        }

        private void ParseItem(XmlReader reader)
        {

            Debug.WriteLine(reader.Name);
        }
    }
}
