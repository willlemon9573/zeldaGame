namespace SprintZero1.Entities.DungeonRoomEntities.Doors
{
    internal interface IDoorEntity : ICollidableEntity
    {
        /// <summary>
        /// When overriden handles what happens when a door is opened
        /// </summary>
        public void Open();
    }
}
