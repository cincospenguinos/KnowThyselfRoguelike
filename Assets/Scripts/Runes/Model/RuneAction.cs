using System;

public abstract class RuneAction : RuneShard {
  public abstract int Threshold { get; }
  protected int CurrentCharge;
  public event Action OnTriggered;

  public RuneAction(Entity entityToModify) {
    OwningEntity = entityToModify;
    CurrentCharge = 0;
  }

  public void ReceiveCharge(int amount) {
    CurrentCharge += amount;
    while (CurrentCharge >= Threshold) {
      OnTriggered?.Invoke();
      Perform();
      CurrentCharge -= Threshold;
    }
  }

  public abstract void Perform();

  public abstract RuneAction Clone(Entity otherEntity);
  public override string TextFull() => $"<b><color=yellow>{CurrentCharge}/{Threshold}</color></b>\n{Text()}";
}