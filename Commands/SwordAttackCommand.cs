using SprintZero1.Entities;

namespace SprintZero1.Commands
{
    internal class SwordAttackCommand : ICommand
    {

        readonly ICombatEntity combatEntity;
        public SwordAttackCommand(ICombatEntity entity)
        {
            combatEntity = entity;
        }

        public void Execute()
        {
            combatEntity.Attack("sword");
        }
    }
}
