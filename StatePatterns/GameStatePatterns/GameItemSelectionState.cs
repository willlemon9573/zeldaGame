using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Controllers;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.GameStateMenu;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameItemSelectionState : BaseGameState
    {
        private readonly Dictionary<IEntity, (ItemSelectionMenu, IController)> _itemSelectionMenuMap;
        private IEntity currentPlayer;
        private IController _currentController;
        private IGameStateMenu _currentMenu;
        private int _gamepadIndex; // the index for gamepads in case there is more than one one player using a gamepad
        public IEntity CurrentPlayer { set => currentPlayer = value; }

        private void CreateMenuWithKeyboard(IEntity player)
        {
            ItemSelectionMenu playerMenu = new ItemSelectionMenu(_game, player);
            IController itemSelectionController = new KeyboardItemMenuController(playerMenu);
            itemSelectionController.LoadControls(player);
            _itemSelectionMenuMap.Add(player, (playerMenu, itemSelectionController));
            _gamepadIndex = 0; // index starts at 0 for gamepads
        }

        private void CreateMenuWithGamePad(IEntity player)
        {
            ItemSelectionMenu playerMenu = new ItemSelectionMenu(_game, player);
            IController itemSelectionController = new GamepadItemMenuController(playerMenu, _gamepadIndex);
            itemSelectionController.LoadControls(player);
            _itemSelectionMenuMap.Add(player, (playerMenu, itemSelectionController));
            _gamepadIndex++;
        }

        /// <summary>
        /// Concstructor
        /// </summary>
        /// <param name="game">Game1 to assign</param>
        public GameItemSelectionState(Game1 game) : base(game)
        {
            _itemSelectionMenuMap = new Dictionary<IEntity, (ItemSelectionMenu, IController)>();
        }

        public override void AddPlayer(Tuple<IEntity, IController> player)
        {
            IEntity playerEntity = player.Item1;
            IController playerController = player.Item2;
            if (playerController is KeyboardController)
            {
                CreateMenuWithKeyboard(playerEntity);
            }
            else
            {
                CreateMenuWithGamePad(playerEntity);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _currentController.Update();
            _currentMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentMenu.Draw(spriteBatch);
        }

        public override void Handle()
        {
            Debug.Assert(currentPlayer != null, "Player is null.");
            if (_itemSelectionMenuMap.TryGetValue(currentPlayer, out var menuTuple))
            {
                _currentController = menuTuple.Item2;
                _currentMenu = menuTuple.Item1;
                (_currentMenu as ItemSelectionMenu).SynchronizeInventory();
            }
        }

    }
}