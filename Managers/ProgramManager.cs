using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 game;
        static List<IEntity> onScreenEntities = new List<IEntity>();
        private static IController controller = new KeyboardController();

        // Runs on startup. Use it to add entity if you want to test things
        public static void Start(Game1 localGame)
        {

        }

        public static void AddOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Add(entity);
        }

        public static void RemoveOnScreenEntity(IEntity entity)
        {
            onScreenEntities.Remove(entity);
        }

        /// <summary>
        /// Added to test palyer Entity. Can't directly add a new player entity to the list as controlelr needs player entity passed into it.
        /// </summary>
        public static void testPlayerEntity()
        {
            IEntity playerEntity = new PlayerEntity(new Vector2(176, 170), 6, Direction.North);
            onScreenEntities.Add(playerEntity);
            controller.LoadDefaultCommands(game, playerEntity);
        }
        public static void Update(GameTime gameTime)
        {

            foreach (IEntity entity in onScreenEntities)
            {
                entity.Update(gameTime);
            }
            controller.Update();
            ColliderManager.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEntity entity in onScreenEntities)
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
