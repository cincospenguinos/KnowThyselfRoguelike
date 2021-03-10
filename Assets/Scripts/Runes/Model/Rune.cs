public class Rune {
  private RuneTrigger _trigger;
  private RuneAction _action;

  public Rune(Entity entity) {
    _trigger = new ThreeDeadEnemiesTrigger();
    _action = new RuneAction(entity);
  }

  public void EventOccurred(string eventName) {
    if (_trigger.OnEvent(eventName)) {
      _action.Apply();
      _trigger.Reset();
    }
  }
}