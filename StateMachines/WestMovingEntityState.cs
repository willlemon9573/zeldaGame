using Microsoft.Xna.Framework;
using SprintZero1.Entities;

namespace SprintZero1.StateMachines
{
    internal class WestMovingEntityState : BaseEntityState
    {
        public WestMovingEntityState(IMovableEntity entity) : base(entity)
        {
            this.entity = entity;
            this.Velocity = new Vector2(-1, 0);
        }

        public override void Update(GameTime gameTime)
        {
            this.entity.Move(this.Velocity);
        }
    }
}
