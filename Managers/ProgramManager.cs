using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 game;
#pragma warning disable IDE0090 // Use 'new(...)'
        static readonly List<IEntity> onScreenEntities = new List<IEntity>();
#pragma warning restore IDE0090 // Use 'new(...)'
        static readonly IEnemyMovementController enemyMovementController;
        // List of available Controllers

        static readonly IController[] controllers = new IController[] 
        #region
        {
            new KeyboardController()
        };
        #endregion

        /// <summary>
        /// Test method to load in default objects. Use to create testing environments.
        /// </summary>
        /// <param name="localGame">Game1 object</param>
        public static void Start(Game1 localGame)
        {
            ICombatEntity link = new PlayerEntity(new Vector2(176, 170), 6, Enums.Direction.North);
            IEntity ProjectileEntity = new ProjectileEntity();
            ICombatEntity enemyEntity = new EnemyEntityWithoutDirection(new Vector2(50, 50), 10, "dungeon_gel", 2);
            enemyMovementController = new SmartEnemyMovementController(enemyEntity, link);
            controllers[0].LoadDefaultCommands(localGame, link, ProjectileEntity);
            AddOnScreenEntity(link);
            AddOnScreenEntity(enemyEntity);
            AddOnScreenEntity(ProjectileEntity);
            game = localGame;
        }

        /// <summary>
        /// Add an entity to the screen
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public static void AddOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Add(entity);
        }

        /// <summary>
        /// Remove Entity from the list
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public static void RemoveOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Remove(entity);
        }

        /// <summary>
        /// Run Update on list of Entities and update any global engines
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Update(gameTime);
            }
            ColliderManager.Update(gameTime);
            enemyMovementController.Update(gameTime);
        }

        /// <summary>
        /// Run Draw on Program Manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
