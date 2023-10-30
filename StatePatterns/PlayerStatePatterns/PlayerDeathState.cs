using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    internal class PlayerDeathState : BasePlayerState
    {
        public PlayerDeathState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override void Request()
        {
            // TODO: Handle changing sprites
        }

        public override void Update(GameTime gameTime)
        {
            // Handle Updating the game
        }
    }
}
