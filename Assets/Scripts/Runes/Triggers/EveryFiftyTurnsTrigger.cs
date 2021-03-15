public class EveryFiftyTurnsTrigger : RuneTrigger {
  public override int ChargeBase => 50;
  private int _elapsedTurns;
  public bool IsTriggered => _elapsedTurns > 0 && _elapsedTurns % 50 == 0;

  public EveryFiftyTurnsTrigger(Entity e) : base(e) {
      _elapsedTurns = 0;
  }

  public override RuneTrigger Clone() {
    return new EveryFiftyTurnsTrigger(OwningEntity);
  }

  public override bool ShouldCharge(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED) {
      _elapsedTurns += 1;
    }

    return IsTriggered;
  }

  public override string Text() => $"in <b>{50 - _elapsedTurns}</b> turns.";

  public override void Reset() {
    _elapsedTurns = 0;
  }
}