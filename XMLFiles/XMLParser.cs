using SprintZero1.Entities;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SprintZero1.Factories;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        public XMLParser(Game1 game)
        {



        }

        public void Parse(String fileName)
        {
            using (XmlReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Block":
                                ParseBlock(reader);
                                break;
                            case "Wall":
                                ParseWall(reader);
                                break;
                            case "Door":
                                ParseDoor(reader);
                                break;
                            case "Enemy":
                                ParseEnemy(reader);
                                break;
                            case "Item":
                                ParseItem(reader);
                                break;
                            default:
                                //error in xmlfile
                                break;
                        }
                    }
                }
            }


        }

        private void ParseBlock(XmlReader reader)
        {
            string name = "";
            int X = 0;
            int Y = 0;
            bool isCollidable = false;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "X":
                            X = reader.ReadElementContentAsInt();
                            break;
                        case "Y":
                            Y = reader.ReadElementContentAsInt();
                            break;
                        case "IsCollidable":
                            isCollidable = reader.ReadElementContentAsBoolean();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Block")
                {
                    //parse the data -> get the sprites draw the thing entity
                }
            }

        }

        private void ParseWall(XmlReader reader)
        {
            string name = "";
            int X = 0;
            int Y = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "X":
                            X = reader.ReadElementContentAsInt();
                            break;
                        case "Y":
                            Y = reader.ReadElementContentAsInt();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Wall")
                {
                    //parse the data -> get the sprites draw the thing entity
                }
            }

        }

        private void ParseDoor(XmlReader reader)
        {
            string name = "";
            int X, Y = 0;
            string type = "";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "X":
                            X = reader.ReadElementContentAsInt();
                            break;
                        case "Y":
                            Y = reader.ReadElementContentAsInt();
                            break;
                        case "Type":
                            type = reader.ReadElementContentAsString();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Door")
                {
                    //parse the data -> get the sprites draw the thing entity
                }
            }

        }

        private void ParseEnemy(XmlReader reader)
        {
            string name = "";
            int X, Y = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "X":
                            X = reader.ReadElementContentAsInt();
                            break;
                        case "Y":
                            Y = reader.ReadElementContentAsInt();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Enemy")
                {
                    //parse the data -> get the sprites draw the thing entity
                }
            }

        }

        private void ParseItem(XmlReader reader)
        {
            string name = "";
            int X, Y = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
                            name = reader.ReadElementContentAsString();
                            break;
                        case "X":
                            X = reader.ReadElementContentAsInt();
                            break;
                        case "Y":
                            Y = reader.ReadElementContentAsInt();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Item")
                {
                    //parse the data -> get the sprites draw the thing entity
                }
            }

        }
    }
}
