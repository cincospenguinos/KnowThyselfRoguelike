public class Rune {
  public readonly RuneTrigger trigger;
  public readonly RuneAction action;
  public event System.Action<int> OnChargeAdded;

  public Rune(Entity entity) {
    trigger = new EntityDiesTrigger();
    action = new IncreaseDamageAction(entity);
  }

  public Rune(RuneTrigger trigger, RuneAction action) {
    this.trigger = trigger;
    this.action = action;
  }

  public void EventOccurred(GameEvent gameEvent) {
    if (action == null || trigger == null) {
      return;
    }

    action.OnEvent(gameEvent);

    if (trigger.ShouldCharge(gameEvent)) {
      var chargeOutput = trigger.ChargeFinal;

      if (chargeOutput > 0) {
        OnChargeAdded?.Invoke(chargeOutput);
        action.ReceiveCharge(chargeOutput);
        trigger.Reset();
      }
    }
  }

  public string ToDebugString() {
    return trigger.GetType().Name + ", " + action.GetType().Name;
  }
}