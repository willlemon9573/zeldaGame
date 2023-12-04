using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands
{
    /// <summary>
    /// The BetterBowAttackCommand class defines the behavior for an entity's better bow attack.
    /// This command is a part of a command pattern that encapsulates an action (bow attack)
    /// and its parameters (the entity performing the attack). It represents an enhanced version
    /// of a bow attack.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BetterBowAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        private readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the BetterBowAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the better bow attack.</param>
        public BetterBowAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a better bow attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "BetterBow" parameter
            combatEntity.Attack();
        }
    }
}
