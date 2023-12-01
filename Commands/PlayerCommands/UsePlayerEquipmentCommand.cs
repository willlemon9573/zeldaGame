using SprintZero1.Entities;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class UsePlayerEquipmentCommand : ICommand
    {
        private PlayerEntity _player;
        public UsePlayerEquipmentCommand(IEntity player)
        {
            _player = player as PlayerEntity;
        }

        public void Execute()
        {
            if (_player.EquipmentSlot is not null)
            {
                _player.CurrentUsableWeapon = _player.EquipmentSlot;
                _player.Attack();
            }
        }
    }
}
