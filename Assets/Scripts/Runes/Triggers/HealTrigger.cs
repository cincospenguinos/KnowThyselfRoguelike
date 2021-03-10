public class HealTrigger : RuneTrigger {

    public HealTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new HealTrigger(OwningEntity);
    }

    public override bool OnEvent(GameEvent gameEvent) {
        return FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.HEAL;
    }

    public override void Reset() {}

    public override string Text() => "When healing any amount, ";
}