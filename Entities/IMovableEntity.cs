using Microsoft.Xna.Framework;
using SprintZero1.Enums;

namespace SprintZero1.Entities
{
    internal interface IMovableEntity
    {
        State State { get; set; }
        Direction Direction { get; }
        Vector2 Position { get; set; }

        void ChangeDirection(Direction direction);
    }
}
