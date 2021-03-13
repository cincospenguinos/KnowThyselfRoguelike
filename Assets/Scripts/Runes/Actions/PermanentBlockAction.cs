
public class PermanentBlockAction : RuneAction {
  public PermanentBlockAction(Entity entityToModify) : base(entityToModify) { }

  public override int Threshold => 400;

  public override RuneAction Clone(Entity otherEntity) {
    return new PermanentBlockAction(otherEntity);
  }

  public override void Perform() {
    OwningEntity.Block++;
  }

  public override string Text() => "Permanently gain 1 block.";
}

public class LessHPMoreBlockAction : RuneAction {
  public LessHPMoreBlockAction(Entity entityToModify) : base(entityToModify) { }

  public override int Threshold => 50;

  public override RuneAction Clone(Entity otherEntity) {
    return new PermanentBlockAction(otherEntity);
  }

  public override void Perform() {
    OwningEntity.Block++;
    OwningEntity.TakeDamage(5);
    OwningEntity.MaxHitPoints -= 5;
  }

  public override string Text() => "Permanently gain 1 block, but take 5 damage and lose 5 max HP.";
}