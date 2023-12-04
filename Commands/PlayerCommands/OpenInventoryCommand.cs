using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class OpenInventoryCommand : BaseChangeGameStateCommand
    {
        private readonly IEntity _player;
        public OpenInventoryCommand(IEntity player) : base()
        {
            _player = player;
        }

        /// <summary>
        /// Execute the command for opening the player inventory
        /// </summary>
        public override void Execute()
        {
            if (_player is ICombatEntity player && player.Health <= 0)
            {
                return;
            }
            GameStatesManager.ChangeGameState(GameState.ItemSelectionScreen);
            (GameStatesManager.CurrentState as GameItemSelectionState).CurrentPlayer = _player;
            GameStatesManager.CurrentState.Handle();
        }
    }
}
