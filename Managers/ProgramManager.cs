using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Controllers.EnemyControllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
       // public static Game1 myGame;
        static List<IEntity> onScreenEntities = new List<IEntity>();
        private static IController controller;
        private static Game1 myGame;
        static IEnemyMovementController enemyMovementController;
        // List of available Controllers

        // IController controller = new IController[] 
        //#region
       // {
       //     new KeyboardController()
       // };
       // #endregion


        public static void Start(Game1 game)
        {
            myGame = game;
            controller = new KeyboardController();

        }

        public static void AddPlayer(Vector2 position, int health, Direction direction) {
            ICombatEntity player = new PlayerEntity(position, health, direction);
            IEntity ProjectileEntity = new ProjectileEntity();
            controller.LoadDefaultCommands(myGame, player, ProjectileEntity);
            ProgramManager.AddOnScreenEntity(player);
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

        public static void RemoveNonLinkEntities()
        {
            int i = 1;
            ColliderManager.RemoveAllExceptLink();
            while (i < onScreenEntities.Count) {

                ProgramManager.RemoveOnScreenEntity(onScreenEntities[i]);
              
                
            }
        }

        public static void Update(GameTime gameTime)
        {
          
                controller.Update();
            ColliderManager.Update(gameTime);
           
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Update(gameTime);
               
            }
           
            enemyMovementController?.Update(gameTime);
            controller.Update();
        }

        /// <summary>
        /// Run Draw on Program Manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            int i = 1;
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Draw(spriteBatch);
                i++;
                if (i == 5)
                {
                    onScreenEntities[0].Draw(spriteBatch);
                }
            }
        }
    }
}
