public class RuneAction {
  private Entity _entity;

  public RuneAction(Entity entityToModify) {
    _entity = entityToModify;
  }

  public void Apply() {
    _entity.DamageModifier += 2;
  }
}