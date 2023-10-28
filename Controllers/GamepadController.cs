using Microsoft.Xna.Framework.Input;
using SprintZero1.Entities;
using SprintZero1.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SprintZero1.Controllers
{
    internal class GamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> gamepadMap;
        private readonly List<Buttons> _movementButtonList;
        private List<Buttons> _previousPressedButtons;
        private readonly Stack<Buttons> movementButtonStack;
        public readonly int index;

        /// <summary>
        /// Constructor an object to control the keyboard
        /// </summary>
        public GamepadController(int index)
        {
            gamepadMap = new Dictionary<Buttons, ICommand>();
            _previousPressedButtons = new List<Buttons>();
            movementButtonStack = new Stack<Buttons>();
            _movementButtonList = new List<Buttons>() { Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight, Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight };
            this.index = index;
        }

        /// <summary>
        /// Recursively removes movement buttons from the movement button 
        /// </summary>
        /// <param name="pressedButtons">collection of currently pressed buttons</param>
        private void FlipAndClean(List<Buttons> pressedButtons)
        {
            if(movementButtonStack.Count > 0) 
            {
                Buttons b = movementButtonStack.Pop();
                FlipAndClean(pressedButtons);
                if(pressedButtons.Contains(b)) 
                {
                    movementButtonStack.Push(b);
                }
            }
        }

        /// <summary>
        /// Function to allow multiple movement buttons
        /// </summary>
        /// <param name="movementButton">the current movement button</param>
        void HandleMovementButtons(Buttons movementButton)
        {
            /*
             * Add button to stack if its not already in the stack
             * else execute the command
             */
            if(!movementButtonStack.Contains(movementButton))
            {
                movementButtonStack.Push(movementButton);
            }
            else if(movementButton == movementButtonStack.Peek())
            {
                Buttons buttonRef = movementButtonStack.Peek();
                Debug.WriteLine(gamepadMap[buttonRef].ToString());
                gamepadMap[buttonRef].Execute();
            }
        }

        public void LoadDefaultCommands(Game1 game, ICombatEntity playerEntity, IEntity ProjectileEntity)
        {
            /* directional commands */
            gamepadMap.Add(Buttons.DPadUp, new MoveUpCommand(playerEntity));
            gamepadMap.Add(Buttons.DPadDown, new MoveDownCommand(playerEntity));
            gamepadMap.Add(Buttons.DPadLeft, new MoveLeftCommand(playerEntity));
            gamepadMap.Add(Buttons.DPadRight, new MoveRightCommand(playerEntity));
            gamepadMap.Add(Buttons.LeftThumbstickUp, new MoveUpCommand(playerEntity));
            gamepadMap.Add(Buttons.LeftThumbstickDown, new MoveDownCommand(playerEntity));
            gamepadMap.Add(Buttons.LeftThumbstickLeft, new MoveLeftCommand(playerEntity));
            gamepadMap.Add(Buttons.LeftThumbstickRight, new MoveRightCommand(playerEntity));

            /* Attack Commands */
            gamepadMap.Add(Buttons.X, new SwordAttackCommand(playerEntity));
            /* Other commands */
            gamepadMap.Add(Buttons.Back, new ExitCommand(game));

            gamepadMap.Add(Buttons.LeftShoulder, new FireArrowCommand(playerEntity, ProjectileEntity));
            gamepadMap.Add(Buttons.RightShoulder, new FireBetterArrowCommand(playerEntity, ProjectileEntity));
            gamepadMap.Add(Buttons.LeftTrigger, new FireBoomerangCommand(playerEntity, ProjectileEntity));
            gamepadMap.Add(Buttons.RightTrigger, new FireBetterBoomerangCommand(playerEntity, ProjectileEntity));
            gamepadMap.Add(Buttons.Y, new FireBombCommand(playerEntity, ProjectileEntity));
            gamepadMap.Add(Buttons.B, new FireMagicFireCommand(playerEntity, ProjectileEntity));

        }

        public void Update()
        {
            List<Buttons> pressedButtons = GetPressedButtons(index);
            int totalButtonCount = 0;
            /* iterate over pressed button collection executing only valid keys in the gamepad map 
               keeping track of the amount of movement button that are also currently being pressed
            */
            foreach(Buttons button in pressedButtons)
            {
                if(_movementButtonList.Contains(button))
                {
                    Debug.WriteLine(button);
                    HandleMovementButtons(button);
                }
                else if(gamepadMap.ContainsKey(button) &&!_previousPressedButtons.Contains(button)) 
                {
                    gamepadMap[button].Execute();
                }
            }

            if(totalButtonCount < movementButtonStack.Count) 
            {
                FlipAndClean(pressedButtons);
            }

            _previousPressedButtons = pressedButtons.ToList<Buttons>();
        }



        /// <summary>
        /// Return list of currently pressed buttons,
        /// since GamePad doesnt have a "Get pressed buttons"
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<Buttons> GetPressedButtons(int index)
        {
            List<Buttons> pressedButtons = new List<Buttons>();

            foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
            {
                if (GamePad.GetState(index).IsButtonDown(btn))
                {
                    pressedButtons.Add(btn);
                }
            }

            return pressedButtons;
        }
    }
}
