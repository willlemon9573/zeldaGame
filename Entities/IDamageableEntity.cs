namespace SprintZero1.Entities
{
    internal interface IDamageableEntity
    {
        /// <summary>
        /// Property for setting and getting health
        /// </summary>
        int Health { get; set; }
        /// <summary>
        /// Make the entity take a specific amount of damage
        /// </summary>
        /// <param name="damage">The amount of damage the entity takes</param>
        void TakeDamage(int damage);
    }
}
