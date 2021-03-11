public class FreeAttackAction : RuneAction {
    private const int CHARGE_THRESHOLD = 28;

    public FreeAttackAction(Entity e) : base(e) {}

    public override void ReceiveCharge(int amount) {
        CurrentCharge += amount;

        while (CurrentCharge > CHARGE_THRESHOLD) {
            OwningEntity.FreeAttacks += 1;
            CurrentCharge -= CHARGE_THRESHOLD;
        }
    }

    public override RuneAction Clone(Entity otherEntity) {
        return new FreeAttackAction(otherEntity);
    }

    public override string Text() => " gain a free attack (currently " + OwningEntity.FreeAttacks + ")";
}