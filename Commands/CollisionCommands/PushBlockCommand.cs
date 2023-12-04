using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;

namespace SprintZero1.Commands.CollisionCommands
{
    internal class PushBlockCommand : ICommand
    {
        private readonly ICollidableEntity _playerEntity;
        private readonly ICollidableEntity _movableBlock;

        private readonly ICommand _pushBack;
        public PushBlockCommand(ICollidableEntity player, ICollidableEntity block)
        {
            _playerEntity = player;
            _movableBlock = block;
            _pushBack = new PushBackCommand(player, block);
        }
        public void Execute()
        {
            Direction playerDir = (_playerEntity as ICombatEntity).Direction;
            Direction blockDir = (_movableBlock as IMovableEntity).Direction;

            if (playerDir == blockDir)
            {
                (_movableBlock as IMovableEntity).Move();
            }
            else
            {
                _pushBack.Execute();
            }
        }
    }
}

