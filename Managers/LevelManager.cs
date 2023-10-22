using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.XMLFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Managers
{
    public class LevelManager
    {
        public Game1 game;
        public XMLParser xmlParser;
        private readonly List<String> levelList;
        private readonly int totalRooms;
        private int index;
        public LevelManager(Game1 game) {
            this.game = game;
            levelList = game.LevelList;
            totalRooms = levelList.Count;
            index = game.LevelListIndex;
            xmlParser = new XMLParser(game);
        }

        public void Initialize() {
            //String xmlFile = "XMLFiles/Room1.xml";
           
            ProgramManager.Start(game);
            Vector2 pos = new Vector2(100, 100);
            ProgramManager.AddPlayer(pos, 1, Direction.South);
            LoadNewRoom(levelList[0]);

        }

        public void LoadNewRoom(String xmlFile) {
            ProgramManager.RemoveNonLinkEntities();
            xmlParser.Parse(xmlFile);
        }

        public void Update(GameTime gameTime) {
            ProgramManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) { 
            ProgramManager.Draw(spriteBatch);
        }
    }
}
