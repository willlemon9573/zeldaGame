using SprintZero1.Enums;
using SprintZero1.StatePatterns.GameStatePatterns;
using SprintZero1.StatePatterns.StatePatternInterfaces;
using System.Collections.Generic;

namespace SprintZero1.Managers
{
    internal static class GameStatesManager
    {
        private static Dictionary<GameState, IGameState> _gameStateMap;

        /// <summary>
        /// Creates the map that will contain the required functions for game state changing
        /// </summary>
        /// <param name="game">The current game</param>
        public static void InitializeGameStateMap(Game1 game)
        {
            _gameStateMap = new Dictionary<GameState, IGameState>()
            {
                { GameState.GameOver, new GameOverState(game) },
                { GameState.ItemSelectionScreen, new GameItemSelectionState(game) },
                { GameState.LevelCompleted, new GameBeatState(game) },
                { GameState.Paused, new GamePausedState(game) },
                { GameState.Playing, new GamePlayingState(game) },
                { GameState.Reset, new GameResetState(game) },
                { GameState.RoomTransition, new GameRoomTransitionState(game) }
            };
        }

        /// <summary>
        /// Gets the new state of the game when a game state change happens
        /// </summary>
        /// <param name="newState">The new state the game changes to</param>
        /// <returns>The new state of the game</returns>
        public static IGameState GetGameState(GameState newState)
        {
            return _gameStateMap[newState];
        }

        public static void Reset()
        {
            _gameStateMap.Clear();
        }
    }
}
