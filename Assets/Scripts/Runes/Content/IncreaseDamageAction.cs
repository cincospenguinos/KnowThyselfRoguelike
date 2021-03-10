public class IncreaseDamageAction : RuneAction {
  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void Apply() {
    if (OwningEntity != null) {
      OwningEntity.DamageModifier += 2;
    }
  }

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageAction(OwningEntity);
  }
}