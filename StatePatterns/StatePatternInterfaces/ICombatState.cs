using SprintZero1.Enums;

namespace SprintZero1.StatePatterns.StatePatternInterfaces
{
    internal interface ICombatState
    {
        void Attack();
        void TakeDamage();
        void GetKnockedBack(Direction direction);
        void Reset();
        void Update();
    }
}
