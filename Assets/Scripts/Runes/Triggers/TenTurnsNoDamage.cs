public class TenTurnsNoDamage : RuneTrigger {
  public const int OUTPUT_CHARGE = 20;
  private int _noDamage;

  public bool IsTriggered => _noDamage > 0 && _noDamage % 10 == 0;

  public TenTurnsNoDamage(Entity e) : base(e) {
    _noDamage = 0;
  }

  public override RuneTrigger Clone() {
    return new TenTurnsNoDamage(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_RECEIVED) {
      _noDamage = 0;
    } else if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED) {
      _noDamage += 1;
    }

    return IsTriggered ? OUTPUT_CHARGE : 0;
  }

  public override string Text() => $"When ten turns have elapsed without taking damage (<color=yellow>{_noDamage}</color> so far,)";

  public override void Reset() {}
}