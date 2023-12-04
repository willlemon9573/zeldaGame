using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.Commands
{
    /// <summary>
    /// The BetterBoomerangAttackCommand class defines the behavior for an entity's enhanced boomerang attack.
    /// This command is a part of a command pattern that encapsulates an action (better boomerang attack)
    /// and its parameters (the entity performing the attack). It represents a strategy
    /// for attacking with an improved version of a boomerang.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class BetterBoomerangAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        private readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the BetterBoomerangAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the better boomerang attack.</param>
        public BetterBoomerangAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a better boomerang attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "betterboomerang" parameter
            combatEntity.Attack();
        }
    }
}
