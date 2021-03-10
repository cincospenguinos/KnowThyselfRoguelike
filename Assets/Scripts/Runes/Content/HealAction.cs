public class HealAction : RuneAction {
    public HealAction(Entity e) : base(e) {}

    public override void Apply() {
        OwningEntity.Heal(2);
    }

    public override RuneAction Clone(Entity otherEntity) {
        return new HealAction(otherEntity);
    }
}