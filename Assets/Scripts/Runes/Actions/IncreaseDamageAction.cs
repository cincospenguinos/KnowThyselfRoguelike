public class IncreaseDamageAction : RuneAction {
  public override int Threshold => 400;

  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.DamageModifier += 1;
  }

  public override string Text() => $"Permanently gain +1 damage.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageAction(otherEntity);
  }
}