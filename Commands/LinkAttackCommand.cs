using SprintZero1.Entities;
using System.Diagnostics;

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
            Debug.WriteLine("Hello?");
            combatEntity.Attack();
        }

    }
}
