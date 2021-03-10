public abstract class RuneTrigger {
  protected string EventName;

  public RuneTrigger(string eventName) {
    EventName = eventName;
  }

  public abstract bool OnEvent(string name);
  public abstract void Reset();
}

public class ThreeDeadEnemiesTrigger : RuneTrigger {
    private int _enemiesDied;

    public bool IsTriggered => _enemiesDied % 3 == 0;

    public ThreeDeadEnemiesTrigger() : base("EnemyDead") {
        _enemiesDied = 0;
    }

    public override bool OnEvent(string name) {
        if (EventName == name) {
            _enemiesDied += 1;
        }

        return IsTriggered;
    }

    public override void Reset(){}
}