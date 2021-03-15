public class MovementTrigger : RuneTrigger {
  public override int ChargeBase => 1;

  public MovementTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new MovementTrigger(OwningEntity);
  }

  public override bool ShouldCharge(GameEvent gameEvent) {
    return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.MOVEMENT;
  } 

  public override string Text() => "when you move.";

  public override void Reset() {}
}