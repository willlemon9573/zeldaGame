using SprintZero1.Entities;
using SprintZero1.GameStateMenu;

namespace SprintZero1.Controllers
{
    internal interface IControllerForItemSelection
    {
        /// <summary>
        /// Loads the controls for the specific controller
        /// </summary>
        void LoadControls(Game1 game, IEntity player, ItemSelectionMenu itemSelectionMenu);

        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update();
    }
}
