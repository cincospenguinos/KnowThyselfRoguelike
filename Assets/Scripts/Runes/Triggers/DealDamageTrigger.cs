using UnityEngine;

public class DealDamageTrigger : RuneTrigger {
    public DealDamageTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new DealDamageTrigger(OwningEntity);
    }

    public override bool OnEvent(GameEvent gameEvent) {
        if (OwningEntity.GetType() == typeof(Player)) {
            Debug.Log("From own entity" + FromOwnEntity(gameEvent));
            Debug.Log(gameEvent.GameEventType);
        }

        return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT;
    }

    public override void Reset() {}

    public override string Text() => "When damage is dealt, ";
}