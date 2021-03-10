public class HealAction : RuneAction {
  public HealAction(Entity e) : base(e) {}

  public override void Apply() {
    OwningEntity.Heal(2);
  }

  public override string Text() => "Heal 2 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealAction(otherEntity);
  }
}