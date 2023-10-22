using Microsoft.Xna.Framework;
using SprintZero1.Enums;
using SprintZero1.Entities;
using SprintZero1.Factories;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerMovingState : BasePlayerState
    {

        private LinkSpriteFactory _linkSpriteFactory = LinkSpriteFactory.Instance;
        private Dictionary<Direction, Vector2> _velocityMap;
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

        public override void ChangeDirection(Direction newDirection)
        { 
            _playerEntity.Direction = newDirection;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetLinkSprite(newDirection);
        }

        public override void Request()
        {
            _playerEntity.Position += _velocityMap[_playerEntity.Direction];
        }

        public override void Update(GameTime gameTime)
        {
            // update the player sprite only when they move
            // base player state will handle drawing the sprite
            _playerEntity.PlayerSprite.Update(gameTime);
        }
    }
}

