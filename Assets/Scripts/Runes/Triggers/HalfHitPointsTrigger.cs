public class HalfHitPointsTrigger : RuneTrigger {
  public const int OUTPUT_CHARGE = 200;

  public HalfHitPointsTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new HalfHitPointsTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    bool ownerCrossedThreshold = gameEvent.GameEventType == GameEvent.EventType.REACH_HALF_HIT_POINTS && FromOwnEntity(gameEvent); 
    return ownerCrossedThreshold ? OUTPUT_CHARGE : 0;
  }

  public override string Text() => $"When your HP falls below 50% (<color=yellow>{OwningEntity.MaxHitPoints / 2}</color>),";

  public override void Reset() {}
}