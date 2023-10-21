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
        int Health { get; set; }
        /* need to give a combat entity weapons */
        /// <summary>
        /// Executes the attack action.
        /// </summary>
        public void Attack(string weaponName);
        /// <summary>
        /// Applies damage to the combat entity
        /// </summary>
        public void TakeDamage();

        /// <summary>
        /// Handles the death of the combat entity
        /// </summary>
        public void Die();
    }
}
