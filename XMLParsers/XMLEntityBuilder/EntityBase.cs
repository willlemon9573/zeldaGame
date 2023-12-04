using SprintZero1.Entities.EntityInterfaces;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    /// <summary>
    /// Abstract class for creating objects for the XML Parsing
    /// </summary>
    internal abstract class EntityBase : IEntityParsingBuilder
    {
        /* --------- protected members ----------- */
        protected string _entityName;
        protected int _entityPositionX;
        protected int _entityPositionY;
        /// <summary>
        /// The following properties are used to build the entity.
        /// </summary>
        public int EntityPositionX { set => _entityPositionX = value; }
        public int EntityPositionY { set => _entityPositionY = value; }
        public string EntityName { set => _entityName = value; }
        /// <summary>
        /// Create the entity
        /// </summary>
        /// <returns>A new instance of the desired entity</returns>
        public abstract IEntity CreateEntity();


    }
}
