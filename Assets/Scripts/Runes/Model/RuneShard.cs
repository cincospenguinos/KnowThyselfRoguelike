public abstract class RuneShard {
  public Entity OwningEntity;
  public int Upgrades = 0;

  public bool FromOwnEntity(GameEvent gameEvent) {
    return gameEvent.EmittingEntity == OwningEntity;
  }

  public abstract string Text();
  public abstract string TextFull();

  /// use to reset temporary state you may be modifying
  public virtual void OnAddedOrRemovedFromRune() {}

  internal virtual void Upgrade() {
    Upgrades++;
  }
}