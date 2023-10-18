using Microsoft.Xna.Framework.Input;
using SprintZero1.Commands;
using SprintZero1.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> keyboardMap;
        readonly List<Keys> _movementKeyList;
        List<Keys> _previouslyPressedKeys;
        readonly Stack<Keys> movementKeyStack;
        /// <summary>
        /// Construct an object to control the keyboard
        /// </summary>
        public KeyboardController()
        {
            keyboardMap = new Dictionary<Keys, ICommand>();
            _previouslyPressedKeys = new List<Keys>();
            movementKeyStack = new Stack<Keys>();
            _movementKeyList = new List<Keys>() { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.W, Keys.S, Keys.A, Keys.D };
        }

        /// <summary>
        /// Recursively removes keys from stack that are not in the list of pressed keys
        /// </summary>
        /// <param name="pressedKeys">collection of currently pressed keys</param>
        private void FlipAndClean(Keys[] pressedKeys)
        {
            if (movementKeyStack.Count > 0)
            {
                Keys k = movementKeyStack.Pop();
                FlipAndClean(pressedKeys);
                if (pressedKeys.Contains(k))
                {
                    movementKeyStack.Push(k);
                }
            }
        }

        /// <summary>
        /// A function to allow multiple movement keys to be pressed at once while maintaining the most recent key command is executed
        /// </summary>
        /// <param name="movementKey">the current movement key that needs to be checked</param>
        void HandleMovementKey(Keys movementKey)
        {
            /* Add key to stack if it's not in the stack already */
            if (!movementKeyStack.Contains(movementKey))
            {
                movementKeyStack.Push(movementKey);
            }
            else if (movementKey == movementKeyStack.Peek())
            {
                Keys keyRef = movementKeyStack.Peek();
                keyboardMap[keyRef].Execute();
            }

        }

        public void LoadDefaultCommands(Game1 game, IEntity playerEntity, ProjectileEntity ProjectileEntity)
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
            //keyboardMap.Add(Keys.Z, new SwordAttackCommand(playerEntity));
            /* Other commands */
            keyboardMap.Add(Keys.D0, new ExitCommand(game));
            keyboardMap.Add(Keys.U, new PreviousItemCommand(game));
            keyboardMap.Add(Keys.I, new NextItemCommand(game));
            keyboardMap.Add(Keys.O, new PreviousEnemyCommand(game));
            keyboardMap.Add(Keys.P, new NextEnemyCommand(game));
            keyboardMap.Add(Keys.Y, new GetPreviousTileCommand(game));
            keyboardMap.Add(Keys.T, new GetNextTileCommands(game));
            keyboardMap.Add(Keys.D1, new ArrowWeapon(playerEntity, ProjectileEntity));
            keyboardMap.Add(Keys.D2, new betterArrowWeapon(playerEntity, ProjectileEntity));
            keyboardMap.Add(Keys.D3, new BoomerangWeapon(playerEntity, ProjectileEntity));
            keyboardMap.Add(Keys.D4, new betterBoomerangWeapon(playerEntity, ProjectileEntity));
            keyboardMap.Add(Keys.D5, new BombWeapon(playerEntity, ProjectileEntity));
            keyboardMap.Add(Keys.D6, new MagicFireWeapon(playerEntity, ProjectileEntity));



        }

        public void Update()
        {
            /* handling movement? */
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            int movementKeyCount = 0;
            /* iterate over pressed key collection executing only valid keys in the keyboard map 
               keeping track of the amount of movement keys that are also currently being pressed
            */
            foreach (Keys key in pressedKeys)
            {
                if (_movementKeyList.Contains(key))
                {
                    HandleMovementKey(key);
                    movementKeyCount++;
                }
                else if (keyboardMap.ContainsKey(key) && !_previouslyPressedKeys.Contains(key))
                {
                    keyboardMap[key].Execute();
                }
            }
            /* Check if movement key stack needs to be cleaned */
            if (movementKeyCount < movementKeyStack.Count)
            {
                FlipAndClean(pressedKeys);
            }

            /* store pressed keys as previously pressed keys */
            _previouslyPressedKeys = pressedKeys.ToList<Keys>();
        }
    }
}