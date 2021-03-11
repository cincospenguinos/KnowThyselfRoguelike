using System;
using TMPro;

public abstract class RuneTrigger {
  public Entity OwningEntity;

  public RuneTrigger(Entity owningEntity) {
    OwningEntity = owningEntity;
  }

  public abstract int OnEvent(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();
  public abstract string Text();

  public bool FromOwnEntity(GameEvent gameEvent) {
    return gameEvent.EmittingEntity == OwningEntity;
  }
}