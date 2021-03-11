public class MovementTrigger : RuneTrigger {
  public MovementTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new MovementTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    if (FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.MOVEMENT) {
      return 1;
    }

    return 0;
  } 

  public override string Text() => "Whenever you move, ";

  public override void Reset() {}
}