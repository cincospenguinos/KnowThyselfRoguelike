public class TenTurnsNoDamage : RuneTrigger {
  public override int Charge => 20;
  private int _turnsLeft;

  public TenTurnsNoDamage(Entity e) : base(e) {
    _turnsLeft = 10;
  }

  public override RuneTrigger Clone() {
    return new TenTurnsNoDamage(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_RECEIVED) {
      _turnsLeft = 10;
    } else if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED) {
      _turnsLeft--;
    }

    var shouldTrigger = _turnsLeft <= 0;
    if (shouldTrigger) {
      _turnsLeft = 10;
    }

    return shouldTrigger ? Charge : 0;
  }

  public override string Text() => $"after <b>{_turnsLeft}</b> turns without taking damage.";

  public override void Reset() {}
}