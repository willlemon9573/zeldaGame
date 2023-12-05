using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using SprintZero1.Entities;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Managers;
using SprintZero1.LevelFiles;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameRoomTransitionState : BaseGameState
    {
        // room transition state can update players
        List<PlayerEntity> players;
        List<IEntity> onScreenEntities = new List<IEntity>();
        int transitionLength;
        int transitionProgress;
        Matrix translation;
        string Destination;
        Direction _direction;

        DungeonRoom originalRoom;
        DungeonRoom destinationRoom;
        Game1 game;

        Dictionary<Direction, Vector2> directionDistances = new Dictionary<Direction, Vector2>
        {
            { Direction.North, new Vector2(0, -176) },
            { Direction.South, new Vector2(0, 176) },
            { Direction.East, new Vector2(256, 0)},
            { Direction.West, new Vector2(-256, 0) }
        };
        Dictionary<Direction, Vector2> cameraDirectionsDictionary = new Dictionary<Direction, Vector2>
        {
            { Direction.North, new Vector2(0, 1) },
            { Direction.South, new Vector2(0, -1) },
            { Direction.East, new Vector2(-1, 0) },
            { Direction.West, new Vector2(1, 0) }
        };

        Vector2 cameraMovementDirection = new Vector2();

        //88
        //128

        public GameRoomTransitionState(Game1 game) : base(game)
        {
            this.game = game;
        }

        public void InchCamera()
        {
            transitionProgress++;
            var dx = cameraMovementDirection.X * transitionProgress * 2;
            var dy = cameraMovementDirection.Y * transitionProgress * 2;
            translation = Matrix.CreateTranslation(dx, dy, 0);
        }

        /// <summary>
        /// Begin Transition to next room
        /// </summary>
        /// <param name="nextRoom">String for next room</param>
        /// <param name="direction">Direction from Door</param>
        public void StartTransition(string nextRoom, Direction direction)
        {
            onScreenEntities.Clear();
            _direction = direction;
            originalRoom = (GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState).CurrentRoom;
            destinationRoom = LevelManager.GetDungeonRoom(nextRoom);
            onScreenEntities.AddRange(destinationRoom.GetEntityList());
            foreach(IEntity entity in  onScreenEntities)
            {
                entity.Position += directionDistances[direction];
            }
            onScreenEntities.AddRange(originalRoom.GetEntityList());
            
            // Set which directoin to start transition Camera
            cameraMovementDirection = cameraDirectionsDictionary[direction];
            transitionProgress = 0;
            if (direction == Direction.North || direction == Direction.South)
                transitionLength = 88;
            else
                transitionLength = 128;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            HUDManager.Draw(spriteBatch);
            foreach(IEntity entity in onScreenEntities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public override void Handle()
        {
        }

        public override void Update(GameTime gameTime)
        {
            InchCamera();
            _game.Translation = translation;
            if(transitionProgress > transitionLength)
            {
                _game.Translation = null;
                foreach(IEntity entity in destinationRoom.GetEntityList())
                {
                    entity.Position -= directionDistances[_direction];
                }
                GameStatesManager.ChangeGameState(GameState.Playing);
            }
        }
    }
}
