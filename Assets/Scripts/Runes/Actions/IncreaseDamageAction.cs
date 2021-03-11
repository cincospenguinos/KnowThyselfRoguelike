public class IncreaseDamageAction : RuneAction {
  private const int CHARGE_THRESHOLD = 400;

  public IncreaseDamageAction(Entity entityToModify) : base(entityToModify) {}

  public override void ReceiveCharge(int amount) {
    CurrentCharge += amount;

    while (CurrentCharge > CHARGE_THRESHOLD) {
      OwningEntity.DamageModifier += 2;
      CurrentCharge -= CHARGE_THRESHOLD;
    }
  }

  public override string Text() => $"increase your damage by 2.";

  public override RuneAction Clone(Entity otherEntity) {
    return new IncreaseDamageAction(otherEntity);
  }
}