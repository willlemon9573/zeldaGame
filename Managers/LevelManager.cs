using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.XMLParsers;
using System;
using System.Diagnostics;
using System.IO;

namespace SprintZero1.Managers
{
    public static class LevelManager
    {
        public static Game1 game;
        public static LevelXMLParser xmlParser;
        private static int totalRooms;
        private static int index;
        //private Dictionary<Point, LevelFiles>
        public static void Initialize(Game1 game)
        {
            xmlParser = new LevelXMLParser();

            string path = @"XMLFiles\LevelXMLFiles";
            foreach (string f in Directory.EnumerateFiles(path))
            {
                Debug.WriteLine(f);
            }
            ProgramManager.Start(game);
        }

        public static void LoadNewRoom(String xmlFile)
        {
            ProgramManager.RemoveNonPlayerEntities();
            xmlParser.Parse(xmlFile);
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
