public class IncreaseDamageAction : RuneAction {
  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void Apply() {
    OwningEntity.DamageModifier += 2;
  }

  public override string Text() => $"increase your damage by 2.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageAction(otherEntity);
  }
}