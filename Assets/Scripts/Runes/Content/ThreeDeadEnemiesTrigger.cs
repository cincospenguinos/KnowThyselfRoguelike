public class ThreeDeadEnemiesTrigger : RuneTrigger {
    private int _enemiesDied;

    public bool IsTriggered => _enemiesDied % 3 == 0;

    public ThreeDeadEnemiesTrigger() : base(null) {
        _enemiesDied = 0;
    }

    public override bool OnEvent(GameEvent gameEvent) {
        if (gameEvent.EventName == "EnemyDead") {
            _enemiesDied += 1;
        }

        return IsTriggered;
    }

    /// Implementation not needed
    public override void Reset(){}

    public override RuneTrigger Clone() {
        return new ThreeDeadEnemiesTrigger();
    }
}