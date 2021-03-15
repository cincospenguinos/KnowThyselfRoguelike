public class EntityDiesTrigger : RuneTrigger {
  public override int ChargeBase => 33;

  public EntityDiesTrigger() : base(null) {}

  public override bool ShouldCharge(GameEvent gameEvent) {
    return gameEvent.GameEventType == GameEvent.EventType.ENEMY_DEAD;
  }

  public override string Text() => $"when an enemy dies.";

  public override void Reset() {}

  public override RuneTrigger Clone() {
    return new EntityDiesTrigger();
  }
}