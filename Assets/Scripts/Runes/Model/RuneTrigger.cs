public abstract class RuneTrigger {
  protected string EventName;

  public RuneTrigger(string eventName) {
    EventName = eventName;
  }

  public abstract bool OnEvent(GameEvent gameEvent);
  public abstract void Reset();
  public abstract RuneTrigger Clone();
}