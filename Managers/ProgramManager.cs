using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class ProgramManager
    {
        public static Game1 game;
        static List<IEntity> onScreenEntities = new List<IEntity>();

        // List of available Controllers
        
        static IController[] controllers = new IController[] 
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
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateFloorSprite("entrance"), new Vector2(128, 151)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewWallSprite(1), new Vector2(200, 100)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewWallSprite(2), new Vector2(56, 100)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewWallSprite(3), new Vector2(56, 204)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewWallSprite(4), new Vector2(200, 204)));
            AddOnScreenEntity(new LevelBlockEntity(TileSpriteFactory.Instance.CreateNewTileSprite("pyramid"), new Vector2(40, 104), true));
            ICombatEntity link = new PlayerEntity(new Vector2(176, 170), 6, Enums.Direction.North);
            IEntity ProjectileEntity = new ProjectileEntity();
            controllers[0].LoadDefaultCommands(localGame, link, ProjectileEntity);
            AddOnScreenEntity(link);
            AddOnScreenEntity(ProjectileEntity);
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewTileSprite("open_north"), new Vector2(128, 80)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewTileSprite("open_west"), new Vector2(16, 152)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewTileSprite("open_east"), new Vector2(240, 152)));
            AddOnScreenEntity(new BackgroundSpriteEntity(TileSpriteFactory.Instance.CreateNewTileSprite("open_south"), new Vector2(128, 224)));
            AddOnScreenEntity(new InvisibleWallEntity(new Vector2(8, 72), new Vector2(112, 32)));
            AddOnScreenEntity(new InvisibleWallEntity(new Vector2(8, 104), new Vector2(32, 40)));
            AddOnScreenEntity(new InvisibleWallEntity(new Vector2(8, 176), new Vector2(32, 72)));
            AddOnScreenEntity(new InvisibleWallEntity(new Vector2(40, 216), new Vector2(80, 32)));
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
