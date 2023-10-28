/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
namespace SprintZero1.ProgramStateMachine
{
    public class GameStateMachine
    {
        private IGameState currentState;

        public GameStateMachine(IGameState initialState)
        {
            currentState = initialState;
            currentState.Enter();
        }

        public void ChangeState(IGameState newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }
    }


}*/