public abstract class RuneAction {
  protected Entity OwningEntity;

  public RuneAction(Entity entityToModify) {
    OwningEntity = entityToModify;
  }

  public abstract void Apply();
  public abstract RuneAction Clone(Entity otherEntity);
}