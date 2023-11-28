using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerVulnerableState : BasePlayerState
    {
        public PlayerVulnerableState(PlayerEntity playerEntity) : base(playerEntity)
        {

        }

        public override void Request()
        {
            Direction playerDirection = _playerEntity.Direction;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetMovingSprite(playerDirection);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
