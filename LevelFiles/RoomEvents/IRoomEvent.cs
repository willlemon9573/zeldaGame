namespace SprintZero1.LevelFiles.RoomEvents
{
    /// <summary>
    /// The interface to handle specific game events
    /// </summary>
    internal interface IRoomEvent
    {
        /// <summary>
        /// Trigger the room event
        /// </summary>
        void TriggerEvent();

        /// <summary>
        /// Check if the event has been triggered
        /// </summary>
        /// <returns></returns>
        bool CanTriggerEvent();
    }
}
