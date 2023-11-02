using SprintZero1.Enums;
using SprintZero1.Factories;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class CloseInventoryCommand : BaseChangeGameStateCommand
    {
        /// <summary>
        /// Command for closing the player inventory
        /// </summary>
        /// <param name="gameChangeStateHandler">Delegate that points to the state changing function</param>
        /// <param name="gameStateHandler">Delegate that points to the state handling function</param>
        public CloseInventoryCommand(Game1 game) : base(game)
        {
        }

        /// <summary>
        /// Execute the command for closing player inventory
        /// </summary>
        public override void Execute()
        {
            _game.GameState = GameStateFactory.GetGameState(GameState.Playing);
        }
    }
}
