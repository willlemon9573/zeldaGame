using SprintZero1.Sprites;

namespace SprintZero1.Items
{
    internal interface IGameItem
    {
        /// <summary>
        /// Get/Set the item id
        /// </summary>
        int ItemID { get; } /* maybe use, maybe not*/
        /// <summary>
        /// Get/Set Item Name
        /// </summary>
        string ItemName { get; }
        /// <summary>
        /// Set and/or get the item sprite
        /// </summary>
        ISprite ItemSprite { get; set; }
    }
}
