using System;
namespace SprintZero1.weapon
{
	public class IProjectile
    {
        internal interface IProjectile
        {

            public void Update(GameTime gameTime);

            private void MoveProjectile(float moveSpeed);
        }
	}
}