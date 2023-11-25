using SprintZero1.Entities;
using SprintZero1.Enums;
using SprintZero1.StatePatterns;
using SprintZero1.StatePatterns.PlayerStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System;
using System.Collections.Generic;

namespace SprintZero1.Factories
{
    internal class PlayerStateFactory
    {
        private readonly Dictionary<State, Func<IPlayerState>> _playerStateMap;
        public PlayerStateFactory(PlayerEntity player)
        {
            _playerStateMap = new Dictionary<State, Func<IPlayerState>>()
            {
                {State.Moving, () => new PlayerMovingState(player) },
                {State.Attacking, () => new PlayerAttackingState(player) },
                {State.Idle, () => new PlayerIdleState(player) },
                {State.InteractingWithItem, () => new PlayerInteractingWithItemState(player) },
                {State.TakingDamage, () => new PlayerDamagedState(player)},
                {State.Die, () => new PlayerDeathState(player)},
                {State.KnockedBack, () => new PlayerKnockBackState(player)},
                {State.Paused, () => new PlayerPauseState(player)},
                {State.Vulnerable, () => new PlayerVulnerableState(player)},
                {State.Invulnerable, () => new PlayerInvulnerabilityState(player) }
            };
        }
        /// <summary>
        /// Get the new state the player is in
        /// </summary>
        /// <param name="newState">The new state the player is transitioning to</param>
        /// <returns>The state that the player is transition too</returns>
        public IPlayerState GetPlayerState(State newState)
        {
            return _playerStateMap[newState].Invoke();
        }
    }
}
