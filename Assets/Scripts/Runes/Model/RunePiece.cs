public abstract class RuneShard {
  public Entity OwningEntity;

  public bool FromOwnEntity(GameEvent gameEvent) {
    return gameEvent.EmittingEntity == OwningEntity;
  }
  public abstract string Text();
  public abstract string TextFull();
}