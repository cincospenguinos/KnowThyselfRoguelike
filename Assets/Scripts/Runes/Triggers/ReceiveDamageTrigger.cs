public class ReceiveDamageTrigger : RuneTrigger {
    public ReceiveDamageTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new ReceiveDamageTrigger(OwningEntity);
    }

    public override bool OnEvent(GameEvent gameEvent) {
        return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_RECEIVED;
    }

    public override void Reset() {}

    public override string Text() => "When damage is received, ";
}