public abstract class EveryXTurnsTrigger : RuneTrigger {
  public abstract int Tempo { get; }
  private int _elapsedTurns;
  public bool IsTriggered => _elapsedTurns > 0 && _elapsedTurns % Tempo == 0;

  public EveryXTurnsTrigger(Entity e) : base(e) {
      _elapsedTurns = 0;
  }

  public override bool ShouldCharge(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED) {
      _elapsedTurns += 1;
    }

    return IsTriggered;
  }

  public override string Text() => $"in <b>{Tempo - _elapsedTurns}</b> turns.";

  public override void Reset() {
    _elapsedTurns = 0;
  }
}

public class Every50TurnsTrigger : EveryXTurnsTrigger {
  public Every50TurnsTrigger(Entity e) : base(e) { }

  public override int ChargeBase => 50;
  public override int Tempo => 50;

  public override RuneTrigger Clone() {
    return new Every50TurnsTrigger(OwningEntity);
  }
}

public class Every100TurnsTrigger : EveryXTurnsTrigger {
  public Every100TurnsTrigger(Entity e) : base(e) { }

  public override int ChargeBase => 110;
  public override int Tempo => 100;

  public override RuneTrigger Clone() {
    return new Every100TurnsTrigger(OwningEntity);
  }
}

public class Every200TurnsTrigger : EveryXTurnsTrigger {
  public Every200TurnsTrigger(Entity e) : base(e) { }

  public override int ChargeBase => 225;
  public override int Tempo => 200;

  public override RuneTrigger Clone() {
    return new Every200TurnsTrigger(OwningEntity);
  }
}
