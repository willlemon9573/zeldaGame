using SprintZero1.Entities;
using System.Collections.Generic;

namespace SprintZero1.InventoryFiles
{
    internal class PlayerInventory
    {
        // have inventory manager pass stuff to player inventory
        Dictionary<IEntity, int> _finiteItems;

        public PlayerInventory()
        {
            _finiteItems = new Dictionary<IEntity, int>();

        }




    }
}
