using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
    public DealDamageTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new DealDamageTrigger(OwningEntity);
    }

    public override bool OnEvent(GameEvent gameEvent) {
        return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT;
    }

    public override void Reset() {}

    public override string Text() => "When damage is dealt, ";
}