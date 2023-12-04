using SprintZero1.Entities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;
using SprintZero1.Managers;
using SprintZero1.StatePatterns.GameStatePatterns;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupTriforcePieceCommand : ICommand
    {
        private readonly PlayerEntity _player;
        private readonly ILootableEntity _triforcePiece;
        private readonly GamePlayingState _state;

        public PickupTriforcePieceCommand(ICollidableEntity playerEntity, ICollidableEntity triforcePiece)
        {
            _player = playerEntity as PlayerEntity;
            _triforcePiece = triforcePiece as ILootableEntity;
            _state = GameStatesManager.GetGameState(GameState.Playing) as GamePlayingState;
        }

        public void Execute()
        {
            /* triforce should technically be changed to be in the player's hand */
            _triforcePiece.Remove();
            /* don't know if this is implemented */
            _player.TransitionToState(State.InteractingWithItem);
            if (_player is PlayerEntity player)
            {
                player.PickedupItem(_triforcePiece);
            }
            _state.ChangeGameState(GameState.LevelCompleted);
        }
    }
}
