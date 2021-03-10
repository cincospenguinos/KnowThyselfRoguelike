using System;
using TMPro;

public abstract class RuneTrigger {
  public Entity OwningEntity;

  public RuneTrigger(Entity owningEntity) {
    OwningEntity = owningEntity;
  }

  public abstract bool OnEvent(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();
  public abstract string Text();
}