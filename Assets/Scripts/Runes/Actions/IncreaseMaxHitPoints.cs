public class IncreaseMaxHitPoints : RuneAction {
  public override int ThresholdBase => 400;

  public IncreaseMaxHitPoints(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.MaxHitPoints += 10;
    OwningEntity.Heal(10);
  }

  public override string Text() => $"Heal and gain +10 Max HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseMaxHitPoints(otherEntity);
  }
}