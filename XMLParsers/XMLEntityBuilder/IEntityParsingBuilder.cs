using SprintZero1.Entities;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal interface IEntityParsingBuilder
    {
        /// <summary>
        /// Set the entity position x
        /// </summary>
        int EntityPositionX { set; }
        /// <summary>
        /// Set the entity position y
        /// </summary>
        int EntityPositionY { set; }
        /// <summary>
        ///  set the entity name
        /// </summary>
        string EntityName { set; }
        /// <summary>
        /// Create the entity
        /// </summary>
        /// <returns></returns>
        IEntity CreateEntity();
    }
}
