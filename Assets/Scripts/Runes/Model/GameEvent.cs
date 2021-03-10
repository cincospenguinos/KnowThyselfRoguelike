public class GameEvent {
    public enum EventType {
        ENEMY_DEAD, MOVEMENT, REACH_HALF_HIT_POINTS,
    };

    public EventType GameEventType;
    public Entity EmittingEntity;

    public GameEvent(EventType eventType) {
        GameEventType = eventType;
        EmittingEntity = null;
    }

    public GameEvent(EventType eventName, Entity emitting) {
        GameEventType = eventName;
        EmittingEntity = emitting;
    }
}