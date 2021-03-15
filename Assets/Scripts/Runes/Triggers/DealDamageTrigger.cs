using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
  public override int ChargeBase => 11;

  public DealDamageTrigger(Entity e) : base(e) { }

  public override RuneTrigger Clone() {
    return new DealDamageTrigger(OwningEntity);
  }

  public override bool ShouldCharge(GameEvent gameEvent) {
    return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT;
  }

  public override void Reset() { }

  public override string Text() => "when you deal damage.";
}