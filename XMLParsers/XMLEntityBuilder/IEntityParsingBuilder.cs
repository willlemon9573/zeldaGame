using SprintZero1.Entities;

namespace SprintZero1.XMLParsers.XMLEntityBuilder
{
    internal interface IEntityParsingBuilder
    {
        int EntityPositionX { set; }
        int EntityPositionY { set; }
        string EntityName { set; }

        IEntity CreateEntity();
    }
}
