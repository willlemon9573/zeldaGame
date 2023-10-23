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
        private readonly Dictionary<Direction, Vector2> _velocityMap;
        /// <summary>
        /// Player moving state constructor
        /// </summary>
        /// <param name="playerEntity">The specific player entity to be used</param>
        public PlayerMovingState(PlayerEntity playerEntity) : base(playerEntity)
        {
            _velocityMap = new Dictionary<Direction, Vector2>()
           {
                {Direction.North, new Vector2(0, -1) },
                {Direction.South, new Vector2(0, 1) },
                {Direction.East, new Vector2(1, 0) },
                {Direction.West, new Vector2(-1, 0) }
           };
        }
        /// <summary>
        /// Handles changing the direction of player when the player is in the moving state
        /// </summary>
        /// <param name="newDirection">The new direction the player is going to face</param>
        public override void ChangeDirection(Direction newDirection)
        {
            _playerEntity.Direction = newDirection;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetLinkSprite(newDirection);
        }

        /// <summary>
        /// Request moving the character if the character is in a state where they can move
        /// </summary>
        public override void Request()
        {
            if (_blockTransition) { return; }
            _playerEntity.Position += _velocityMap[_playerEntity.Direction];
        }
        /// <summary>
        /// Updates the player sprite when they move
        /// </summary>
        /// <param name="gameTime">The current time state of the game</param>
        public override void Update(GameTime gameTime)
        {
            // update the player sprite only when they move
            // base player state will handle drawing the sprite
            _playerEntity.PlayerSprite.Update(gameTime);
        }
    }
}

