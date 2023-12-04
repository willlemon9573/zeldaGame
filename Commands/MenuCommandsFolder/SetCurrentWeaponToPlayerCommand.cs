using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Enums;
using SprintZero1.GameStateMenu;
using SprintZero1.Managers;

namespace SprintZero1.Commands.MenuCommandsFolder
{
    /// <summary>
    /// The setCurrentWeaponToPlayer class defines the behavior for setting the current weapon to the player entity.
    /// This command is part of a command pattern that encapsulates an action (setting the current weapon to the player)
    /// and its context (the player entity and the item selection menu).
    /// </summary>
    /// <author>Zihe Wang</author>
    internal class SetCurrentWeaponToPlayerCommand : ICommand
    {
        // Fields to store references to the item selection menu and player entity
        private readonly ItemSelectionMenu _itemSelectionMenu;
        private readonly IEntity _player;

        /// <summary>
        /// Initializes a new instance of the setCurrentWeaponToPlayer class.
        /// </summary>
        /// <param name="player">The player entity to which the weapon will be set.</param>
        /// <param name="itemSelectionMenu">The item selection menu context for this command.</param>
        public SetCurrentWeaponToPlayerCommand(IEntity player, ItemSelectionMenu itemSelectionMenu)
        {
            _player = player;
            _itemSelectionMenu = itemSelectionMenu;
        }

        /// <summary>
        /// Executes the command to set the current weapon in the player's inventory.
        /// </summary>
        public void Execute()
        {
            // Retrieve the current weapon from the item selection menu and set it to the player
            EquipmentItem currentWeapon = _itemSelectionMenu.CurrentWeapon;
            if (currentWeapon is EquipmentItem.WoodenSword) { return; };
            PlayerInventoryManager.ChangeEquipment(_player, currentWeapon);
        }
    }
}
