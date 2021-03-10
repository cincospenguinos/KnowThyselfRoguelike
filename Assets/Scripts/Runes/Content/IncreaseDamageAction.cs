public class IncreaseDamageAction : RuneAction {
  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void Apply() {
    OwningEntity.DamageModifier += 2;
  }
}