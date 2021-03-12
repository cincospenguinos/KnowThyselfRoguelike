using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
  public override int Charge => 11;

  public DealDamageTrigger(Entity e) : base(e) { }

  public override RuneTrigger Clone() {
    return new DealDamageTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    bool entityDealtDamage = FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT;

    return entityDealtDamage ? Charge : 0;
  }

  public override void Reset() { }

  public override string Text() => "when you deal damage.";
}