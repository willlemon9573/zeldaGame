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

        public static void Update(GameTime gameTime)
        {

            foreach (IEntity entity in onScreenEntities)
            {
                entity.Update(gameTime);
            }
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
