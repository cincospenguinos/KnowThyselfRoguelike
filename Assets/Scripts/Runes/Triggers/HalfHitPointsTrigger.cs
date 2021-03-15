public class HalfHitPointsTrigger : RuneTrigger {
  public override int ChargeBase => 200;
  public int Cooldown = 100;

  public HalfHitPointsTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new HalfHitPointsTrigger(OwningEntity);
  }

  public override bool ShouldCharge(GameEvent gameEvent) {
    if (gameEvent.GameEventType == GameEvent.EventType.TURN_ELAPSED && Cooldown < 100) {
      Cooldown += 1;
    }

    bool ownerCrossedThreshold = gameEvent.GameEventType == GameEvent.EventType.REACH_HALF_HIT_POINTS && FromOwnEntity(gameEvent) && Cooldown == 100; 
    return ownerCrossedThreshold;
  }

  public override string Text() => $"when your HP falls below (<b>{(int)(OwningEntity.MaxHitPoints / 2)}</b>, {Cooldown} cooldown).";

  public override void Reset() {}
}