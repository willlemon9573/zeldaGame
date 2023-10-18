using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 myGame;
        static List<IEntity> onScreenEntities = new List<IEntity>();
        private static IController controller;
        // Runs on startup. Use it to add entity if you want to test things

        
        public static void Start(Game1 game)
        {
            myGame = game;
            controller = new KeyboardController();

        }

        public static void AddPlayer(Vector2 position, int health, Direction direction) {
            IEntity player = new PlayerEntity(position, health, direction);
            controller.LoadDefaultCommands(myGame, player);
            ProgramManager.AddOnScreenEntity(player);
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
