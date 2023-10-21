using SprintZero1.Enums;
using SprintZero1.XMLFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SprintZero1.Managers
{
    public class LevelManager
    {
        public Game1 game;
        public XMLParser xmlParser;
        public LevelManager(Game1 game) {
            this.game = game;
            xmlParser = new XMLParser(game);
        }

        public void Initialize() {
            String xmlFile = "XMLFiles/Room5Right.xml";
            ProgramManager.Start(game);
            Vector2 pos = new Vector2(100, 100);
            ProgramManager.AddPlayer(pos, 1, Direction.South);
            xmlParser.Parse(xmlFile);

        }

        public void LoadRoom() { 
        
        }

        public void Update() { 
       
        }

        public void Draw() { 
            
        }
    }
}
