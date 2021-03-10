public class GameEvent {
    public string EventName;
    public Entity EmittingEntity;

    public GameEvent(string eventName) {
        EventName = eventName;
        EmittingEntity = null;
    }

    public GameEvent(string eventName, Entity emitting) {
        EventName = eventName;
        EmittingEntity = emitting;
    }
}