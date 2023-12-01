using SprintZero1.Entities;

namespace SprintZero1.Commands
{
    /// <summary>
    /// The BombAttackCommand class defines the behavior for an entity's bomb attack.
    /// This command is a part of a command pattern that encapsulates an action (bomb attack)
    /// and its parameters (the entity performing the attack). It represents a strategy
    /// for attacking with a bomb.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BombAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        private readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the BombAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the bomb attack.</param>
        public BombAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a bomb attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "bomb" parameter
            combatEntity.Attack();
        }
    }
}
