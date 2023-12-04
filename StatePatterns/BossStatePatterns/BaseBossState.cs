using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Enums;
using SprintZero1.Factories;
using SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.StatePatterns.BossStatePatterns
{
    internal abstract class BaseBossState : IEnemyState
    {
        private const string BossDamageSound = "boss_hit";
        protected readonly SoundEffect _bossDamageSound;
        protected readonly BaseBossEntity _boss;
        protected bool _canTransition;
        protected Dictionary<State, Func<IEnemyState>> _stateTransitionMap;

        public bool CanTransition { get { return _canTransition; } }

        public BaseBossState(BaseBossEntity boss)
        {
            _boss = boss;
            _canTransition = true;
            _stateTransitionMap = new Dictionary<State, Func<IEnemyState>>()
            {
                { State.Moving, () => new AquamentusMovingState(boss) },
                { State.Attacking, () => new AquamentusAttackingState(boss) },
                { State.Die, () => new AquamentusDeathState(boss) }
            };
            _bossDamageSound = SoundFactory.GetSound(BossDamageSound);
        }


        public void BlockTransition()
        {
            _canTransition = false;
        }

        public void UnblockTranstion()
        {
            _canTransition = true;
        }

        public void ChangeDirection(Direction newDirection)
        {
            _boss.Direction = newDirection;
        }

        public void TransitionState(State newState)
        {
            if (!_canTransition) { return; }

            if (_stateTransitionMap.TryGetValue(newState, out Func<IEnemyState> action))
            {
                _boss.CurrentState = action();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _boss.Sprite.Draw(spriteBatch, _boss.Position, Color.White);
        }

        public abstract void Request();

        public abstract void Update(GameTime gameTime);
    }
}
