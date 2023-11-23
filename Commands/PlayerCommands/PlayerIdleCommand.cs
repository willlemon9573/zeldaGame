using SprintZero1.Entities;
using SprintZero1.Enums;

namespace SprintZero1.Commands.PlayerCommands
{
    /// <summary>
    /// Handles transitioning player to idle state
    /// </summary>
    internal class PlayerIdleCommand : ICommand
    {
        private readonly PlayerEntity _player;
        public PlayerIdleCommand(PlayerEntity player)
        {
            _player = player;
        }

        public void Execute()
        {
            _player.TransitionToState(State.Idle);
        }
    }
}
