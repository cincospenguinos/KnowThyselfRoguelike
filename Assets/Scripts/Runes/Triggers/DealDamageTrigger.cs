using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
    public DealDamageTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new DealDamageTrigger(OwningEntity);
    }

    public override int OnEvent(GameEvent gameEvent) {
        return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT ? 1 : 0;
    }

    public override void Reset() {}

    public override string Text() => "When damage is dealt, ";
}