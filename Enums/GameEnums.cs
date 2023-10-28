namespace SprintZero1.Enums
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public enum State
    {
        Attacking,
        Moving,
        TakingDamage,
        Idle,
        Interacting
    }

    public enum AttackType
    {
        Melee,  
        Ranged  
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        LevelCompleted
    }

}
