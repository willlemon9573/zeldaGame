using SprintZero1.Entities;
using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands.PlayerCommands
{
    internal class SwordAttackCommand : ICommand
    {
        private readonly PlayerEntity _combatEntity;
        /// <summary>
        /// Create an object to handle when an entity needs to attack with a sword
        /// </summary>
        /// <param name="entity">The entity that uses the command</param>
        public SwordAttackCommand(ICombatEntity entity)
        {
            _combatEntity = entity as PlayerEntity;
        }

        public void Execute()
        {
            _combatEntity.CurrentUsableWeapon = _combatEntity.SwordSlot;
            _combatEntity.Attack();
        }
    }
}
