using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.DebuggingTools;
using SprintZero1.Entities;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.StatePatterns.GameStatePatterns
{

    internal class GamePlayingState : BaseGameState
    {
        private DungeonRoom _currentRoom;

        /// <summary>
        /// List of players
        /// </summary>
        private readonly List<PlayerEntity> _players = new List<PlayerEntity>();
        // Note: Base variable is EntityManager
        public DungeonRoom CurrentRoom { get { return _currentRoom; } }
        SpriteDebuggingTools _spriteDebugger;
        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game)
        {
            _spriteDebugger = new SpriteDebuggingTools(game);
        }

        public override void Handle()
        {
            _currentRoom.UpdateEnemyController(_players[0]);
        }

        public void AddPlayer(PlayerEntity player)
        {
            _players.Add(player);
        }

        /// <summary>
        /// Update Room Entities after something is removed when player isn't leaving the room
        /// </summary>
        public void UpdateRoomEntities()
        {
            List<IEntity> entities = _currentRoom.GetEntityList();
            entities.AddRange(_players);
        }

        /// <summary>
        /// Load dungeon room from key string name.
        /// Transitions the current room to the next room.
        /// _currentRoom then is set to nextRoom
        /// </summary>
        /// <param name="nextRoomName"></param>
        public void LoadDungeonRoom(string nextRoomName)
        {
            DungeonRoom nextRoom = LevelManager.GetDungeonRoom(nextRoomName);
            _currentRoom = nextRoom;
        }

        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            HUDManager.Update(gameTime);
            Controllers.ForEach(controller => controller.Update());
            _currentRoom.Update(gameTime);
            _players.ForEach(player => player.Update(gameTime));
            List<IEntity> collidableEntities = _currentRoom.GetEntityList();
            collidableEntities.AddRange(_players);
            ColliderManager.CheckCollisions(collidableEntities.OfType<ICollidableEntity>().ToList());
        }

        /// <summary>
        /// Handles Drawing The entire game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            HUDManager.Draw(spriteBatch);
            _spriteDebugger.DrawRectangle((_players[0] as ICollidableEntity).Collider.Collider, Color.Red, spriteBatch);
            _players.ForEach(player => player.Draw(spriteBatch));
            _currentRoom.Draw(spriteBatch);
        }
    }
}
