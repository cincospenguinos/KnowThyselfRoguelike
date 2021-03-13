using System;
using TMPro;

public abstract class RuneTrigger : RuneShard {
  public abstract int Charge { get; }
  public Entity OwningEntity;

  public RuneTrigger(Entity owningEntity) {
    OwningEntity = owningEntity;
  }

  public abstract int OnEvent(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();

  public bool FromOwnEntity(GameEvent gameEvent) {
    return gameEvent.EmittingEntity == OwningEntity;
  }
  public override string TextFull() => $"<b><color=yellow>Charge {Charge}</color></b> {Text()}";
}