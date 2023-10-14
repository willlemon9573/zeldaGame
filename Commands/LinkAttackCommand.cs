using SprintZero1.Entities;

namespace SprintZero1.Commands
{
    internal class LinkAttackCommand : ICommand
    {

        ICombatEntity combatEntity;
        public LinkAttackCommand(IEntity entity)
        {
            combatEntity = (ICombatEntity)entity;
        }

        public void Execute()
        {
            combatEntity.Attack();
        }

    }
}
