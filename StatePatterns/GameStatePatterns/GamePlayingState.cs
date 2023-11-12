using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using SprintZero1.Entities;
using System.Collections.Generic;
using SprintZero1.Colliders;
using System.Linq;
using System;
using SprintZero1.Controllers;

namespace SprintZero1.StatePatterns.GameStatePatterns
{

    internal class GamePlayingState : BaseGameState
    {
        public Game1 _game;
        public DungeonRoom _currentRoom;

        /// <summary>
        /// List of players
        /// </summary>
        public List<PlayerEntity> _players = new List<PlayerEntity>();
        // Note: Base variable is EntityManager
        

        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game) 
        {
            _game = game;
        }


        /// <summary>
        /// Updates all controllers in State
        /// </summary>
        public override void Handle()
        {
            foreach (IController controller in Controllers)
            {
                controller.Update();
            }
        }

        public void AddPlayer(PlayerEntity player)
        {
            _players.Add(player);
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
            EntityManager.ParseDungeonRoom(nextRoom);
            EntityManager.Add(_players.OfType<IEntity>().ToList());
            _currentRoom = nextRoom;
        }

        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //ProgramManager.Update(gameTime);
            EntityManager.Update(gameTime);
            foreach(IEntity entity in EntityManager.OnScreenEntities())
            {
                entity.Update(gameTime);
            }
            ColliderManager.CheckCollisions(EntityManager.OnScreenEntities().OfType<ICollidableEntity>().ToList());
        }

        /// <summary>
        /// Handles Drawing The entire game
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //ProgramManager.Draw(spriteBatch);
            foreach (IEntity entity in EntityManager.OnScreenEntities())
            {
                entity.Draw(spriteBatch);
            }
        }
    }
}
