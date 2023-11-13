using SprintZero1.Entities;
using SprintZero1.Entities.LootableItemEntity;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PickupTriforcePieceCommand : ICommand
    {
        PlayerEntity _player;
        ILootableEntity _triforcePiece;
        public PickupTriforcePieceCommand(ICollidableEntity playerEntity, ICollidableEntity triforcePiece)
        {
            _player = playerEntity as PlayerEntity;
            _triforcePiece = triforcePiece as ILootableEntity;
        }

        public void Execute()
        {
            /* triforce should technically be changed to be in the player's hand */
            _triforcePiece.Remove();
            /* don't know if this is implemented */
            _player.TransitionToState(State.InteractingWithItem);
        }
    }
}
