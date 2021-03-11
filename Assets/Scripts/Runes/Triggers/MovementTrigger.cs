public class MovementTrigger : RuneTrigger {
  public const int OUTPUT_CHARGE = 1;

  public MovementTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new MovementTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.MOVEMENT) {
      return OUTPUT_CHARGE;
    }

    return 0;
  } 

  public override string Text() => "Whenever you move, ";

  public override void Reset() {}
}