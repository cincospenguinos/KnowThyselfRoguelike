using System;

public abstract class RuneAction : RuneShard {
  public abstract int ThresholdBase { get; }
  public int ThresholdFinal => UnityEngine.Mathf.FloorToInt(ThresholdBase / (1 + 0.06f * Upgrades));
  public float ChargePercentage => (float) CurrentCharge / ThresholdFinal;

  protected int CurrentCharge;
  public event Action OnTriggered;

  public RuneAction(Entity entityToModify) {
    OwningEntity = entityToModify;
    CurrentCharge = 0;
  }

  public void ReceiveCharge(int amount) {
    CurrentCharge += amount;
    while (CurrentCharge >= ThresholdFinal) {
      OnTriggered?.Invoke();
      OwningEntity.RuneActionTriggered(Text());
      Perform();
      CurrentCharge -= ThresholdFinal;
    }
  }

  internal virtual void OnEvent(GameEvent gameEvent) { }

  public abstract void Perform();

  public abstract RuneAction Clone(Entity otherEntity);
  public override string TextFull() => $"<b><color=yellow>{CurrentCharge}/{ThresholdFinal}</color></b>\n{Text()}";
}