namespace SprintZero1.Enums
{
    /// <summary>
    /// Holds enums for direction
    /// </summary>
    public enum Direction
    {
        North,
        South,
        West,
        East
    }
    /// <summary>
    /// Holds enums for states
    /// </summary>
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
    /// <summary>
    /// The list of stackable items link can hold
    /// </summary>
    public enum Items
    {
        GreenRupee,
        BlueRupee,
        Bomb,
        Arrow,
        Clock,
        HeartContainer,
        RecoveryHeart,
        DungeonKey,
        TriforceFragment
    }
    /// <summary>
    /// Items that are found in a dungeon
    /// </summary>
    public enum DungeonItems
    {
        Level1Compass,
        Level1Map,
    }
    /// <summary>
    /// Holds the enums for weapons 
    /// </summary>
    public enum EquipmentItem
    {
        WoodenSword,
        WhiteSword,
        MagicalSword,
        WoodenShield,
        MagicalShield,
        Boomerang,
        BetterBoomerang,
        Bomb,
        Bow,
        BetterBow,
        MagicalRod
    }
}
