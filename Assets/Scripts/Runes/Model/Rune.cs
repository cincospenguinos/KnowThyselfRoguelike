public class Rune {
  private RuneTrigger _trigger;
  private RuneAction _action;

  public Rune(Entity entity) {
    _trigger = new ThreeDeadEnemiesTrigger();
    _action = new IncreaseDamageAction(entity);
  }

  public Rune(RuneTrigger trigger, RuneAction action) {
    _trigger = trigger;
    _action = action;
  }

  public void EventOccurred(string eventName) {
    if (_trigger.OnEvent(eventName)) {
      _action.Apply();
      _trigger.Reset();
    }
  }
}