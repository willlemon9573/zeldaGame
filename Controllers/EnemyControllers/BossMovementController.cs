internal class BossMovementController : IEnemyMovementController
{
    private readonly ICombatEntity _bossEntity;
    private readonly List<IEntity> _players;
    private readonly double _attackInterval = 1.5; 
    private double _timeSinceLastAttack = 0;

    public BossMovementController(ICombatEntity bossEntity, List<IEntity> players)
    {
        _bossEntity = bossEntity;
        _players = players;
    }

    public void Update(GameTime gameTime)
    {
        double elapsed = gameTime.ElapsedGameTime.TotalSeconds;
        _timeSinceLastAttack += elapsed;

        if (_timeSinceLastAttack >= _attackInterval)
        {
            AttackNearestPlayer();
            _timeSinceLastAttack = 0;
        }

        MoveInPattern();
    }

    private void AttackNearestPlayer()
    {
        IEntity nearestPlayer = FindNearestPlayer(_bossEntity.Position, _players);
        if (nearestPlayer != null)
        {
            (_bossEntity as EnemyBasedEntity)?.PerformAttack();
        }
    }

    public void MoveInPattern(GameTime gameTime)
    {
        Vector2 bossPosition = _bossEntity.Position;
        IEntity nearestPlayer = FindNearestPlayer(bossPosition, _players);

        if (nearestPlayer != null)
        {
            Vector2 playerPosition = nearestPlayer.Position;
            float distanceToPlayer = Vector2.Distance(bossPosition, playerPosition);
            float healthPercentage = _bossEntity.Health / _bossEntity.MaxHealth;

            if (healthPercentage < 0.5f && distanceToPlayer < CautiousDistance)
            {
                _targetPosition = bossPosition + (bossPosition - playerPosition);
            }
            else if (distanceToPlayer > AggressiveDistance)
            {
                _targetPosition = playerPosition;
            }

            Vector2 direction = Vector2.Normalize(_targetPosition - bossPosition);
            _bossEntity.Position += direction * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _bossEntity.Position = Vector2.Clamp(_bossEntity.Position, new Vector2(0, 0), new Vector2(GameWorldWidth, GameWorldHeight));

            UpdateBossDirection(direction);
        }
    }

    private IEntity FindNearestPlayer(Vector2 bossPosition, List<IEntity> players)
    {
        return players.OrderBy(p => Vector2.Distance(bossPosition, p.Position)).FirstOrDefault();
    }
    private void UpdateBossDirection(Vector2 direction)
    {
        if (Math.Abs(direction.X) > Math.Abs(direction.Y))
        {
            _bossEntity.Direction = direction.X > 0 ? Direction.East : Direction.West;
        }
        else
        {
            _bossEntity.Direction = direction.Y > 0 ? Direction.South : Direction.North;
        }
    }
}
