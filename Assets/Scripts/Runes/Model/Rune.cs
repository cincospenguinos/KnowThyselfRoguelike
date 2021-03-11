public class Rune {
  public readonly RuneTrigger trigger;
  public readonly RuneAction action;
  public event System.Action<GameEvent> OnTriggered;

  public Rune(Entity entity) {
    trigger = new EntityDiesTrigger();
    action = new IncreaseDamageAction(entity);
  }

  public Rune(RuneTrigger trigger, RuneAction action) {
    this.trigger = trigger;
    this.action = action;
  }

  public void EventOccurred(GameEvent gameEvent) {
    int chargeOutput = trigger.OnEvent(gameEvent);

    if (chargeOutput > 0) {
      action.ReceiveCharge(chargeOutput);
      trigger.Reset();
      OnTriggered?.Invoke(gameEvent);
    }
  }

  public string ToDebugString() {
    return trigger.GetType().Name + ", " + action.GetType().Name;
  }
}