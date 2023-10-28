namespace SprintZero1.InventoryFiles
{
    /// <summary>
    /// Interface for items such as the Map, tunics, etc
    /// </summary>
    internal interface IUtilityItem : IPlayerItem
    {
        void UtilityItemAction();
    }
}
