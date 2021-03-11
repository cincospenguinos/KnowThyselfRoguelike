public class EveryFiftyTurnsTrigger : RuneTrigger {
  public const int OUTPUT_CHARGE = 50;
  private int _elapsedTurns;
  public bool IsTriggered => _elapsedTurns > 0 && _elapsedTurns % 50 == 0;

  public EveryFiftyTurnsTrigger(Entity e) : base(e) {
      _elapsedTurns = 0;
  }

  public override RuneTrigger Clone() {
    return new EveryFiftyTurnsTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED) {
      _elapsedTurns += 1;
    }

    return IsTriggered ? OUTPUT_CHARGE : 0;
  }

  public override string Text() => $"When 50 turns have elapsed (<color=yellow>{_elapsedTurns} so far</color>),";

  public override void Reset() {
    _elapsedTurns = 0;
  }
}