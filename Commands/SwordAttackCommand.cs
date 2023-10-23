using SprintZero1.Entities;
namespace SprintZero1.Commands
{
    internal class SwordAttackCommand : ICommand
    {

        readonly ICombatEntity combatEntity;
        readonly PlayerEntity playerEntity;
        public SwordAttackCommand(ICombatEntity entity)
        {
            combatEntity = entity;
            playerEntity = entity as PlayerEntity;
        }

        public void Execute()
        {
            combatEntity.Attack("sword");
        }
    }
}
