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

  public void EventOccurred(GameEvent gameEvent) {
    if (_trigger.OnEvent(gameEvent)) {
      _action.Apply();
      _trigger.Reset();
    }
  }
}