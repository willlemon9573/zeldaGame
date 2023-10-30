
namespace SprintZero1.Enums
{
    /// <summary>
    /// Holds enums for direction 
    /// </summary>
    public enum Direction
    {
        North, // Represents the North direction
        South, // Represents the South direction
        West,  // Represents the West direction
        East   // Represents the East direction
    }

    /// <summary>
    /// Holds enums for states
    /// </summary>
    public enum State
    {
        Attacking,     // Represents the state of attacking
        Moving,        // Represents the state of moving
        TakingDamage,  // Represents the state of taking damage
        Idle,          // Represents the state of being idle
        Interacting    // Represents the state of interacting
    }

    public enum AttackType
    {
        Melee,  // Represents a melee attack type
        Ranged  // Represents a ranged attack type
    }

    /// <summary>
    /// Enums pertaining to the items the player can pick up
    /// </summary>
    public enum UsableItems
    {
        Clock,            // Freezes enemies and makes link invulnerable
        HeartContainer,   // Increases player's health by 1 heart
        RecoveryHeart,    // Recovers player's health by 1 heart
        TriforceFragment  // A fragment of the Triforce
    }

    public enum StackableItems
    {
        Rupee,       // A standard rupee (amount = 1)
        BlueRupee,   // A blue rupee (amount = 5)
        Bomb,        // A bomb that can be used as a weapon or tool
        Arrow,       // An arrow that can be used with a bow
        DungeonKey,  // A key that can be used to unlock areas in a dungeon
    }

    /// <summary>
    /// Items that are found in a dungeon
    /// </summary>
    public enum DungeonItems
    {
        Level1Compass,  // A compass found in level 1  dungeon 
        Level1Map       // A map found in level 1 dungeon 
    }

    /// <summary>
    /// Holds the enums for weapons 
    /// </summary>
    public enum EquipmentItem
    {
        WoodenSword,     // A basic wooden sword weapon 
        WhiteSword,      // An upgraded white sword weapon 
        MagicalSword,    // A powerful magical sword weapon 
        WoodenShield,    // A basic wooden shield for defense 
        MagicalShield,   // An upgraded magical shield for defense 
        Boomerang,       // A boomerang weapon that can hit enemies from a distance 
        BetterBoomerang, // An upgraded boomerang weapon with improved capabilities 
        Bomb,            // A bomb that can cause area damage 
        Bow,             // A bow that can shoot arrows at enemies 
        BetterBow,       // An upgraded bow with improved capabilities 
        MagicalRod       // A magical rod weapon with special abilities 
    }

}
