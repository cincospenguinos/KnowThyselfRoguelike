public class HalfHitPointsTrigger : RuneTrigger {
  public override int Charge => 200;

  public HalfHitPointsTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new HalfHitPointsTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    bool ownerCrossedThreshold = gameEvent.GameEventType == GameEvent.EventType.REACH_HALF_HIT_POINTS && FromOwnEntity(gameEvent); 
    return ownerCrossedThreshold ? Charge : 0;
  }

  public override string Text() => $"when your HP falls below (<b>{(int)(OwningEntity.MaxHitPoints / 2)}</b>).";

  public override void Reset() {}
}