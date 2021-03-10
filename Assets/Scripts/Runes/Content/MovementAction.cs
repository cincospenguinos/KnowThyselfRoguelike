public class MovementTrigger : RuneTrigger
{
    public MovementTrigger(Entity e) : base(e) {}

    public override RuneTrigger Clone() {
        return new MovementTrigger(OwningEntity);
    }

    public override bool OnEvent(GameEvent gameEvent) {
        if (gameEvent.EmittingEntity == OwningEntity && gameEvent.EventName == "MovementEvent") {
            return true;
        }

        return false;
    } 

    public override void Reset() {}
}