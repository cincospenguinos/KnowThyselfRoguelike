public class IncreaseMaxHitPoints : RuneAction {
  public override int Threshold => 400;

  public IncreaseMaxHitPoints(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.MaxHitPoints += 2;
  }

  public override string Text() => $"Permanently gain +1 damage.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseMaxHitPoints(otherEntity);
  }
}