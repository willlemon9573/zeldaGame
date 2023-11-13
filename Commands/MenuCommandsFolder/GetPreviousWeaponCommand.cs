using SprintZero1.Entities;
using SprintZero1.GameStateMenu;

namespace SprintZero1.Commands.MenuCommandsFolder
{
    /// <summary>
    /// The GetPreviousWeaponCommand class defines the behavior for cycling to the previous weapon in the item selection menu.
    /// This command is part of a command pattern that encapsulates an action (changing the weapon selection to the previous one)
    /// and its parameters (the item selection menu context).
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class GetPreviousWeaponCommand : ICommand
    {
        // Field to store the reference to the item selection menu
        private ItemSelectionMenu _itemSelectionMenu;

        /// <summary>
        /// Initializes a new instance of the GetPreviousWeaponCommand class.
        /// </summary>
        /// <param name="itemSelectionMenu">The item selection menu context for this command.</param>
        public GetPreviousWeaponCommand(ItemSelectionMenu itemSelectionMenu)
        {
            // Assign the provided item selection menu to the _itemSelectionMenu field
            _itemSelectionMenu = itemSelectionMenu;
        }

        /// <summary>
        /// Executes the command to cycle to the previous weapon in the item selection menu.
        /// </summary>
        public void Execute()
        {
            // Calls the method to set the previous weapon in the item selection menu
            _itemSelectionMenu.SetPreviousWeapon();
        }
    }
}
