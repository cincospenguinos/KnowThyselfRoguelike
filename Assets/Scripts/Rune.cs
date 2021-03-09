public class RuneTrigger {
  private string _eventName;
  private int _enemiesDied;

  public bool IsTriggered => _enemiesDied % 3 == 0;

  public RuneTrigger(string eventName) {
    _enemiesDied = 0;
    _eventName = eventName;
  }

  public bool OnEvent(string name) {
    if (_eventName == name) {
      _enemiesDied += 1;
    }

    return IsTriggered;
  }

  /// Not needed for this specific trigger...
  public void Reset() {}
}

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