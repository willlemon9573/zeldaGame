using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 _game;
        // List of available Controllers
        private static List<IEnemyMovementController> onScreenEnemyController = new List<IEnemyMovementController>();
        private static List<IEntity> _nonPlayerEntityList = new List<IEntity>();
        private static List<Tuple<IEntity, IController>> _playerList = new List<Tuple<IEntity, IController>>();
        private static DungeonRoom _currentRoom;

        public static DungeonRoom CurrentRoom { get { return _currentRoom; } }

        /// <summary>
        /// Create the list of Players and their keyboards
        /// </summary>
        private static void InitializePlayers()
        {
            const string CONTROLS_DOCUMENT_PATH = @"XMLFiles\PlayerXMLFiles\ControllerSettings.xml";
            int startingX = 176;
            int startingY = 170;
            float startingHealth = 3f;
            Direction startingDirection = Direction.North;
            PlayerEntity player = new PlayerEntity(new Vector2(startingX, startingY), startingHealth, startingDirection);
            ControlsManager.CreateKeyboardControlsMap(CONTROLS_DOCUMENT_PATH, player, _game);
            IController keyboardController = new KeyboardController();
            keyboardController.LoadControls(player);
            _playerList.Add(new Tuple<IEntity, IController>(player, keyboardController));
        }

        /// <summary>
        /// Start the program manager and all components that follow
        /// </summary>
        /// <param name="game">The current game</param>
        public static void Start(Game1 game)
        {
            _game = game;
            InitializePlayers();
            string entrance_room = "entrance";
            ChangeRooms(entrance_room);
            ISprite rupee = ItemSpriteFactory.Instance.CreateNonAnimatedItemSprite("rupee");

            ILootableEntity rup = new StackableItemEntity(rupee, new Vector2(120, 120), _currentRoom.RemoveItem, PlayerInventoryManager.AddStackableItemToInventory, StackableItems.Rupee);
            Debug.WriteLine($"{rup.GetType()}");
            _currentRoom.AddRoomItem(rup);
            UpdateRoomEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Reset()
        {
            // TODO
        }



        /// <summary>
        /// Change the current room the player is in
        /// </summary>
        /// <param name="roomName">the next room to change to</param>
        public static void ChangeRooms(string roomName)
        {
            _currentRoom = LevelManager.GetDungeonRoom(roomName);
            _nonPlayerEntityList = _currentRoom.GetEntityList();
        }
        /// <summary>
        /// For use when doors change / entities die
        /// </summary>
        public static void UpdateRoomEntities()
        {
            _nonPlayerEntityList = _currentRoom.GetEntityList();
        }



        /// <summary>
        /// TODO: Fill out
        /// </summary>
        /// <param name="enemyController"></param>
        public static void AddOnScreenEnemyController(IEnemyMovementController enemyController)
        {
            onScreenEnemyController.Add(enemyController);
        }
        /// <summary>
        /// Temporary
        /// </summary>
        /// <returns></returns>
        public static PausedStateUpdater GetPausedStateUpdater()
        {
            KeyboardController k = _playerList[0].Item2 as KeyboardController;
            return k.PausedStateUpdate;
        }

        private static void UpdatePlayers(GameTime gameTime)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                _playerList[i].Item1.Update(gameTime);
                _playerList[i].Item2.Update();
            }
        }

        private static void UpdateNPCs(GameTime gameTime)
        {
            for (int i = 0; i < _nonPlayerEntityList.Count; i++)
            {
                _nonPlayerEntityList[i].Update(gameTime);
            }
        }

        public static void Update(GameTime gameTime)
        {
            UpdatePlayers(gameTime);
            UpdateNPCs(gameTime);
            List<ICollidableEntity> collidableEntities = _playerList.Where(tuple => tuple.Item1 is ICollidableEntity).Select(tuple => tuple.Item1 as ICollidableEntity).ToList();
            collidableEntities.AddRange(_nonPlayerEntityList.Where(entity => entity is ICollidableEntity).Select(entity => entity as ICollidableEntity).ToList());
            ColliderManager.CheckCollisions(collidableEntities);
        }

        private static void DrawPlayers(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                _playerList[i].Item1.Draw(spriteBatch);
            }
        }

        private static void DrawNPCs(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _nonPlayerEntityList.Count; ++i)
            {
                _nonPlayerEntityList[i].Draw(spriteBatch);
            }
        }

        private static void UpdateRuppees(int amount)
        {
            // increment some value that represents ruppes
            // get the new sprites
        }

        /// <summary>
        /// Run Draw on Program Manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawPlayers(spriteBatch);
            DrawNPCs(spriteBatch);
            // sprites drawn
        }
    }
}
