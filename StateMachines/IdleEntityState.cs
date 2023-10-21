using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StateMachines
{
    internal class IdleEntityState : BaseEntityState
    {
        public IdleEntityState(IMovableEntity entity) : base(entity)
        {
            this.entity = entity;
            this.Velocity = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            this.entity.Move(this.Velocity);
        }
    }
}
