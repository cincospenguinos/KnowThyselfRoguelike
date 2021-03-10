public class ThreeDeadEnemiesTrigger : RuneTrigger {
    private int _enemiesDied;

    public bool IsTriggered => _enemiesDied % 3 == 0 && _enemiesDied > 0;

    public ThreeDeadEnemiesTrigger() : base(null) {
        _enemiesDied = 0;
    }

    public override bool OnEvent(GameEvent gameEvent) {
        if (gameEvent.GameEventType == GameEvent.EventType.ENEMY_DEAD) {
            _enemiesDied += 1;
        }

        return IsTriggered;
    }

    public override string Text() => $"Every third kill (<color=yellow>{3 - _enemiesDied} remaining</color>), ";

    /// Implementation not needed
    public override void Reset(){}

    public override RuneTrigger Clone() {
        return new ThreeDeadEnemiesTrigger();
    }
}