﻿using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1
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
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            keyboardMap.Add(Keys.U, new ExitCommand(game));
            keyboardMap.Add(Keys.I, new ExitCommand(game));
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
