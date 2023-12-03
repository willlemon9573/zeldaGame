using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands
{
    /// <summary>
    /// The BowAttackCommand class defines the behavior for an entity's bow attack.
    /// This command is a part of a command pattern that encapsulates an action (bow attack)
    /// and its parameters (the entity performing the attack). It enables the entity to
    /// perform attacks using a bow.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BowAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        private readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the BowAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the bow attack.</param>
        public BowAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a bow attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "Bow" parameter
            combatEntity.Attack();
        }
    }
}
