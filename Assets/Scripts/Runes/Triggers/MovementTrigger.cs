public class MovementTrigger : RuneTrigger {
  public override int Charge => 1;

  public MovementTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new MovementTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.MOVEMENT) {
      return Charge;
    }

    return 0;
  } 

  public override string Text() => "when you move.";

  public override void Reset() {}
}