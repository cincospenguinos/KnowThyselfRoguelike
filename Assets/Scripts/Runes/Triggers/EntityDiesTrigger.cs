public class EntityDiesTrigger : RuneTrigger {
  public override int Charge => 33;
  private int _enemiesDied;

  public bool IsTriggered => _enemiesDied % 3 == 0 && _enemiesDied > 0;

  public EntityDiesTrigger() : base(null) {
    _enemiesDied = 0;
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.ENEMY_DEAD) {
      _enemiesDied += 1;
    }

    return IsTriggered ? Charge : 0;
  }

  public override string Text() => $"after killing (<b>{3 - _enemiesDied}</b>) more enemies.";

  public override void Reset() {
    _enemiesDied = 0;
  }

  public override RuneTrigger Clone() {
    return new EntityDiesTrigger();
  }
}