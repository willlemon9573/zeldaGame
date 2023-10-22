namespace SprintZero1.StatePatterns.CombatStatePatterns
{
    internal class DisabledCombatState : BaseCombatState
    {
        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
