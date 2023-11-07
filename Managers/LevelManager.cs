using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.LevelFiles;
using SprintZero1.XMLParsers;
using System;
using System.Collections.Generic;
using System.IO;

namespace SprintZero1.Managers
{
    public static class LevelManager
    {
        public static Game1 game;

        private static int totalRooms;
        private static int index;
        private static Dictionary<string, DungeonRoom> _dungeonRoomMap = new Dictionary<string, DungeonRoom>();


        public static void Initialize(Game1 game)
        {
            LevelXMLParser parser = new LevelXMLParser();

            string LevelFolderPath = @"XMLFiles/LevelXmlFiles";
            foreach (var filePath in Directory.EnumerateFiles(LevelFolderPath))
            {
                DungeonRoom room = parser.Parse(filePath);
                _dungeonRoomMap.Add(room.RoomName, room);
            }
            ProgramManager.Start(game);
        }

        public static void LoadNewRoom(String xmlFile)
        {
            ProgramManager.RemoveNonPlayerEntities();

        }

        public static void Update(GameTime gameTime)
        {
            ProgramManager.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            ProgramManager.Draw(spriteBatch);
        }
    }
}
