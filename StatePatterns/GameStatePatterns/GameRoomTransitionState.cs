using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.LevelFiles;
using SprintZero1.Managers;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.GameStatePatterns
{
    internal class GameRoomTransitionState : BaseGameState
    {
        private readonly List<IEntity> onScreenEntities = new List<IEntity>();
        private int transitionLength;
        private int transitionProgress;
        private Matrix translation;
        private Direction _direction;

        private DungeonRoom originalRoom;
        private DungeonRoom destinationRoom;
        private Game1 game;

        private readonly Dictionary<Direction, Vector2> directionDistances = new Dictionary<Direction, Vector2>
        {
            { Direction.North, new Vector2(0, -176) },
            { Direction.South, new Vector2(0, 176) },
            { Direction.East, new Vector2(256, 0)},
            { Direction.West, new Vector2(-256, 0) }
        };
        private readonly Dictionary<Direction, Vector2> cameraDirectionsDictionary = new Dictionary<Direction, Vector2>
        {
            { Direction.North, new Vector2(0, 1) },
            { Direction.South, new Vector2(0, -1) },
            { Direction.East, new Vector2(-1, 0) },
            { Direction.West, new Vector2(1, 0) }
        };

        Vector2 cameraMovementDirection = new Vector2();

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
        public void StartTransition(string nextRoom, Direction direction, Vector2 newPlayerDirection)
        {
            foreach (var playerTuple in _livePlayerList.Values)
            {
                if (playerTuple.Item1 is ICollidableEntity entity)
                {
                    entity.Position = newPlayerDirection;
                    entity.Collider.Update(entity);
                }
            }
            onScreenEntities.Clear();
            _direction = direction;
            originalRoom = (GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState).CurrentRoom;
            destinationRoom = LevelManager.GetDungeonRoom(nextRoom);
            onScreenEntities.AddRange(destinationRoom.GetEntityList());
            foreach (IEntity entity in onScreenEntities)
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
            foreach (IEntity entity in onScreenEntities)
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
            if (transitionProgress > transitionLength)
            {
                _game.Translation = null;
                foreach (IEntity entity in destinationRoom.GetEntityList())
                {
                    entity.Position -= directionDistances[_direction];
                }
                GameStatesManager.ChangeGameState(GameState.Playing);
            }
        }
    }
}
