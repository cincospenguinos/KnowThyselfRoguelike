public class EntityDiesTrigger : RuneTrigger {
  public override int Charge => 33;

  public EntityDiesTrigger() : base(null) {}

  public override int OnEvent(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.ENEMY_DEAD) {
      return Charge;
    }

    return 0;
  }

  public override string Text() => $"when killing an enemy.";

  public override void Reset() {}

  public override RuneTrigger Clone() {
    return new EntityDiesTrigger();
  }
}