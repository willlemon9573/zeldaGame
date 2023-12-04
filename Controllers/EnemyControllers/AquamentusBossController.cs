using Microsoft.Xna.Framework;
using SprintZero1.Commands.EnemyCommands;
using SprintZero1.Entities.EntityInterfaces;
using SprintZero1.LevelFiles;
using System;

namespace SprintZero1.Controllers.EnemyControllers
{
    /// <summary>
    /// A simple controller made for the Aquamentus Boss as he does not require much
    /// </summary>
    internal class AquamentusBossController
    {
        private const float TimeToAttack = 1f;
        private float _elapsedTime;
        private readonly ICombatEntity _aquamentus;
        private readonly RemoveDelegate _entityRemover;
        private bool _isRunning;
        private BossMoveCommand _moveCommand;
        Action currentAction;
        public AquamentusBossController(ICombatEntity aquamentus, RemoveDelegate entityRemover, Rectangle bossMovementBoundary)
        {
            _aquamentus = aquamentus;
            _entityRemover = entityRemover;
            _isRunning = true;
            _elapsedTime = 0;
            _moveCommand = new BossMoveCommand(aquamentus, bossMovementBoundary);
        }

        public void Start()
        {
            _isRunning = true;
            currentAction = Move;
            _elapsedTime = 0f;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Attack()
        {
            _aquamentus.Attack();
            currentAction = Move;
        }

        public void Move()
        {
            _moveCommand.Execute();
        }


        public void Update(GameTime gameTime)
        {

            if (!_isRunning) { return; }

            if (_aquamentus.Health <= 0)
            {
                _entityRemover(_aquamentus);
                Stop();
                return;
            }

            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (_elapsedTime >= TimeToAttack)
            {
                Attack();
                _elapsedTime -= TimeToAttack; // reset attack timer
            }
            Move();

        }
    }
}
