using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using System.Collections.Generic;
using System.Diagnostics;

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
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            keyboardMap.Add(Keys.U, new PreviousItemCommand(game));
            keyboardMap.Add(Keys.I, new NextItemCommand(game));
        }

        public void Update()
        {
            Keys[] pressedkeys = Keyboard.GetState().GetPressedKeys();
            // search through presseds keys and execute the given command
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
