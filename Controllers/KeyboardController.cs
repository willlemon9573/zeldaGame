using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using SprintZero1.Commands;
namespace SprintZero1.Controllers
{
    public class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> keyboardMap;
        private HashSet<Keys> previouslyPressedKeys;

        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            keyboardMap = new Dictionary<Keys, ICommand>();
            previouslyPressedKeys = new HashSet<Keys>();
        }

        public void LoadDefaultCommands(Game1 game)
        {
            keyboardMap.Add(Keys.Y, new GetNextBlockCommand(game));
            keyboardMap.Add(Keys.T, new GetPreviousBlockCommand(game));
            //keyboardMap.Add(Keys.W, new ChangeLinkDirectionCommand(game，0));
        }

        public void Update()
        {
            Keys[] pressedkeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedkeys)
            {
                if (!previouslyPressedKeys.Contains(key) && keyboardMap.ContainsKey(key))
                {
                    keyboardMap[key].Execute();
                }
            }
            previouslyPressedKeys = new HashSet<Keys>(pressedkeys);
        }
    }
}
