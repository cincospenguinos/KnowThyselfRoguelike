public class IncreaseDamageAction : RuneAction {
  public override int ThresholdBase => 400;

  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.AddedDamage += 5;
  }

  public override string Text() => $"Permanently gain +5 damage.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageAction(otherEntity);
  }
}

public class IncreaseDamageSmallAction : RuneAction {
  public override int ThresholdBase => 100;

  public IncreaseDamageSmallAction(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.AddedDamage++;
  }

  public override string Text() => $"Permanently gain +1 damage.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageSmallAction(otherEntity);
  }
}