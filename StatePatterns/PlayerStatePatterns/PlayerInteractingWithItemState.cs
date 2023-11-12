using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StatePatterns.PlayerStatePatterns
{
    /// <summary>
    /// State to handle when the player picks up a new item
    /// </summary>
    internal class PlayerInteractingWithItemState : BasePlayerState
    {
        public PlayerInteractingWithItemState(PlayerEntity playerEntity) : base(playerEntity)
        {
        }

        public override void Request()
        {
            // TODO : Needs to update player's sprite to interacting sprite
        }

        public override void Update(GameTime gameTime)
        {
            // TODO : Update player
        }
    }
}
