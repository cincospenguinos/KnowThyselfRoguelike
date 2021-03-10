public class Rune {
  public readonly RuneTrigger trigger;
  public readonly RuneAction action;

  public Rune(Entity entity) {
    trigger = new ThreeDeadEnemiesTrigger();
    action = new IncreaseDamageAction(entity);
  }

  public Rune(RuneTrigger trigger, RuneAction action) {
    this.trigger = trigger;
    this.action = action;
  }

  public void EventOccurred(GameEvent gameEvent) {
    if (trigger.OnEvent(gameEvent)) {
      action.Apply();
      trigger.Reset();
    }
  }
}