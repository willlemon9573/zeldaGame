using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.PlayerStatePatterns;
namespace SprintZero1.Commands
{

    internal class ReturnPlayerToIdleCommand : ICommand
    {
        private readonly PlayerEntity playerEntity;

        public ReturnPlayerToIdleCommand(IEntity entity)
        {
            playerEntity = (PlayerEntity)entity;
        }

        public void Execute()
        {
            // return if player state is already idle
            if (playerEntity.PlayerSprite is PlayerIdleState) { return; }
            playerEntity.TransitionToState(State.Idle);
        }
    }
}
