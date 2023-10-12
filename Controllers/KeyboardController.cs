using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> keyboardMap;
        private Keys prevKey;
        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            keyboardMap = new Dictionary<Keys, ICommand>();
        }

        public void LoadDefaultCommands(Game1 game, IEntity playerEntity)
        {
            /* directional commands */
            keyboardMap.Add(Keys.Up, new MoveUpCommand(playerEntity));
            keyboardMap.Add(Keys.Down, new MoveDownCommand(playerEntity));
            keyboardMap.Add(Keys.Left, new MoveLeftCommand(playerEntity));
            keyboardMap.Add(Keys.Right, new MoveRightCommand(playerEntity));
            keyboardMap.Add(Keys.W, new MoveUpCommand(playerEntity));
            keyboardMap.Add(Keys.S, new MoveDownCommand(playerEntity));
            keyboardMap.Add(Keys.A, new MoveLeftCommand(playerEntity));
            keyboardMap.Add(Keys.D, new MoveRightCommand(playerEntity));
            /* Attack Commands */
            keyboardMap.Add(Keys.Z, new LinkAttackCommand(game));
            /* Other commands */
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            keyboardMap.Add(Keys.U, new PreviousItemCommand(game));
            keyboardMap.Add(Keys.I, new NextItemCommand(game));
            keyboardMap.Add(Keys.O, new PreviousEnemyCommand(game));
            keyboardMap.Add(Keys.P, new NextEnemyCommand(game));
            keyboardMap.Add(Keys.Y, new GetPreviousTileCommand(game));
            keyboardMap.Add(Keys.T, new GetNextTileCommands(game));
        }

        public void Update()
        {
            // monogame updates too quickly for this
            Keys[] pressedkeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedkeys)
            {
                if (prevKey == key && keyboardMap.ContainsKey(key))
                {
                    keyboardMap[key].Execute();
                }
            }
            /* Temporary fix to make sure player doesn't move at an angle */
            if (pressedkeys.Length > 0 && prevKey != pressedkeys[0])
            {
                prevKey = pressedkeys[0];
            }
        }
    }
}