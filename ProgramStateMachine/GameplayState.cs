/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero1.Enums;
using SprintZero1.Managers;
namespace SprintZero1.ProgramStateMachine
{
    public class GameplayState : IGameState
    {
        private readonly ProgramManager programManager;

        public GameplayState(ProgramManager programManager)
        {
            this.programManager = programManager;
        }

        public void Enter()
        {
        }

        public void Update(GameTime gameTime)
        {
            programManager.Update(gameTime);
        }

        public void Exit()
        {
        }
    }

    public class PauseState : IGameState
    {
        public void Enter()
        {
            // 初始化暂停状态
        }

        public void Update(GameTime gameTime)
        {
            // 在暂停状态下，不更新游戏逻辑
        }

        public void Exit()
        {
            // 清理暂停状态
        }
    }


}*/