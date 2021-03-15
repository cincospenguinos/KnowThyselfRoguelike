using System;
using TMPro;

public abstract class RuneTrigger : RuneShard {
  public abstract int ChargeBase { get; }
  public int ChargeFinal => (int) UnityEngine.Mathf.Ceil(ChargeBase * (1 + 0.1f * Upgrades));

  public RuneTrigger(Entity owningEntity) {
    OwningEntity = owningEntity;
  }

  public abstract bool ShouldCharge(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();

  public override string TextFull() => $"<b><color=yellow>Charge {ChargeFinal}</color></b> {Text()}";
}