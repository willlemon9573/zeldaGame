using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 _game;
#pragma warning disable IDE0090 // Use 'new(...)'
        static readonly List<IEntity> onScreenEntities = new List<IEntity>();
#pragma warning restore IDE0090 // Use 'new(...)'
        private static PlayerEntity player;
        // List of available Controllers
        static readonly IController[] controllers = new IController[]
        #region
        {
            new KeyboardController()
        };
        #endregion

        public static void Start(Game1 game)
        {
            _game = game;
            player = new PlayerEntity(new Vector2(176, 170), 6, Enums.Direction.North);
            IEntity ProjectileEntity = new ProjectileEntity();
            AddOnScreenEntity(player);
            controllers[0].LoadDefaultCommands(game, player, ProjectileEntity);
            AddOnScreenEntity(ProjectileEntity);
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
            ColliderManager.RemoveAllExcept(player.PlayerCollider);
            onScreenEntities.Clear();
            onScreenEntities.Add(player);
            player.Position = new Vector2(150, 150);
        }

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
            ColliderManager.Update();
        }

        /// <summary>
        /// Run Draw on Program Manager
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i < onScreenEntities.Count; i++)
            {
                onScreenEntities[i].Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
        }
    }
}
