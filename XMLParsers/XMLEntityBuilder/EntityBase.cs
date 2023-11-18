using SprintZero1.Entities;

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
        public int EntityPositionX { set => _entityPositionX = value; }
        public int EntityPositionY { set => _entityPositionY = value; }
        public string EntityName { set => _entityName = value; }
        public abstract IEntity CreateEntity();


    }
}
