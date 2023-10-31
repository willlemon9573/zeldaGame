namespace SprintZero1.Controllers
{
    internal interface IController
    {
        /// <summary>
        /// Loads the controls for the specific controller
        /// </summary>
        void LoadControls();

        /// <summary>
        /// Updates game based on which input is read
        /// </summary>
        void Update();
    }
}
