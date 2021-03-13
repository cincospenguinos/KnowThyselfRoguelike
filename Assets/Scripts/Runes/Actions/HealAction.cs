public class HealAction : RuneAction {
  public override int Threshold => 66;

  public HealAction(Entity e) : base(e) {}

  public override void Perform() {
    OwningEntity.Heal(10);
  }

  public override string Text() => "Heal 10 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealAction(otherEntity);
  }
}