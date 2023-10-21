﻿using SprintZero1.Entities;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SprintZero1.Factories;
using SprintZero1.Managers;
using System.IO;
using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        
       
        public XMLParser(Game1 game)
        {
           
        }

        public void Parse(String fileName)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            using (XmlReader reader = new XmlTextReader(workingDirectory + "/" + fileName))
            {
                while (reader.Read()) 
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        Debug.WriteLine(reader.Name);
                        switch (reader.Name)
                        {
                            case "Floor":
                                ParseFloor(reader);
                                break;
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
                                break;
                        }
                    }
                }
            }

            
        }

        private void ParseFloor(XmlReader reader)
        {
            string name = "";
            int X = 0;
            int Y = 0;
            bool keepLooping = true;
            while (reader.Read() && keepLooping)
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
                            
                            keepLooping = false;
                            break;
                    }

                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Floor")
                {

                    Vector2 pos = new Vector2(X, Y);

                    ISprite newFloorSprite = TileSpriteFactory.Instance.CreateFloorSprite(name);
                    Entity floor = new LevelBLockEntity(newFloorSprite, pos, false);

                    if (floor != null)
                    {
                        ProgramManager.AddOnScreenEntity(floor);
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
            bool keepLooping = true;
            while (reader.Read() && keepLooping)
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
                            Vector2 pos = new Vector2(X, Y);

                            ISprite newblockSprite = TileSpriteFactory.Instance.CreateNewTileSprite(name);
                            Entity block = new LevelBLockEntity(newblockSprite, pos, isCollidable);

                            if (block != null)
                            {
                                ProgramManager.AddOnScreenEntity(block);
                            }
                            keepLooping = false;
                            break;
                    }
                   
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Block")
                {

                    

                }
            }

        }

        private void ParseWall(XmlReader reader)
        {
            int quad = 0;
            int X = 0;
            int Y = 0;
           
            bool keepLooping = true;
            while (reader.Read() && keepLooping)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Quad":
                            quad = reader.ReadElementContentAsInt();
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
                           
                            keepLooping = false;
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Wall")
                {
                    //parse the data -> get the sprites draw the thing entity
                    Vector2 pos = new Vector2(X, Y);
                    ISprite newblockSprite = TileSpriteFactory.Instance.CreateNewWallSprite(quad);
                    Entity block = new LevelBLockEntity(newblockSprite, pos, true);

                    if (block != null)
                    {
                        ProgramManager.AddOnScreenEntity(block);
                    }


                }
            }

        }

        private void ParseDoor(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;
            string type = "";
            bool keepLooping = true;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    //Checks what entity is current
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

                            keepLooping = false;
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Door")
                {
                    //parse the data -> get the sprites draw the thing entity
                    Vector2 pos = new Vector2(X, Y);
                    ISprite newDoorSprite = TileSpriteFactory.Instance.CreateNewTileSprite(name);
                    Entity Door = new LevelBLockEntity(newDoorSprite, pos, false);
                    if (Door != null)
                    {
                        ProgramManager.AddOnScreenEntity(Door);
                    }
                }
            }

        }

        private void ParseEnemy(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;

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
                   
                    ISprite enemySprite = EnemyFactory.Instance.CreateEnemySprite(name, new Vector2(X,Y), 0);
                     Entity enemy = new LevelBLockEntity(enemySprite, new Vector2(X, Y), false);
                    if (enemy != null)
                    {
                        ProgramManager.AddOnScreenEntity(enemy);
                    }
                }
            }

        }

        private void ParseItem(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;

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
                    ISprite itemSprite = ItemFactory.Instance.CreateItemSprite(name);
                    Entity item = new LevelBLockEntity(itemSprite, new Vector2(X,Y), false);
                    if (item != null)
                    {
                        ProgramManager.AddOnScreenEntity(item);
                    }
                }
            }

        }
    }
}
