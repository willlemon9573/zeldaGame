using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SprintZero1.Colliders;
using SprintZero1.DebuggingTools;
using SprintZero1.Entities;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{

    internal class GamePlayingState : BaseGameState
    {
        private DungeonRoom _currentRoom;

        /// <summary>
        /// List of players
        /// </summary>
        private readonly List<IEntity> _players = new List<IEntity>();
        private readonly List<IEntity> _projectiles = new List<IEntity>();

        private readonly ColliderManager _colliderManager;
        // Note: Base variable is EntityManager
        public DungeonRoom CurrentRoom { get { return _currentRoom; } }
        readonly MouseTools _mouseController; /* for debugging */
        private Song _dungeonMusic;
        private SpriteDebuggingTools _spriteDebuggingTools;
        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game)
        {
            _colliderManager = new ColliderManager();
            _mouseController = new MouseTools(game.GraphicsDevice);
            _dungeonMusic = SoundFactory.GetMusic("DungeonMusic");
            SoundFactory.AdjustMusicVolume(.3f);
            SoundFactory.PlayMusic(_dungeonMusic);
            _spriteDebuggingTools = new SpriteDebuggingTools(game);
        }

        public override void Handle()
        {
            _currentRoom.UpdateEnemyController(_players[0]);
            SoundFactory.AdjustMusicVolume(0.3f);
        }

        public void AddPlayer(PlayerEntity player)
        {
            _players.Add(player);
            _colliderManager.AddCollidableEntity(player);
        }

        public void RemoveCollider(IEntity entity)
        {
            _colliderManager.RemoveCollidableEntity(entity);
        }

        public void AddCollider(IEntity entity)
        {
            _colliderManager.AddCollidableEntity(entity);
        }

        /// <summary>
        /// Load dungeon room from key string name.
        /// Transitions the current room to the next room.
        /// _currentRoom then is set to nextRoom
        /// </summary>
        /// <param name="nextRoomName"></param>
        public void LoadDungeonRoom(string nextRoomName)
        {
            _colliderManager.ClearCollidableEntities();
            _currentRoom = LevelManager.GetDungeonRoom(nextRoomName);
            _colliderManager.AddCollidableEntities(_currentRoom.GetEntityList());
            _colliderManager.AddCollidableEntities(_players);
            _currentRoom.ColliderManager = _colliderManager;
        }

        public void AddProjectile(IEntity projectile)
        {
            _projectiles.Add(projectile);
        }

        public void RemoveProjectile(IEntity projectile)
        {
            _projectiles.Remove(projectile);
        }

        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _mouseController.UpdateCoordinates();
            HUDManager.Update(gameTime);
            Controllers.ForEach(controller => controller.Update());
            _currentRoom.Update(gameTime);
            _players.ForEach(player => player.Update(gameTime));
            /* Updating projectiles as they will be modified when no longer drawn*/
            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].Update(gameTime);
            }
            _colliderManager.CheckCollisions();
        }

        /// <summary>
        /// Handles Drawing The entire game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            _mouseController.DrawCoordinates(spriteBatch);
            _mouseController.DrawClickedRectangleCoordinates(spriteBatch);
            HUDManager.Draw(spriteBatch);
            _players.ForEach(player => player.Draw(spriteBatch));
            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].Draw(spriteBatch);
                Rectangle r = (_projectiles[i] as ICollidableEntity).Collider.Collider;
                _spriteDebuggingTools.DrawRectangle(r, Color.SandyBrown, spriteBatch);
            }

            _currentRoom.Draw(spriteBatch);
        }
    }
}
