using Microsoft.Xna.Framework;
using SprintZero1.Entities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// Handles the player when they are in the moving state
    /// @author Aaron Heishman
    /// </summary>
    internal class PlayerMovingState : BasePlayerState
    {
        private Vector2 directionToMove;
        private readonly float PlayerSpeed = 75f; // 75 pixels per second
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        /// <summary>
        /// Player moving state constructor
        /// </summary>
        /// <param name="playerEntity">The specific player entity to be used</param>
        public PlayerMovingState(PlayerEntity playerEntity) : base(playerEntity)
        {
            _velocityMap = new Dictionary<Direction, Vector2>()
           {
                {Direction.North, new Vector2(0, -PlayerSpeed) },
                {Direction.South, new Vector2(0, PlayerSpeed) },
                {Direction.East, new Vector2(PlayerSpeed, 0) },
                {Direction.West, new Vector2(-PlayerSpeed, 0) }
           };
        }

        /// <summary>
        /// Request moving the character if the character is in a state where they can move
        /// </summary>
        public override void Request()
        {
            if (!_canTransition) { return; }
            directionToMove = _velocityMap[_playerEntity.Direction];
        }
        /// <summary>
        /// Updates the player sprite when they move
        /// </summary>
        /// <param name="gameTime">The current time state of the game</param>
        public override void Update(GameTime gameTime)
        {
            if (!_canTransition) { return; }
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerEntity.Position += (directionToMove * deltaTime);
            // update the player sprite only when they move
            // base player state will handle drawing the sprite
            _playerEntity.PlayerSprite.Update(gameTime);
        }
    }
}

