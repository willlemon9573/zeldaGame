using SprintZero1.Entities.EnemyEnetities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns.BossStatePatterns.AquamentusStatePattern;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class BossStateFactory
    {

        private readonly Dictionary<State, IEnemyState> _stateTransitionMap;
        public BossStateFactory(BaseBossEntity boss)
        {
            _stateTransitionMap = new Dictionary<State, IEnemyState>()
            {
                { State.Moving, new AquamentusMovingState(boss) },
                { State.Attacking, new AquamentusAttackingState(boss) },
                { State.Die, new AquamentusDeathState(boss) }
            };
        }

        public IEnemyState GetState(State newState)
        {
            return _stateTransitionMap[newState];
        }

    }
}
