using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.Managers;
using SprintZero1.Sprites;
using System;
using System.IO;
using System.Xml;
using SprintZero1.Controllers.EnemyControllers;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        public XMLParser() { }

        public void Parse(String fileName)
        {
            String workingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            using (XmlReader reader = new XmlTextReader(workingDirectory + "/" + fileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
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
                    IEntity floor = new BackgroundSpriteEntity(newFloorSprite, pos);

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
                            IEntity block = new LevelBlockEntity(newblockSprite, pos);

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
                    Vector2 hitbox = new Vector2(1, 1);
                    ISprite newWallSprite = TileSpriteFactory.Instance.CreateNewWallSprite(quad);
                    IEntity wall = new LevelBlockEntity(newWallSprite, pos);

                    if (wall != null)
                    {
                        ProgramManager.AddOnScreenEntity(wall);
                    }


                }
            }

        }

        private void ParseDoor(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;
            string type = "";
            int destination = -1;
            bool keepLooping = true;
            while (reader.Read() && keepLooping)
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
                        case "Destination":
                            destination = reader.ReadElementContentAsInt();
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
                    IEntity Door = new LevelDoorEntity(newDoorSprite, pos, destination);
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
            int X = 0, Y = 0, health = 0, frames = 0;
            bool isBoss = false;
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
                        case "Health":
                            health = reader.ReadElementContentAsInt();
                            break;
                        case "Frames":
                            frames = reader.ReadElementContentAsInt();
                            break;
                        case "IsBoss":
                            isBoss = reader.ReadElementContentAsBoolean();
                            break;
                        default:
                            //not needed really since we write the xml files
                            //report error in xml file
                            keepLooping = false;
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Enemy")
                {
                    //parse the data -> get the sprites draw the thing entity


                    ICombatEntity enemy = new EnemyEntityWithDirection(new Vector2(X, Y), health, name, isBoss);
                    if (enemy != null)
                    {
                        ProgramManager.AddOnScreenEntity(enemy);
                        IEnemyMovementController movementController = new SmartEnemyMovementController(enemy, ProgramManager.Player);
                        ProgramManager.AddOnScreenEnemyController(movementController);
                    }
                    
                    /* 
                     * what to use for adding enemies in xml file skeleton
                     * <Enemy>
                     *  <Name></Name>
                     *  <X></X>
                     *  <Y></Y>
                     *  <Health></Health>
                     *  <Frames></Frames>
                     *  <IsBoss></IsBoss>
                     * </Enemy>
                     */
                }
            }

        }

        private void ParseItem(XmlReader reader)
        {
            string name = "";
            int X = 0, Y = 0;
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
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Item")
                {
                    //parse the data -> get the sprites draw the thing entity
                    ISprite itemSprite = ItemSpriteFactory.Instance.CreateAnimatedItemSprite(name);
                    IEntity item = new LevelBlockEntity(itemSprite, new Vector2(X, Y));
                    if (item != null)
                    {
                        ProgramManager.AddOnScreenEntity(item);
                    }
                }
            }

        }
    }
}
