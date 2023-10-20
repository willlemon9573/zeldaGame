using SprintZero1.Entities;

namespace SprintZero1.Commands
{
    internal class SwordAttackCommand : ICommand
    {

        readonly ICombatEntity combatEntity;
        public SwordAttackCommand(IEntity entity)
        {
            combatEntity = (ICombatEntity)entity;
        }

        public void Execute()
        {
            combatEntity.Attack("sword");
        }
    }
}
