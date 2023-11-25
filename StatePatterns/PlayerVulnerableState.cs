using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.PlayerStatePatterns;

namespace SprintZero1.StatePatterns
{
    internal class PlayerVulnerableState : BasePlayerState
    {
        public PlayerVulnerableState(PlayerEntity playerEntity) : base(playerEntity)
        {

        }

        public override void Request()
        {
            Direction playerDirection = _playerEntity.Direction;
            _playerEntity.PlayerSprite = _linkSpriteFactory.GetLinkSprite(playerDirection);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
