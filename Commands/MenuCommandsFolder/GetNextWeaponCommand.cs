using SprintZero1.GameStateMenu;

namespace SprintZero1.Commands.MenuCommandsFolder
{
    /// <summary>
    /// The GetNextWeaponCommand class defines the behavior for cycling to the next weapon in the item selection menu.
    /// This command is part of a command pattern that allows for encapsulating an action (changing the weapon selection)
    /// and its parameters (the item selection menu context).
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class GetNextWeaponCommand : ICommand
    {
        // Field to store the reference to the item selection menu
        private ItemSelectionMenu _itemSelectionMenu;

        /// <summary>
        /// Initializes a new instance of the GetNextWeaponCommand class.
        /// </summary>
        /// <param name="itemSelectionMenu">The item selection menu context for this command.</param>
        public GetNextWeaponCommand(ItemSelectionMenu itemSelectionMenu)
        {
            // Assign the provided item selection menu to the _itemSelectionMenu field
            _itemSelectionMenu = itemSelectionMenu;
        }

        /// <summary>
        /// Executes the command to cycle to the next weapon in the item selection menu.
        /// </summary>
        public void Execute()
        {
            // Calls the method to set the next weapon in the item selection menu
            _itemSelectionMenu.SetNextWeapon();
        }
    }
}
