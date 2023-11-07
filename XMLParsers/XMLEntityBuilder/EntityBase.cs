using SprintZero1.Entities;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    /// <summary>
    /// Derived class from Base Entity Builder for building blocks
    /// </summary>
    internal abstract class EntityBase : IEntityParsingBuilder
    {
        /* --------- protected members ----------- */
        protected string _entityName;
        protected int _entityPositionX;
        protected int _entityPositionY;
        private int _destPointX;
        private int _destPointY;

        public int EntityPositionX { set => _destPointX = value; }
        public int EntityPositionY { set => _destPointY = value; }
        public string EntityName { set => _entityName = value; }

        public abstract IEntity CreateEntity();
    }
}
