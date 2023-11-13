﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Colliders;
using SprintZero1.Controllers;
using SprintZero1.Entities;
using SprintZero1.Enums;
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
        private List<PlayerEntity> _players = new List<PlayerEntity>();
        // Note: Base variable is EntityManager

        private Dictionary<Direction, Vector2> RoomStartPositions = new Dictionary<Direction, Vector2>
        {
            {Direction.North, new Vector2(128, 114) },
            {Direction.East, new Vector2(220, 160) },
            {Direction.South, new Vector2(128, 210)},
            {Direction.West, new Vector2(40, 160)}
        };

        public DungeonRoom CurrentRoom { get { return _currentRoom; } }

        /// <summary>
        /// The state of the game when the game is running
        /// </summary>
        /// <param name="game">The game</param>
        public GamePlayingState(Game1 game) : base(game)
        {
        }


        /// <summary>
        /// Updates all controllers in State
        /// </summary>
        public override void Handle()
        {
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
            EntityManager.UpdateEntities(entities);
        }

        /// <summary>
        /// Load dungeon room from key string name.
        /// Transitions the current room to the next room.
        /// _currentRoom then is set to nextRoom
        /// </summary>
        /// <param name="nextRoomName"></param>
        /// <param name="dir">Direction you entered door from</param>
        public void LoadDungeonRoom(string nextRoomName, Direction dir)
        {
            foreach (PlayerEntity player in _players)
            {
                player.Position = RoomStartPositions[dir + 2];
            }
            DungeonRoom nextRoom = LevelManager.GetDungeonRoom(nextRoomName);
            EntityManager.ParseDungeonRoom(nextRoom);
            EntityManager.Add(_players.OfType<IEntity>().ToList());
            _currentRoom = nextRoom;
        }

        /// <summary>
        /// Return the main player in the gameplaying state
        /// </summary>
        /// <returns></returns>
        public PlayerEntity ReturnMainPlayer()
        {
            return _players[0];
        }

        /// <summary>
        /// Handles updating the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //ProgramManager.Update(gameTime);
            EntityManager.Update(gameTime);
            HUDManager.Update(gameTime);
            List<IEntity> entities = EntityManager.OnScreenEntities();
            foreach (IController controller in Controllers)
            {
                controller.Update();
            }
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
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
            HUDManager.Draw(spriteBatch);
            List<IEntity> entities = EntityManager.OnScreenEntities();
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Draw(spriteBatch);
            }
        }
    }
}
