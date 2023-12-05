using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SprintZero1.Colliders;
using SprintZero1.Entities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities.BoomerangEntity;
using SprintZero1.Entities.WeaponEntities.BowAndMagicFireEntity;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using System.Collections.Generic;
using System.Linq;

namespace SprintZero1.StatePatterns.GameStatePatterns
{

    internal class GamePlayingState : BaseGameState
    {

        /// <summary>
        /// List of projects that may be drawn on screen
        /// </summary>
        private readonly List<IEntity> _projectiles = new List<IEntity>();
        private readonly ColliderManager _colliderManager;
        // Note: Base variable is EntityManager
        public DungeonRoom CurrentRoom { get { return _currentRoom; } }
        private readonly Song _dungeonMusic;
        private bool _pauseUpdate;

        public bool PauseUpdate { get { return _pauseUpdate; } set { _pauseUpdate = value; } }

        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game)
        {
            _colliderManager = new ColliderManager();
            _dungeonMusic = SoundFactory.GetMusic("DungeonMusic");
            SoundFactory.AdjustMusicVolume(.3f);
            SoundFactory.PlayMusic(_dungeonMusic);
            _pauseUpdate = false;
        }

        public void IncrementLivePlayers()
        {
            if (_livePlayerCount == _livePlayerList.Count) { return; }
            _livePlayerCount++;
        }

        public void DecrementLivePlayers()
        {
            _livePlayerCount--;
            if (_livePlayerCount == 0)
            {
                GameStatesManager.ChangeGameState(GameState.GameOver);
            }
        }

        public override void Handle()
        {
            List<IEntity> entityList = _livePlayerList.Values.Select(tuple => tuple.Item1).ToList();
            _currentRoom.UpdateEnemyController(entityList);
            SoundFactory.AdjustMusicVolume(0.3f);
        }

        /// <summary>
        /// Remove a collider from the list of collidable entities
        /// </summary>
        /// <param name="entity">The entity with the collider being removed</param>
        public void RemoveCollider(IEntity entity)
        {
            _colliderManager.RemoveCollidableEntity(entity);
        }

        /// <summary>
        /// Add a collider from the list of collidable entities
        /// </summary>
        /// <param name="entity">the entity with the collider being added</param>
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
            if (_projectiles.Count > 0)
            {
                foreach (IEntity projectile in _projectiles)
                {
                    if (projectile is BoomerangBasedEntity boomerang)
                    {
                        boomerang.IsActive = false;
                    }
                    else if (projectile is NonComingBackWeaponEntity nonComingBackWeapon)
                    {
                        nonComingBackWeapon.IsActive = false;
                    }
                }
                _projectiles.Clear();
            }
            _currentRoom = LevelManager.GetDungeonRoom(nextRoomName);
            _colliderManager.AddCollidableEntities(_currentRoom.GetEntityList());
            foreach (var playerTuple in _livePlayerList.Values)
            {
                _colliderManager.AddCollidableEntity(playerTuple.Item1);
            }
            _currentRoom.ColliderManager = _colliderManager;
        }

        public void AddProjectile(IEntity projectile)
        {
            _projectiles.Add(projectile);
            AddCollider(projectile);
        }

        public void RemoveProjectile(IEntity projectile)
        {
            _projectiles.Remove(projectile);
            RemoveCollider(projectile);
        }

        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // not a good implementation, but again, time constraints
            if (_pauseUpdate)
            {
                // Update player, but not controller
                foreach (var playerTuple in _livePlayerList.Values)
                {
                    if (playerTuple.Item1 is PlayerEntity player)
                    {
                        player.TransitionToState(State.Idle);
                        player.Update(gameTime);
                    }
                }
                _colliderManager.CheckCollisions();
                return;
            }

            HUDManager.Update(gameTime);
            // update player and their respective controller
            for (int i = 0; i < _livePlayerList.Count; i++)
            {
                _livePlayerList[i + 1].Item2.Update();
                _livePlayerList[i + 1].Item1.Update(gameTime);
            }
            _currentRoom.Update(gameTime);

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
            HUDManager.Draw(spriteBatch);
            // draw each player
            foreach (var playerTuple in _livePlayerList.Values)
            {
                playerTuple.Item1.Draw(spriteBatch);
            }

            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].Draw(spriteBatch);
            }

            _currentRoom.Draw(spriteBatch);
        }
    }
}
