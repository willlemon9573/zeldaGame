namespace SprintZero1.Items
{
    /// <summary>
    /// Interface for things such as Rupees, Arrows, Bombs, etc
    /// </summary>
    internal interface IUsableItem : IGameItem
    {
        /// <summary>
        /// Get the current amount of the stackable item 
        /// </summary>
        int CurrentCount { get; }
        /// <summary>
        /// Get the max capacity of the stack item
        /// </summary>
        int MaxCapacity { get; }
        /// <summary>
        /// Increment the current stock by the amount supplied
        /// </summary>
        /// <param name="amount">The amount to be incremented</param>
        void IncrementCount(int amount);
        /// <summary>
        /// Use the item whether it be a rupee, arrow, bomb, etc.
        /// </summary>
        void UseItem();

    }
}
