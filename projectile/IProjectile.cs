using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SprintZero1.projectile
{
    internal interface IProjectile
    {

        public void Update(GameTime gameTime);

        void MoveProjectile(float moveSpeed);
    }
}