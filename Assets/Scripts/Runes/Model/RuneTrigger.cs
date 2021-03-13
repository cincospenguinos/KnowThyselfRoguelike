using System;
using TMPro;

public abstract class RuneTrigger : RuneShard {
  public abstract int Charge { get; }

  public RuneTrigger(Entity owningEntity) {
    OwningEntity = owningEntity;
  }

  public abstract int OnEvent(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();

  public override string TextFull() => $"<b><color=yellow>Charge {Charge}</color></b> {Text()}";
}