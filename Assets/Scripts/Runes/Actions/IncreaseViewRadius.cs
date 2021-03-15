using UnityEngine;

public class IncreaseViewRadiusAction : RuneAction {
  public override int ThresholdBase => 300;

  public IncreaseViewRadiusAction(Entity entityToModify) : base(entityToModify) {}

  public override void Perform() {
    OwningEntity.SightRange += 1;
  }

  public override string Text() => $"Permanently gain +1 sight.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseViewRadiusAction(otherEntity);
  }
}
