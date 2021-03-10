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