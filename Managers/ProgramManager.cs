﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 _game;

        private static List<PlayerEntity> playerList = new List<PlayerEntity>();
        private static IEntity projectileHandler;
        /* Inventory Testing */
        private static List<StackableItems> itemList = new List<StackableItems>() { StackableItems.Rupee, StackableItems.Bomb, StackableItems.Arrow, StackableItems.DungeonKey };
        // List of available Controllers
        static List<IEnemyMovementController> onScreenEnemyController = new List<IEnemyMovementController>();
        static readonly IController[] controllers = new IController[]

        #region
        {
            new KeyboardController(),
            new GamepadController(0),
            new GamepadController(1),
            new GamepadController(2),
            new GamepadController(3)
        };
        #endregion
        static PlayerEntity player;
        public static PlayerEntity Player { get { return player; } }
        private static GameState gameState = GameState.Playing;

        public static GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }




        public static void Start(Game1 game)
        {
            _game = game;

            player = new PlayerEntity(new Vector2(126, 200), 6, Enums.Direction.North);
            playerList.Add(player);
            AddOnScreenEntity(player);
            controllers[0].LoadDefaultCommands(game, player);

        }

        /// <summary>
        /// Add an entity to the screen
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// 

        public static void AddOnScreenEnemyController(IEnemyMovementController enemyController)
        {
            onScreenEnemyController.Add(enemyController);
        }
        public static void AddOnScreenEntity(IEntity entity)
        {
            EntityManager.Add(entity);
        }

        /// <summary>
        /// Remove Entity from the list
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public static void RemoveOnScreenEntity(IEntity entity)
        {
            EntityManager.Remove(entity);
        }

        public static void RemoveNonPlayerEntities()
        {
            IEntity player = playerList[0];
            EntityManager.LoadNextScreen(player);
            player.Position = new Vector2(126, 200);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (IEnemyMovementController enemyController in onScreenEnemyController)
            {
                enemyController.Update(gameTime);
            }

            EntityManager.Update(gameTime);
            List<IEntity> entities = EntityManager.OnScreenEntities();
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
            //projectileHandler.Update(gameTime);
            ColliderManager.CheckCollisions(entities.OfType<ICollidableEntity>().ToList());

        }

        /// <summary>
        /// Run Draw on Program Manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            IEntity player = playerList[0];
            List<IEntity> onScreenEntities = EntityManager.OnScreenEntities();
            for (int i = 1; i < onScreenEntities.Count; i++)
            {
                onScreenEntities[i].Draw(spriteBatch);
                if (i == 5)
                {
                    // draw player/projectile here to have player be drawn "under" doors and for project to be "under" link
                    player.Draw(spriteBatch);
                }
            }
        }
    }
}
