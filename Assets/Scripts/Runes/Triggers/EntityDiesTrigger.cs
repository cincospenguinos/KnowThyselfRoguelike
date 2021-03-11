public class EntityDiesTrigger : RuneTrigger {
    private int _enemiesDied;

    public bool IsTriggered => _enemiesDied % 3 == 0 && _enemiesDied > 0;

    public EntityDiesTrigger() : base(null) {
        _enemiesDied = 0;
    }

    public override bool OnEvent(GameEvent gameEvent) {
        if (gameEvent.GameEventType == GameEvent.EventType.ENEMY_DEAD) {
            _enemiesDied += 1;
        }

        return IsTriggered;
    }

    public override string Text() => $"Every third kill (<color=yellow>{3 - _enemiesDied} remaining</color>), ";

    public override void Reset(){
        _enemiesDied = 0;
    }

    public override RuneTrigger Clone() {
        return new EntityDiesTrigger();
    }
}