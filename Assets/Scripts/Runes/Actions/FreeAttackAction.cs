public class FreeAttackAction : RuneAction {
    public FreeAttackAction(Entity e) : base(e) {}

    public override void ReceiveCharge(int amount) {
        OwningEntity.FreeAttacks += 1;
    }

    public override RuneAction Clone(Entity otherEntity) {
        return new FreeAttackAction(otherEntity);
    }

    public override string Text() => " gain a free attack (currently " + OwningEntity.FreeAttacks + ")";
}