public class HealLargeAction : RuneAction {
  public override int Threshold => 180;

  public HealLargeAction(Entity e) : base(e) {}

  public override void Perform() {
    OwningEntity.Heal(30);
  }

  public override string Text() => "Heal 30 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealLargeAction(otherEntity);
  }
}

public class HealMediumAction : RuneAction {
  public override int Threshold => 66;

  public HealMediumAction(Entity e) : base(e) {}

  public override void Perform() {
    OwningEntity.Heal(10);
  }

  public override string Text() => "Heal 10 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealMediumAction(otherEntity);
  }
}

public class HealTinyAction : RuneAction {
  public override int Threshold => 7;

  public HealTinyAction(Entity e) : base(e) {}

  public override void Perform() {
    OwningEntity.Heal(1);
  }

  public override string Text() => "Heal 1 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealTinyAction(otherEntity);
  }
}