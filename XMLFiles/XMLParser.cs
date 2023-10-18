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
using SprintZero1.Managers;
using System.IO;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        public TileSpriteFactory tileSpriteFactory;
        public EnemyFactory enemyFactory;
        public ItemFactory itemFactory;
        
       
        public XMLParser(Game1 game)
        {
            tileSpriteFactory = new TileSpriteFactory();
            enemyFactory = new EnemyFactory();
            itemFactory = new ItemFactory();
           
        }

        public void Parse(String fileName)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            using (XmlReader reader = new XmlTextReader(workingDirectory + "/" + fileName))
            {
                while (reader.Read()) //error here
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
                    Vector2 pos = new Vector2(X, Y);
                    ISprite newblockSprite = tileSpriteFactory.CreateNewTileSprite(name);
                    Entity block = new LevelBLockEntity(newblockSprite, pos, isCollidable);
                    if (block != null) {
                        ProgramManager.AddOnScreenEntity(block);
                    }
                    

                }
            }

        }

        private void ParseWall(XmlReader reader)
        {
            int quad = 0;
            int X = 0;
            int Y = 0;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Name":
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
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Wall")
                {
                    //parse the data -> get the sprites draw the thing entity
                    Vector2 pos = new Vector2(X, Y);
                    ISprite newWallSprite = tileSpriteFactory.CreateNewWallSprite(quad);
                }
            }

        }

        private void ParseDoor(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;
            string type = "";

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
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Door")
                {
                    //parse the data -> get the sprites draw the thing entity
                    Vector2 pos = new Vector2(X, Y);
                    ISprite newDoorSprite = tileSpriteFactory.CreateNewDoorSprite(name);
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
                   
                    ISprite enemySprite = enemyFactory.CreateEnemySprite(name, new Vector2(X,Y), 0);
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
                    ISprite itemSprite = itemFactory.CreateItemSprite(name);
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
