public class RuneAction {
  private Entity _entity;

  public RuneAction(Entity entityToModify) {
    _entity = entityToModify;
  }

  public void Apply() {
    _entity.DamageModifier += 2;
  }
}

public class Rune {
  private RuneTrigger _trigger;
  private RuneAction _action;

  public Rune(Entity entity) {
    _trigger = new RuneTrigger("EnemyDead");
    _action = new RuneAction(entity);
  }

  public void EventOccurred(string eventName) {
    // Every time an enemy dies, double the damage modifier
    if (_trigger.OnEvent(eventName)) {
      _action.Apply();
      _trigger.Reset();
    }
  }
}