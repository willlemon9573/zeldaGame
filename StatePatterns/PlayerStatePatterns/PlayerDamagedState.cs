using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerDamagedState : BasePlayerState
    {
        public PlayerDamagedState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override void Request()
        {
            // TODO: handle the reuqest
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO need to make link draw with tint or whatever
        }

        public override void Update(GameTime gameTime)
        {
            // TODO Handle updating link
        }
    }
}
