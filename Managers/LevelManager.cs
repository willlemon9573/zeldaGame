using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.XMLFiles;
using System;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    public static class LevelManager
    {
        public static Game1 game;
        public static XMLParser xmlParser;
        private static readonly int totalRooms;
        private static int index;
        static List<String> levelList = new List<String> { "XMLFiles/Room1.xml", "XMLFiles/Room1Left.xml", "XMLFiles/Room1Right.xml", "XMLFiles/Room2.xml",
        "XMLFiles/Room3.xml", "XMLFiles/Room3Left.xml", "XMLFiles/Room3Right.xml", "XMLFiles/Room4.xml", "XMLFiles/Room4Left.xml", "XMLFiles/Room4Leftest.xml",
                "XMLFiles/Room4Right.xml", "XMLFiles/Room4Rightest.xml", "XMLFiles/Room5.xml", "XMLFiles/Room5Right.xml", "XMLFiles/Room5Rightest.xml",
                "XMLFiles/Room6.xml","XMLFiles/Room6Left.xml","XMLFiles/RoomSecret.xml" , "XMLFiles/RoomDev.xml"};
        private static int levelListIndex = 0;

        public static List<string> LevelList
        {
            get { return levelList; }
        }

        public static int LevelListIndex
        {
            get { return levelListIndex; }
            set { levelListIndex = value; }
        }

        static LevelManager()
        {
            totalRooms = levelList.Count;
            index = LevelListIndex;
            xmlParser = new XMLParser();
        }

        public static void Initialize(Game1 game)
        {
            ProgramManager.Start(game);
            LoadNewRoom(levelList[1]);
        }

        public static void LoadNewRoom(String xmlFile)
        {
            ProgramManager.RemoveNonLinkEntities();
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
