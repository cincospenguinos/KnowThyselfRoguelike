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

public class Rune {
  private RuneTrigger _trigger;
  private Entity _owningEntity;

  public Rune(Entity entity) {
    _owningEntity = entity;
    _trigger = new RuneTrigger("EnemyDead");
  }

  public void EventOccurred(string eventName) {
    // Every time an enemy dies, double the damage modifier
    if (_trigger.OnEvent(eventName)) {
      _owningEntity.DamageModifier += 2;
      _trigger.Reset();
    }
  }
}