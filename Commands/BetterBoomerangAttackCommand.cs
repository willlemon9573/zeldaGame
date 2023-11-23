using SprintZero1.Entities;
namespace SprintZero1.Commands
{
    /// <summary>
    /// The RegularBoomerangAttackCommand class defines the behavior for an entity's boomerang attack.
    /// This command is part of a command pattern that allows for encapsulating an action (boomerang attack)
    /// and its parameters (the entity performing the attack).
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class RegularBoomerangAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the RegularBoomerangAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the attack.</param>
        public RegularBoomerangAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a boomerang attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "boomerang" parameter
            combatEntity.Attack();
        }
    }
}
