using SprintZero1.Entities;

namespace SprintZero1.Commands
{
    /// <summary>
    /// The MagicFireAttackCommand class defines the behavior for an entity's magic fire attack.
    /// This command is a part of a command pattern that encapsulates an action (magic fire attack)
    /// and its parameters (the entity performing the attack). It represents a strategy
    /// for attacking with magical fire.
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class MagicFireAttackCommand : ICommand
    {
        // Field for storing the reference to the combat entity
        readonly ICombatEntity combatEntity;

        /// <summary>
        /// Initializes a new instance of the MagicFireAttackCommand class.
        /// </summary>
        /// <param name="entity">The combat entity that will perform the magic fire attack.</param>
        public MagicFireAttackCommand(ICombatEntity entity)
        {
            // Assign the provided entity to the combatEntity field
            combatEntity = entity;
        }

        /// <summary>
        /// Executes the command to perform a magic fire attack with the associated entity.
        /// </summary>
        public void Execute()
        {
            // Triggers the attack method of the combat entity with a "MagicFire" parameter
            combatEntity.Attack();
        }
    }
}
