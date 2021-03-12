using System;
using TMPro;

public abstract class RuneTrigger : RunePiece {
  public abstract int Charge { get; }
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
  public string TextFull() => $"<b><color=yellow>Charge {Charge}</color></b> {Text()}";
}