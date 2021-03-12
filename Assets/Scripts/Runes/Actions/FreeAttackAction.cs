public class FreeAttackAction : RuneAction {
  public override int Threshold => 28;

  public FreeAttackAction(Entity e) : base(e) { }

  public override void Perform() {
    OwningEntity.FreeAttacks += 1;
  }

  public override RuneAction Clone(Entity otherEntity) {
    return new FreeAttackAction(otherEntity);
  }

  public override string Text() => "Gain a free attack (currently " + OwningEntity.FreeAttacks + ")";
}