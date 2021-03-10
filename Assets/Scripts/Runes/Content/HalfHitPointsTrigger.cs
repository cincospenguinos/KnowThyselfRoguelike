using UnityEngine;

public class HalfHitPointsTrigger : RuneTrigger {
  public HalfHitPointsTrigger(Entity e) : base(e) {}

  public override RuneTrigger Clone() {
    return new HalfHitPointsTrigger(OwningEntity);
  }

  public override bool OnEvent(GameEvent gameEvent) {
    return OwningEntity.HitPoints < OwningEntity.MaxHitPoints / 2;
  }

  public override string Text() => $"When your HP falls below 50% (<color=yellow>{OwningEntity.MaxHitPoints / 2}</color>),";

  public override void Reset() {}
}