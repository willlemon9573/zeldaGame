namespace SprintZero1.Entities
{
    /// <summary>
    /// An interface for entities who engage in combat
    /// </summary>
    internal interface ICombatEntity : IMovableEntity
    {
        /// <summary>
        /// Get and set the combat entity's health
        /// </summary>
        float Health { get; set; }
        /// <summary>
        /// Executes the attack action.
        /// </summary>
        public void Attack();
        /// <summary>
        /// Applies damage to the combat entity
        /// </summary>
        public void TakeDamage(int damage);
        /// <summary>
        /// Handles the death of the combat entity
        /// </summary>
        public void Die();
    }
}
