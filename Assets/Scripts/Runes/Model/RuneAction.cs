public abstract class RuneAction {
  public Entity OwningEntity;

  public RuneAction(Entity entityToModify) {
    OwningEntity = entityToModify;
  }

  public abstract void Apply();
  public abstract RuneAction Clone(Entity otherEntity);
}