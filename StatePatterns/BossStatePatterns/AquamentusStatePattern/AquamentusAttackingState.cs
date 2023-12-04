using Microsoft.Xna.Framework;
using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.Entities.WeaponEntities;
using SprintZero1.Enums;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern
{

    internal class AquamentusAttackingState : BaseBossState
    {
        private const int MaxProjectiles = 3;
        private const float MaxStateTime = 1 / 2f;
        private float _elapsedTime;
        private List<IWeaponEntity> _projectileList;
        public AquamentusAttackingState(BaseBossEntity boss) : base(boss)
        {
            _projectileList = new List<IWeaponEntity>();
            // aquamentus fires 3 projectiles at an angle. These will be used to calculate the direction the projectile should go
            float westAngle = 155;
            float northwestAngle = 180;
            float southwestAngle = 205;
            float[] angles = new float[] { northwestAngle, westAngle, southwestAngle };
            // create the projectiles aquamentus fires
            for (int i = 0; i < MaxProjectiles; i++)
            {
                _projectileList.Add(new AquamentusProjectileEntity(angles[i]));
            }
        }

        public override void Request()
        {
            if (!_canTransition) { return; }
            _canTransition = false;
            _elapsedTime = 0;
            foreach (var projectile in _projectileList)
            {
                projectile.UseWeapon(Direction.East, _boss.Position);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_elapsedTime >= MaxStateTime)
            {
                _canTransition = true;
                TransitionState(State.Moving);
            }
        }
    }
}
