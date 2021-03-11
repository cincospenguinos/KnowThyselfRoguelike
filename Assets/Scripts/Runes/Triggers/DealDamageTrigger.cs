using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
    public const int OUTPUT_CHARGE = 11;

    public DealDamageTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new DealDamageTrigger(OwningEntity);
    }

    public override int OnEvent(GameEvent gameEvent) {
        bool entityDealtDamage = FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT;

        return entityDealtDamage ? OUTPUT_CHARGE : 0;
    }

    public override void Reset() {}

    public override string Text() => "When damage is dealt, ";
}